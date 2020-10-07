using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Gateway.Helper
{
    public static class ValidationHelper
    {

        private static readonly Regex IsIPv4AddressRegex = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$", RegexOptions.Compiled);
        private static readonly Regex IsIPv6AddressRegex = new Regex(@"^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*", RegexOptions.Compiled);
        private static readonly Regex IsEmptyRegex = new Regex(@"^[\s\t\r\n]*\S+", RegexOptions.Compiled);
        private static readonly Regex IsWholeNumberRegex = new Regex(@"^-?\d+$", RegexOptions.Compiled);
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
            if (string.IsNullOrEmpty(creditCardNumber) || creditCardNumber.Trim().Length < 13 || creditCardNumber.Trim().Length > 19)
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

        #region IP

        /// <summary>
        /// Is this a valid IP 4 internet address?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsIPv4Address(string val)
        {
            return IsIPv4AddressRegex.IsMatch(val);
        }

        /// <summary>
        /// Is this a valid IP 6 internet address?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsIPv6Address(string val)
        {
            return IsIPv6AddressRegex.IsMatch(val);
        }

        #endregion

        /// <summary>
        /// Is this string empty contains no white space characters Regex version?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsEmpty(string val)
        {
            return val == null || !IsEmptyRegex.IsMatch(val);
        }

        /// <summary>
        /// Just numbers only
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsWholeNumber(string val)
        {
            if (IsEmpty(val))
            {
                return true;
            }

            return IsWholeNumberRegex.IsMatch(val);
        }
    }
}
