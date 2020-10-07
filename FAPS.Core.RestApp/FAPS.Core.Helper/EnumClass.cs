using System;
using System.Collections.Generic;
using System.Text;

namespace FAPS.Core.Helper
{
    //public enum ERequestType { UNKNOWN = 0, SALE = 100, AUTHORIZE = 105, VOID = 110, REFUND = 120, SETTLE = 130, TIPADJUST = 195, CREDIT = 200, DEBIT = 210 }

    public class ERequestType
    {
        public const string SALE = "Sale";
        public const string RESALE = "ReSale";
        public const string AUTHORIZE = "Authorize";
        public const string VOID = "Void";
        public const string REFUND = "Refund";
        public const string SETTLE = "Settle";
        public const string REAUTHORIZE = "ReAuthorize";
        public const string TIPADJUST = "Tip Adjust";
        public const string CLOSEBATCH = "Close Batch";
        public const string CREDIT = "Credit";
        public const string DEBIT = "Debit";
        public const string MODIFYRECURRING = "Modify Recurring";
        public const string GENERATETOKEN = "Generate Token";
        public const string UNKNOWN = "Unknown";
    }



}
