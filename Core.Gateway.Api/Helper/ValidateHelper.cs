using Core.Gateway.Models;
using Core.Gateway.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Gateway.Api.Helper
{
    public class ValidateHelper
    {
        public async Task<string> ValidateOrderIdAndGenerateNewOrderIdIfNeededAsync(Request request, MerchantInfoResult merchantInfoResult, ErrorModel errorModel)
        {
            var validationFailedMsgList = new List<ValidationFailedMsg>();
            try
            {

                if (!request.OrderIdIsUnique.HasValue)
                {
                    request.OrderIdIsUnique = false; //Doc indicate default is true if not assigned.
                }

                if (!request.AutoGenerateorderId.HasValue)
                {
                    request.AutoGenerateorderId = true; //Doc indicate default is true if not assigned.
                }

                var orderId = request.OrderId;

                if (string.IsNullOrWhiteSpace(orderId))
                {
                    Logger.InformationLog($"In PaymentService.GetCreditCardFromCryptogram, No orderId assigned");

                    //Use case 1
                    if (request.AutoGenerateorderId == false)
                    {
                        Logger.InformationLog($"In PaymentService.GetCreditCardFromCryptogram, Failed orderId test. Order ID is required");
                        //If the user sets this to false it is assumed the web page or clients code behind is setting this.

                        validationFailedMsgList.Add(new ValidationFailedMsg()
                        {
                            Key = "orderId",
                            Message = string.Format("{0} is required.", "Order Id")
                        });
                    }
                    else
                    {
                        //Auto generate an Order ID

                        orderId = GenerateNewOrderId();

                        if (request.OrderIdIsUnique.Value)
                        {
                            //Okay lets see if the newOrderId I created is unique!

                            //var orderIdIsUnique = ;

                            //if (!orderIdIsUnique)
                            //{
                            //    traceMsg.AppendFormat("Order Id '{0}' is not unique\nRetry, generating new Order Id",
                            //        orderId);
                            //    //Lets assume heavy volume of work, try one more time
                            //    Thread.Sleep(1000); //Release control.
                            //    orderId = PaymentHelper.GenerateNewOrderId();
                            //    orderIdIsUnique = GatewayHelper.OrderIdIsUnique(info, orderId, calledFrom);
                            //}
                            //if (!orderIdIsUnique)
                            //{
                            //    traceMsg.AppendFormat("Order Id '{0}' is still not unique\nCanceling generation",
                            //        orderId);
                            //    //Okay we should have had success by now!
                            //    errors.Add(resx.Error_UnableToCreateUniqueOrderId);
                            //}
                            //else
                            //{
                            //    traceMsg.AppendFormat("Order Id '{0}' is unique\n", orderId);
                            //}
                        }
                    }
                }



                return "1245";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      public static string GenerateNewOrderId()
        {
            string newOrderId = string.Format("{0}", DateTime.Now.ToUniversalTime().Ticks);
            return newOrderId;
        }
    }
}
