using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.Gateway.Models
{
    public class Request
    {
        public MerchantInfo MerchantInfo { get; set; }
        public bool isValid { get; set; }
        public string OrderId { get; set; }
        public bool? OrderIdIsUnique { get; set; }
        public bool? AutoGenerateorderId { get; set; }
        public string PartialOrderId { get; set; }
        public bool? PreventPartial { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal Fee { get; set; }
        public string CreditCardToken { get; set; }
        public string CreditCardCryptogram { get; set; }
        public string CardNumber { get; set; }
        public Int32? CardExpMonth { get; set; }
        public int? CardExpYear { get; set; }
        public string CardType { get; set; }
        public decimal ConvFeeAmount { get; set; }
        public int? CVV { get; set; }
        public bool CVVNotRequired { get; set; }
        public string Ksn { get; set; }
        public string MagData { get; set; }
        public string EncryptionBlockType { get; set; }
        public string SignaturePngImage { get; set; }
        public bool? Contactless { get; set; }
        public string OwnerName { get; set; }
        public string OwnerStreet { get; set; }
        public string OwnerStreet2 { get; set; }
        public string OwnerCity { get; set; }
        public string OwnerState { get; set; }
        public string OwnerZip { get; set; }
        public string OwnerCountry { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerPhone { get; set; }
        public string AuthCode { get; set; }
        public string CheckRecurringStart { get; set; }
        public DateTime CloseDate { get; set; }
        public string Recurring { get; set; }
        public DateTime RecurringStartDate { get; set; }
        public DateTime RecurringEndDate { get; set; }
        public bool PurchaseCard { get; set; }
        public string CustomerRefNo { get; set; }
        public bool LocalTaxFlag { get; set; }
        public decimal TaxAmount { get; set; }
        public string ShippingZip { get; set; }
        public decimal ProcessingFee { get; set; }
        public Hashtable OtherFields { get; set; }
        public List<Level3Item> Level3Items { get; set; }
        public string Industry { get; set; }
        public string CimRefNum { get; set; }
        public string CimSequence { get; set; }
        public bool expireCryptogram { get; set; }
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

    public class MerchantInfo
    {
        public long MerchantId { get; set; }
        public string accountType { get; set; }
        public string TransactionType { get; set; }
        public string MerchantKey { get; set; }
        public long ProcessorId { get; set; }
        public string IpAddress { get; set; }
        public string TraceModeEnabled { get; set; }
        public Hashtable OtherFields { get; set; }
        public string FeIpAddress { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TransactionTypeEnum
    {
        NotSet,
        Auth,
        MasterPassAuth,
        Sale,
        Query,
        Credit,
        CreditRetailOnly,
        Void,
        VoidPartial,
        Settle,
        TipAdjust,
        ReAuth,
        ReSale,
        ReDebit,
        CloseBatch,
        Avs,

        //Ach
        AchCredit,
        AchDebit,
        AchGetCategories,
        AchCreateCategory,
        AchDeleteCategory,
        AchSetupStore,
        AchVoid,

        //Vault
        VaultCreateContainer,
        VaultCreateAchRecord,
        VaultCreateCreditCardRecord,
        VaultCreateShippingRecord,
        VaultDeleteContainerAndAllAsscData,
        VaultDeleteAchRecord,
        VaultDeleteCreditCardRecord,
        VaultDeleteShippingRecord,
        VaultUpdateContainer,
        VaultUpdateAchRecord,
        VaultUpdateCreditCardRecord,
        VaultUpdateShippingRecord,

        VaultQueryVault,
        VaultQueryVaultCreditCard,
        VaultQueryVaultAch,
        VaultQueryVaultShippingAddr,

        //Misc
        GenerateTokenFromCreditCard,
        TokenToCreditCard, //TODO REMOVE THIS-- MISLEADING
        GetCreditCardFromCryptogram,
        GetAchDataFromCryptogram,
        CreditCardToCryptogram,

        //RecurringBilling
        RecurringModify,

        //AccountUpdater
        AccountUpdaterSubmit,
        AccountUpdaterReturn,

        CCChargebackQuery,
        QueryBatchSummary,

        //Conv Fee
        GetFeeForCC,
        GetFeeForACH,
        InitFeeForCC,
        InitFeeForACH,
        Query2
    }

}
