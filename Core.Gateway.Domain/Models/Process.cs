using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Payment.Model.Models
{
    public class Process
    {
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public Int32? ExpMonth { get; set; }
        public Int32? ExpYear { get; set; }
        public string CVV2 { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string Token { get; set; }
        public string RecurringType { get; set; }
        public string Street2 { get; set; }
        public string Street { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Tid { get; set; }
        public string SourceId { get; set; }
        public string Mid { get; set; }
        public string Processor { get; set; }
        public string IP { get; set; }
        public string City { get; set; }
        public double Amount { get; set; }
        public string TransType { get; set; }
        public double SettlementAmount { get; set; }
        public List<Level3Item> Level3Items { get; set; }
        public string PostedBy { get; set; }
        public string CimRefNum { get; set; }
        public string AchAccountType { get; set; }
        public string SortCode { get; set; }
        public string TransitNumber { get; set; }
        public string BankNumber { get; set; }
        public string DDA { get; set; }
        public string AchEntry { get; set; }
        public string AchClassCode { get; set; }
        public double SalesTax { get; set; }
        public string AchCategoryText { get; set; }
        public string ABA { get; set; }
        public string ShippingZip { get; set; }
        public string LocalTaxFlag { get; set; }
        public string CustomerReferenceNumber { get; set; }
        public bool PurchaseCard { get; set; }
        public string MagData { get; set; }
        public string AuthCode { get; set; }
        public string Script { get; set; }
        public string OpType { get; set; }
        public bool Debug { get; set; }
        public string CloseDate { get; set; }
        public string PartialID { get; set; }
        public Hashtable LastResult { get; }
        public string OrderID { get; set; }
        public string Storename { get; set; }       
        public string hostname { get; }
        public bool doTokenization { get; }
        public string ReferenceNumber { get; set; }
    }

    public class Level3Item
    {
        public string Description { get; set; }
        public string Number { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? ItemDiscountAmount { get; set; }
        public decimal? ItemDiscountRate { get; set; }
    }


}
