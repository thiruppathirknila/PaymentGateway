using Core.Gateway.Models;
using Core.Payment.Tokenizer.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Payment.Tokenizer
{
    public class TokenizerProcess
    {
        /// <summary>
        /// Get a token from credit card
        /// </summary>
        /// <param name="card">card value</param>
        /// <returns>Token</returns>
        public static string GetTokenFromCCTS(string card, ConfigurationModel configuration)
        {
            try
            {
                return SocketTokenizerClient.GetTokenfromCCRequest(card, configuration);
            }
            catch (Exception ex)
            {
                throw new Exception("Tokenizer getting token from credit card processing error: " + ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Get a clear pan/card from token
        /// </summary>
        /// <param name="token">token value</param>
        /// <returns>PAN/CreditCard</returns>
        public static string GetCardFromTokenTS(string token, ConfigurationModel configuration)
        {
            try
            {
                return SocketTokenizerClient.GetCCfromTokenRequest(token, configuration);
            }
            catch (Exception ex)
            {
                throw new Exception("Tokenizer getting card from token processing error: " + ex.Message + ex.StackTrace);
            }
        }
    }
}
