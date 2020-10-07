using Core.Gateway.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateway.Helper
{
    public static class CreditCardHelper
    {
        /// <summary>
        /// Return the issuer for the credit card listed
        /// </summary>
        /// <param name="ccNumber"></param>
        /// <returns></returns>
        public static string GetCreditCardType(string ccNumber)
        {
            if (ValidationHelper.IsValidCreditCardNo(ccNumber))
            {
                if (CardHelper.IsVisa(ccNumber))
                {
                    return CreditCardTypeEnums.Visa.ToString();
                }

                if (CardHelper.IsAmex(ccNumber))
                {
                    return CreditCardTypeEnums.Amex.ToString();
                }

                if (CardHelper.IsDiscover(ccNumber) || CardHelper.IsDiners(ccNumber) || CardHelper.IsJCB(ccNumber))
                {
                    return CreditCardTypeEnums.Discover.ToString();
                }

                if (CardHelper.IsMasterCard(ccNumber))
                {
                    return CreditCardTypeEnums.MasterCard.ToString();
                }

                return CreditCardTypeEnums.NotSet.ToString();
            }

            return string.Empty;
        }
    }
}
