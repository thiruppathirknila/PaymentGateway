using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Service.Helpers
{
    public static class ValidationHelper
    {

        /// <summary>
        /// Does this credit card pass the minium test
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsValidCreditCardNo(string val)
        {
            return Mod10Check(val);
        }

        private static bool Mod10Check(string creditCardNumber)
        {
            //Code copied from gateway.sln cm.aspx.cs ln:1585

            //Add the following test to handle no or invalid data before calculation test gets started.
            //As per Payment webservice card numbers will be at least 13 digits long
            //As per various sources card numbers can be up to 19 digits long (as of 08-2016)
            // ReSharper disable once PossibleNullReferenceException
            if (string.IsNullOrWhiteSpace(creditCardNumber) || creditCardNumber.Trim().Length < 13 || creditCardNumber.Trim().Length > 19)
            {
                return false;
            }

            // ReSharper disable once InconsistentNaming
            char[] card_array = creditCardNumber.ToCharArray();
            int mod10 = 0;
            for (int i = 0; i < card_array.Length; i++)
            {
                // Grad int values in reverse order
                int val = (Convert.ToInt32(card_array[card_array.Length - i - 1]) - 48);
                if (i % 2 == 1)
                    val *= 2;

                if (val > 9)
                    val -= 9;

                mod10 += val;
            }

            return (mod10 % 10) == 0;
        }
    }
}
