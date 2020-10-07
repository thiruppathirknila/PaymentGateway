using Core.Gateway.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Core.Payment.Tokenizer.Helper
{
    public class SocketTokenizerClient
    {
        // used to build STX request messages
        private const char End = (char)0x03;
        private const char Separator = (char)0x1C;
        private const char Start = (char)0x02;

        /// <summary>
        /// This method is used to generate or get a token based on the pan/card input.
        /// </summary>
        /// <param name="pan">primary account number to tokenize.</param>
        /// <returns>returns a string as token.</returns>
        public static string GetTokenfromCCRequest(string pan, ConfigurationModel configuration)
        {
            var request = $"{Start}{Separator}{configuration.Company1}{Separator}2{Separator}{pan}{Separator}{Separator}{Separator}{End}\n";
            return SendSingleRequest(request, configuration);
        }

        /// <summary>
        /// This method is used to retrieve a pan/card based on a token input. 
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>returns a string as a pan/card.</returns>
        public static string GetCCfromTokenRequest(string token, ConfigurationModel configuration)
        {
            var request = $"{Start}{Separator}{configuration.Company1}{Separator}20{Separator}{token}{Separator}{Separator}{Separator}{End}\n";
            return SendSingleRequest(request, configuration);
        }

        /// <summary>
        /// This method is used to send single requests.
        /// </summary>
        /// <param name="input">Handles single requests.</param>
        /// <returns></returns>
        public static string SendSingleRequest(string input, ConfigurationModel configuration)
        {
            return SendToSocket<int>(input, configuration);
        }

        /// <summary>
        /// This process is used to send request(STX or JSON) to netEndPoint.
        /// </summary>
        /// <typeparam name="T">if int then send STX request; if string then send JSON request.</typeparam>
        /// <param name="requestMessage"></param>
        /// <returns>returns a string.</returns>
        private static string SendToSocket<T>(string requestMessage, ConfigurationModel configuration)
        {
            Type itemType = typeof(T);
            var responseMessage = string.Empty;
            try
            {
                IPAddress socketIp = Dns.GetHostEntry(configuration.ServiceUrl).AddressList[0];
                IPEndPoint netEndpoint = new IPEndPoint(socketIp, Convert.ToInt16(configuration.ServicePort));
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect(netEndpoint);
                    if (socket.Connected)
                    {
                        // We should not sit on requests, so if it takes longer than 5 seconds consider it timed out
                        socket.SendTimeout = 5000;
                        socket.ReceiveTimeout = 5000;

                        if (itemType == typeof(int))
                        {
                            var bytes = new byte[2048];
                            var encodedMessage = Encoding.ASCII.GetBytes(requestMessage);
                            socket.Send(encodedMessage);
                            var bytesReceived = socket.Receive(bytes);
                            var results = Encoding.ASCII.GetString(bytes, 0, bytesReceived);
                            var message = results.Split(Separator);
                            responseMessage = message[2];
                        }
                        else if (typeof(T) == typeof(string))
                        {
                            byte[] encodedMessage = Encoding.ASCII.GetBytes($"{requestMessage}{(char)10}"); // ensuring we add a line feed w/ ascii code
                            socket.Send(encodedMessage);
                            byte[] encodedResponse = new byte[socket.ReceiveBufferSize];
                            socket.Receive(encodedResponse);
                            responseMessage = Encoding.ASCII.GetString(encodedResponse);
                            responseMessage = responseMessage.Substring(0, responseMessage.IndexOf("\n"));
                        }
                    }
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
            catch (Exception socketEx)
            {
                //responseMessage = $"Tokenizer Socket Request Failed: {socketEx.Message}.";
                throw;
            }
            return responseMessage;
        }
    }
}
