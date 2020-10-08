using Core.Gateway.Helper;
using Core.Gateway.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Core.Gateway.Interfaces;
using System;
using System.Threading.Tasks;
using Core.Gateway.Domain.Interfaces;
using Core.Gateway.Api.Helper;

namespace Core.Gateway.Services
{
    public class TransactionService : IDisposable
    {

        #region TransactionService
        private readonly IConfiguration _config;
        private readonly IPaymentService _paymentService;
        private readonly IMessageService _messageService;

        public TransactionService(IConfiguration config, IPaymentService paymentService, IMessageService messageService)
        {
            _config = config;
            _paymentService = paymentService;
            _messageService = messageService;
        }

        public void Dispose()
        {

        }
        #endregion

        #region ProcessRequest
        public async Task<dynamic> ProcessRequest(Request request)
        {
            request.isValid = true;
            var errorModel = new ErrorModel();
            

            try
            {
                var val = await _messageService.GetErrorMessage("ECord1");

                Logger.InformationLog($"In TransactionService.ProcessRequest, Process Request Start");

                var merchantInfoResult = await _paymentService.GetMerchantInfo(errorModel,request.MerchantInfo.MerchantKey, request.MerchantInfo.IpAddress, Convert.ToInt32(request.MerchantInfo.ProcessorId));               

                if(errorModel.validationFailedMsg.Count>0 || (errorModel.errors !=null && errorModel.errors.Count >0))
                {
                    GatewayResult Result = BuildValidationOrErrorFailedStatusReturnObject(errorModel,request.MerchantInfo.TransactionType);
                    return Result;
                }

                Logger.InformationLog($"In TransactionController.ProcessRequest, GetMerchantInfo: " + JsonConvert.SerializeObject(merchantInfoResult));

                //If creditCardCryptogram is Not Null
                if (!string.IsNullOrEmpty(request.CreditCardCryptogram) && string.IsNullOrEmpty(request.CardNumber))
                {
                    var creditCardFromCryptogramResult = await _paymentService.GetCreditCardFromCryptogram(new PaymentDataFromCryptogramInput()
                    {
                        Cryptogram = request.CreditCardCryptogram,
                        TransactionType = Convert.ToString(request.MerchantInfo.TransactionType)
                    });
                    if (creditCardFromCryptogramResult.Message != null)
                    {
                        request.CardNumber = creditCardFromCryptogramResult.CardNumber;
                        request.CardExpMonth = Convert.ToInt32(creditCardFromCryptogramResult.CardExpMonth);
                        request.CardExpYear = Convert.ToInt32(creditCardFromCryptogramResult.CardExpYear);
                        request.CVV = Convert.ToInt32(creditCardFromCryptogramResult.CardSecurityCode);
                        request.ConvFeeAmount = Convert.ToDecimal(creditCardFromCryptogramResult.FeeAmount);
                    }
                }

                //If token number is null then call tokenizer to generate token number
                if (string.IsNullOrEmpty(request.CreditCardToken) && !string.IsNullOrEmpty(request.CardNumber))
                {
                    request.CreditCardToken = _paymentService.GetTokenFromCreditCard(request.CardNumber);
                }

                var oldToken = request.CreditCardToken;

                //Check null values and MagData is empty
                var isNullProperties = (!string.IsNullOrEmpty(request.CreditCardToken) && request.CardExpMonth != null
                       && request.CardExpYear != null && string.IsNullOrEmpty(request.MagData));
                var process = new Process();
                if (isNullProperties)
                {
                    request.isValid = false;
                    process.ExpMonth = request.CardExpMonth;
                    process.ExpYear = request.CardExpYear;
                    process.CardType = CreditCardHelper.GetCreditCardType(request.CardNumber ?? _paymentService.GetCreditCardNumberFromToken(request.CreditCardToken));
                    process.Token = request.CreditCardToken;

                    //GetUpdatedCardDetails
                    process = await _paymentService.GetUpdatedCardDetails(process);

                    //Re asign the expMonth, expYear and token
                    request.CardExpMonth = process.ExpMonth;
                    request.CardExpYear = process.ExpYear;
                    request.CreditCardToken = process.Token;
                }

                process = ValidateExpMonthAndYear(errorModel, process);

                if (errorModel.validationFailedMsg.Count > 0 || (errorModel.errors != null && errorModel.errors.Count > 0))
                {
                    GatewayResult Result = BuildValidationOrErrorFailedStatusReturnObject(errorModel, request.MerchantInfo.TransactionType);
                    return Result;
                }

                ValidateExtensions.ValidateIpAddress(merchantInfoResult.IpAddress, errorModel);

                if (merchantInfoResult.AccountType == IndustryTypesEnum.ach.ToString())
                {
                    errorModel.errors.Add(string.Format("Invalid Account Type For Transaction ('{0}')", merchantInfoResult.AccountType));
                    return null;
                }
                var ValidateHelper = new ValidateHelper();

                var orderId= new ValidateHelper().ValidateOrderIdAndGenerateNewOrderIdIfNeededAsync(request, merchantInfoResult, errorModel);

                process = await _paymentService.ValidateInitAndBuildProcessObject(request, merchantInfoResult, errorModel, process);

                if (!request.isValid)
                {
                    errorModel.validationFailedMsg.Add(new ValidationFailedMsg() { Key = "", Message = "" });//On Hold
                }

                if (errorModel.errors.Count > 0 || errorModel.validationFailedMsg.Count > 0)
                {
                    GatewayResult Result = BuildValidationOrErrorFailedStatusReturnObject(errorModel, request.MerchantInfo.TransactionType);
                    return Result;
                }

                if (!string.IsNullOrWhiteSpace(oldToken) && !string.IsNullOrWhiteSpace(process.Token))
                {
                    if (!oldToken.Equals(process.Token))
                    {
                        process.CardNumber = _paymentService.GenerateCardNumberUsingToken(process.Token.Trim());
                    }
                }

                if (process != null && request.OtherFields != null && request.OtherFields.Count > 0)
                {
                    _paymentService.ValidateAndAddCustomFields(process, request.OtherFields/*, info.processor*/);// On Hold
                }
                if (!string.IsNullOrWhiteSpace(request.CreditCardCryptogram))
                {
                    request.expireCryptogram =  CryptogramHelper.ForceExpireCryptogramAsync(_config.GetSection("CryptogramApi").Value, request);
                }
                IndustryTypesEnum industryType = request.MerchantInfo.accountType.ParseEnum<IndustryTypesEnum>();
                
                
                return process;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog($"Exception In  TransactionService.ProcessRequest. exception={ex.Message}, Trace={ex.StackTrace}");
                throw ex;
            }
            finally
            {
                Logger.InformationLog($"Out  TransactionService.ProcessRequest, Process Request End");
            }
        }
        #endregion

        private GatewayResult BuildValidationOrErrorFailedStatusReturnObject(ErrorModel errorModel,string TransactionType)
        {
            //Gateway business rules failed
            return new GatewayResult
            {
                isError = (errorModel.errors != null && errorModel.errors.Count > 0) ? true : false,
                errorMessages = errorModel.errors,
                validationHasFailed = errorModel.validationFailedMsg.Count > 0,
                validationFailures = errorModel.validationFailedMsg,
                isSuccess = false,
                action = TransactionType                
            };            
        }


        private Process ValidateExpMonthAndYear(ErrorModel errorModel, Process process)
        {
            if (ValidateExtensions.ValidateCreditCardExpMonth(process.ExpMonth.ToString(),errorModel))
            {
                var ExpMonth = process.ExpMonth.ToString();
                ExpMonth = Convert.ToInt32(ExpMonth).ToString("D2");
                process.ExpMonth = Convert.ToInt32(ExpMonth);
            }

            if (ValidateExtensions.ValidateCreditCardExpYear(process.ExpYear.ToString(),errorModel))
            {
                var ExpYear = process.ExpYear.ToString();
                ExpYear = Convert.ToInt32(ExpYear).ToString("D2");
                process.ExpMonth = Convert.ToInt32(ExpYear);
            }
            return process;
        }
    }
}
