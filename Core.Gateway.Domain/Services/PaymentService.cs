using Core.Gateway.Data.Queries;
using Core.Gateway.Helper;
using Core.Gateway.Models;
using Core.Payment.Tokenizer;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Payment.Data.Repository;
using Core.Gateway.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Gateway.Domain.Interfaces;
using System.Collections;
namespace Core.Gateway.Services
{
    public class PaymentService : IPaymentService
    {
        #region PaymentService
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository _repository;
        private readonly ICommandText _query;
        private readonly IProcessHelper _processHelper;
        public PaymentService(IConfiguration configuration, IGenericRepository repository, ICommandText query, IProcessHelper processHelper)
        {
            _configuration = configuration;
            _repository = repository;
            _query = query;
            _processHelper = processHelper;
        }
        #endregion

        #region GetMerchantInfo
        public async Task<MerchantInfoResult> GetMerchantInfo(ErrorModel errorModel, String MerchantKey, String IpAddress, Nullable<int> processorId)
        {
            Logger.InformationLog($"In PaymentService.GetMerchantInfo, Get MerchantInfo Start");
            var validationFailedMsgList = new List<ValidationFailedMsg>();
            try
            {
                var dbPara = new DynamicParameters();
                dbPara.Add("processorId", Convert.ToInt64(processorId));
                var MerchantInforesult = await Task.FromResult(_repository.RunQuery<MerchantInfoResult>(_query.GetMerchantInfo, dbPara)).Result;

                if (MerchantInforesult != null)
                {
                    if (MerchantInforesult.GatewayId.HasValue)
                    {
                        if (ConvertToNullableGuid(MerchantKey) != MerchantInforesult.GatewayId.Value)
                        {
                            validationFailedMsgList.Add(new ValidationFailedMsg()
                            {
                                Key = "MerchantInfo",
                                Message = string.Format("MerchantKey gatewayId ({0}) or processorId ({1}) does not have a match in Trans center. Please verify MerchantKey value is correct", MerchantKey, processorId)
                            });
                        }
                    }
                    else
                    {
                        validationFailedMsgList.Add(new ValidationFailedMsg()
                        {
                            Key = "MerchantInfo",
                            Message = "Gateway Id is not set in Merchant transactions. Please verify Gateway Id has a value in Trans center"
                        });
                    }
                }
                else
                {
                    validationFailedMsgList.Add(new ValidationFailedMsg()
                    {
                        Key = "MerchantInfo",
                        Message = "Merchant key not provided"
                    });
                }
                errorModel.validationFailedMsg = validationFailedMsgList;
                return MerchantInforesult;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog($"Exception In  PaymentService.GetMerchantInfo. exception={ex.Message}, Trace={ex.StackTrace}");
                throw ex;
            }
            finally
            {
                Logger.InformationLog($"Out  PaymentService.GetMerchantInfo, Get MerchantInfo End");
            }
        }
        #endregion

        #region GetCreditCardFromCryptogram
        public async Task<CryptogramLookupResultData> GetCreditCardFromCryptogram(PaymentDataFromCryptogramInput PaymentDataFromCryptogramInput)
        {
            Logger.InformationLog($"In PaymentService.GetCreditCardFromCryptogram, Get CreditCard From Cryptogram Process Start");

            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(PaymentDataFromCryptogramInput), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(_configuration.GetSection("CryptogramApi").Value + "Lookup", content))
                    {
                        return JsonConvert.DeserializeObject<CryptogramLookupResultData>(await response.Content.ReadAsStringAsync(), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLog($"Exception In  PaymentService.GetCreditCardFromCryptogram. exception={ex.Message}, Trace={ex.StackTrace}");
                throw ex;
            }
            finally
            {
                Logger.InformationLog($"Out  PaymentService.GetCreditCardFromCryptogram, Get CreditCard From Cryptogram Process End");
            }
        }
        #endregion

        #region GetTokenFromCreditCard
        public string GetTokenFromCreditCard(string CardCardNumber)
        {
            try
            {
                return TokenizerProcess.GetTokenFromCCTS(CardCardNumber, GetConfigurationModel());
            }
            catch (Exception tokenEx)
            {
                throw tokenEx;
            }
        }
        #endregion

        #region GetCreditCardNumberFromToken
        public string GetCreditCardNumberFromToken(string CreditCardToken)
        {
            return TokenizerProcess.GetCardFromTokenTS(CreditCardToken, GetConfigurationModel()); ;
        }
        #endregion


        public async Task<Process> GetUpdatedCardDetails(Process process)
        {
            Logger.InformationLog($"In PaymentService.GetUpdatedCardDetails, Get Updated Card Details Process Start");
            try
            {
                //var result = await _repository.GetUpdatedCardDetails(process.Token, process.CardType, Convert.ToString(process.ExpMonth) + Convert.ToString(process.ExpYear));

                var dbPara = new DynamicParameters();
                dbPara.Add("Token", Convert.ToInt64(process.Token));
                dbPara.Add("CardType", process.CardType);
                dbPara.Add("Expiration", Convert.ToString(process.ExpMonth) + Convert.ToString(process.ExpYear));

                var result = await Task.FromResult(_repository.RunQuery<dynamic>(_query.GetUpdatedCardDetails, dbPara)).Result;

                process.Token = result.Token;
                process.CardType = result.CardType;
                process.ExpMonth = Convert.ToInt32(result.Expiration.Substring(0, 2));
                process.ExpYear = Convert.ToInt32(result.Expiration.Substring(2, 2));

                return process;
            }
            catch (Exception ex)
            {
                Logger.ErrorLog($"Exception In  PaymentService.GetUpdatedCardDetails. exception={ex.Message}, Trace={ex.StackTrace}");
                throw ex;
            }
            finally
            {
                Logger.InformationLog($"Out  PaymentService.GetUpdatedCardDetails, Get Updated Card Details Process End");
            }
        }

        public async Task<Process> ValidateInitAndBuildProcessObject(Request request, MerchantInfoResult merchantInfoResult, ErrorModel errorModel, Process process)
        {
            try
            {

              
                //var orderId =await new ValidateHelper().ValidateOrderIdAndGenerateNewOrderIdIfNeededAsync(request, merchantInfoResult, errorModel, _processHelper);

                return process;
            }
            catch(Exception ex)
            {
                Logger.ErrorLog($"Exception In  PaymentService.GetUpdatedCardDetails. exception={ex.Message}, Trace={ex.StackTrace}");
                throw ex;
            }
            finally
            {
                Logger.InformationLog($"Out  PaymentService.GetUpdatedCardDetails, Get Updated Card Details Process End");
            }
        }


        #region GetConfigurationModel
        public ConfigurationModel GetConfigurationModel()
        {
            return new ConfigurationModel()
            {
                ProcessName = _configuration.GetSection("TokenizerConfiguration:ProcessName").Value,
                NaeUser = _configuration.GetSection("TokenizerConfiguration:NaeUser").Value,
                NaePswd = _configuration.GetSection("TokenizerConfiguration:NaePswd").Value,
                DBUser = _configuration.GetSection("TokenizerConfiguration:DBUser").Value,
                DBPswd = _configuration.GetSection("TokenizerConfiguration:DBPswd").Value,
                Company1 = _configuration.GetSection("TokenizerConfiguration:Company1").Value,
                Company2 = _configuration.GetSection("TokenizerConfiguration:Company2").Value,
                Company3 = _configuration.GetSection("TokenizerConfiguration:Company3").Value,
                Company4 = _configuration.GetSection("TokenizerConfiguration:Company4").Value,
                OperationTypeCC = _configuration.GetSection("TokenizerConfiguration:OperationTypeCC").Value,
                OperationTypeToken = _configuration.GetSection("TokenizerConfiguration:OperationTypeToken").Value,
                ServiceUrl = _configuration.GetSection("TokenizerConfiguration:ServiceUrl").Value,
                ServicePort = Convert.ToInt32(_configuration.GetSection("TokenizerConfiguration:ServicePort").Value)
            };
        }
        #endregion


        public static Guid? ConvertToNullableGuid(string theValue)
        {
            if (string.IsNullOrWhiteSpace(theValue))
            {
                return null;
            }

            Guid val;
            if (Guid.TryParse(theValue, out val))
            {
                return val;
            }
            return null;
        }

        public string GenerateCardNumberUsingToken(string Token)
        {
            return TokenizerProcess.GetCardFromTokenTS(Token, GetConfigurationModel()); ;
        }

        public void ValidateAndAddCustomFields(Process process, Hashtable request)
        {
            throw new NotImplementedException();
        }
    }
}
