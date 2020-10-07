using Core.Gateway.Models;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Core.Gateway.Interfaces
{
    public interface IPaymentService
    {
        Task<MerchantInfoResult> GetMerchantInfo(ErrorModel errorModel,String MerchantKey, String IpAddress, Nullable<int> processorId);

        Task<CryptogramLookupResultData> GetCreditCardFromCryptogram(PaymentDataFromCryptogramInput PaymentDataFromCryptogramInput);

        string GetTokenFromCreditCard(string CardNumber);

        string GetCreditCardNumberFromToken(string CreditCardToken);

        Task<Process> GetUpdatedCardDetails(Process process);

        Task<Process> ValidateInitAndBuildProcessObject(Request request,MerchantInfoResult merchantInfoResult, ErrorModel errorModel, Process process);
        string GenerateCardNumberUsingToken(string Token);
        void ValidateAndAddCustomFields(Process process, Hashtable request);


    }
}
