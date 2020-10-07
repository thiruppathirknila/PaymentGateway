using Core.Gateway.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateway.Helper
{
    public static class ValidateExtensions
    {
        public static bool ValidateIpAddress(this string val, ErrorModel errorModel)
        {
            if (string.IsNullOrWhiteSpace(val))
            {
                if (errorModel.errors != null)
                {
                    errorModel.errors.Add("Client Ip address is not assigned");
                }
                return false;
            }
            if (ValidationHelper.IsIPv4Address(val))
            {
                return true;
            }
            if (errorModel.errors != null)
            {
                errorModel.errors.Add(string.Format("Client Ip address is not valid {0}", val));
            }
            return false;
        }

        /// <summary>
        /// Is credit card expiration month between 1 - 12?
        /// </summary>
        /// <param name="val"></param>
        /// <param name="validationMsg"></param>
        /// <returns></returns>
        public static bool ValidateCreditCardExpMonth(string val, ErrorModel errorModel)
        {
            var validationFailedMsgList = new List<ValidationFailedMsg>();

            if (ValidationHelper.IsEmpty(val))
            {
                if (errorModel.validationFailedMsg != null)
                {
                    validationFailedMsgList.Add(new ValidationFailedMsg()
                    {
                        Key = "cardExpMonth",
                        Message = string.Format("Credit Card expiration month is required")
                    });
                }
                return false;
            }

            if (val.Length > 2)
            {
                if (errorModel.validationFailedMsg != null)
                {
                    validationFailedMsgList.Add(new ValidationFailedMsg()
                    {
                        Key = "cardExpMonth",
                        Message = string.Format("Credit Card month must be between 1 and 12")
                    });
                }
                return false;
            }

            if (ValidationHelper.IsWholeNumber(val))
            {
                try
                {
                    int numVal = int.Parse(val);
                    if (numVal > 0 && numVal < 13)
                    {
                        return true;
                    }
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch (Exception)
                {
                    //Ignore
                }
            }
            if (errorModel.validationFailedMsg != null)
            {
                validationFailedMsgList.Add(new ValidationFailedMsg()
                {
                    Key = "cardExpMonth",
                    Message = string.Format("Credit Card month must be between 1 and 12")
                });
            }
            return false;
        }

        public static bool ValidateCreditCardExpYear(string val, ErrorModel errorModel)
        {
            var validationFailedMsgList = new List<ValidationFailedMsg>();

            var currentYear = Convert.ToInt32(DateTime.Now.ToString("yy"));

            if (ValidationHelper.IsEmpty(val))
            {
                if (errorModel.validationFailedMsg != null)
                {
                    validationFailedMsgList.Add(new ValidationFailedMsg()
                    {
                        Key = "cardExpYear",
                        Message = string.Format("Credit Card expiration year is required and must be a 2 digit number")
                    });
                }
                return false;
            }

            if (Convert.ToInt32(val) < currentYear || val.Length > 2)
            {
                if (errorModel.validationFailedMsg != null)
                {

                    validationFailedMsgList.Add(new ValidationFailedMsg()
                    {
                        Key = "cardExpMonth",
                        Message = $"Credit Card expiration year must be a 2 digit number(YY) and >= { currentYear }"
                    });

                }
                return false;
            }

            if (ValidationHelper.IsWholeNumber(val))
            {
                try
                {
                    int numVal = int.Parse(val);
                    if (numVal > 00 && numVal < 100)
                    {
                        return true;
                    }
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch (Exception)
                {
                    //Ignore
                }
            }
            if (errorModel.validationFailedMsg != null)
            {
                validationFailedMsgList.Add(new ValidationFailedMsg()
                {
                    Key = "cardExpMonth",
                    Message = "Credit Card year must be between 00 and 99"
                });
            }
            return false;
        }
    }
}
