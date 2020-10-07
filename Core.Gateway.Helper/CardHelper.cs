using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Gateway.Helper
{
    public class CardHelper
    {
        // CONST
        public const string CARDTYPE_UNKNOWN = "Unknown";

        // Common regular expressions used to evaluate card data
        private static Regex visaPattern = new Regex("^4[0-9]{6,}$", RegexOptions.Compiled);
        private static Regex mastercardPattern = new Regex("^5[1-5][0-9]{5,}|^222[1-9][0-9]{3,}|^22[3-9][0-9]{4,}|^2[3-6][0-9]{5,}|^27[01][0-9]{4,}|^2720[0-9]{3,}$", RegexOptions.Compiled);
        private static Regex maestroPattern = new Regex("^(?:5[0678][0-9][0-9]|6304|6390|67[0-9][0-9])[0-9]{8,15}$", RegexOptions.Compiled);
        private static Regex amexPattern = new Regex("^3[47][0-9]{5,}$", RegexOptions.Compiled);
        private static Regex discoverPattern = new Regex("^[68](?:011|5[0-9]{2}|[1][013567])[0-9]{3,}$", RegexOptions.Compiled);
        private static Regex dinersPattern = new Regex("^3(?:0[0-5]|[68][0-9])[0-9]{4,}$", RegexOptions.Compiled);
        private static Regex jcbPattern = new Regex("(?:2131|1800|35[0-9]{3})[0-9]{11}", RegexOptions.Compiled);

        // Secondary regular expressions to evaluate card data with more relaxed rules, to be used as a fallback
        private static Regex visaFallbackPattern = new Regex("^4[0-9]{1,}$", RegexOptions.Compiled);
        private static Regex mastercardFallbackPattern = new Regex("^[25][0-9]{1,}$", RegexOptions.Compiled);
        private static Regex amexFallbackPattern = new Regex("^3[0-9]{1,}$", RegexOptions.Compiled);
        private static Regex discoverFallbackPattern = new Regex("^[68][0-9]{1,}$", RegexOptions.Compiled);


        /// <summary>
        /// Given a card number, check if it matches length and formatting requirements for a Visa credit/debit card.
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static bool IsVisa(string cardNumber)
        {
            return (visaPattern.IsMatch(cardNumber) && (cardNumber?.Length == 13 || cardNumber?.Length == 16 || cardNumber?.Length == 19));
        }

        /// <summary>
        /// Given a card number, check if it matches length and formatting requirements for a MasterCard credit/debit card.
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static bool IsMasterCard(string cardNumber)
        {
            return (mastercardPattern.IsMatch(cardNumber) && (cardNumber?.Length == 16));
        }

        /// <summary>
        /// Given a card number, check if it matches length and formatting requirements for an American Express credit/debit card.
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static bool IsAmex(string cardNumber)
        {
            return (amexPattern.IsMatch(cardNumber) && (cardNumber?.Length == 15));
        }

        /// <summary>
        /// Given a card number, check if it matches length and formatting requirements for a Discover credit/debit card.
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static bool IsDiscover(string cardNumber)
        {
            return (discoverPattern.IsMatch(cardNumber) && (cardNumber?.Length == 16 || cardNumber?.Length == 19));
        }

        /// <summary>
        /// Given a card number, check if it matches length and formatting requirements for a Diners club credit/debit card.
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static bool IsDiners(string cardNumber)
        {
            return (dinersPattern.IsMatch(cardNumber) && (cardNumber?.Length == 14));
        }

        /// <summary>
        /// Given a card number, check if it matches length and formatting requirements for a JCB credit/debit card.
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static bool IsJCB(string cardNumber)
        {
            return (jcbPattern.IsMatch(cardNumber));
        }

        /// <summary>
        /// Checks whether or not a card (number) is from the Maestro brand.
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns>True, if the card number matches the regex pattern.</returns>
        /// <remarks>Currently, the regex pattern does not match a number like `0604824311989290` which is, apparently, a valid number. This will need to have further investigation.</remarks>
        public static bool IsMaestro(string cardNumber)
        {
            return (maestroPattern.IsMatch(cardNumber));
        }

    }
}
