using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Model.Models
{
    public class GatewayConstants
    {
        /*  ------------------------------------------------------------------------
         * 
         *               Used to tell trace message which fn in
         * 
         * -------------------------------------------------------------------------*/
        public const string CALLED_FROM_AUTH = "Gateway::Auth";
        public const string CALLED_FROM_AUTH_USING_VAULT = "Gateway::AuthUsingVault";
        public const string CALLED_FROM_AVS = "Gateway::Avs";
        public const string CALLED_FROM_AVS_USING_VAULT = "Gateway::AvsUsingVault";
        public const string CALLED_FROM_CLOSE_BATCH = "Gateway::CloseBatch";
        public const string CALLED_FROM_CREDIT = "Gateway::Credit";
        public const string CALLED_FROM_VOID = "Gateway::Void";
        public const string CALLED_FROM_PARTIAL_VOID = "Gateway::PartialVoid";
        public const string CALLED_FROM_ACH_VOID = "Gateway::AchVoid";

        public const string CALLED_FROM_SETTLE = "Gateway::Settle";
        public const string CALLED_FROM_TIP_ADJUST = "Gateway::TipAdjust";
        public const string CALLED_FROM_RE_AUTH = "Gateway::ReAuth";
        public const string CALLED_FROM_RE_DEBIT = "Gateway::ReDebit";
        public const string CALLED_FROM_RE_SALE = "Gateway::ReSale";
        public const string CALLED_FROM_CREDIT_RETAIL_ALONE = "Gateway::CreditRetailAlone";
        public const string CALLED_FROM_CREDIT_RETAIL_ALONE_USING_VAULT = "Gateway::CreditRetailAloneUsingVault";
        public const string CALLED_FROM_QUERY = "Gateway::Query";
        public const string CALLED_FROM_SALE = "Gateway::Sale";
        public const string CALLED_FROM_SALE_USING_VAULT = "Gateway::SaleUsingVault";
        public const string CALLED_FROM_PROCESSOR_HELPER = "Gateway::ProcessorHelper";

        public const string CALLED_FROM_RECURRING_MODIFY = "Gateway::RecurringModify";

        public const string CALLED_FROM_ACCOUNT_UPDATER_SUBMIT = "Gateway::AccountUpdaterSubmit";
        public const string CALLED_FROM_ACCOUNT_UPDATER_SUBMIT_VAULT = "Gateway::AccountUpdaterSubmitVault";
        public const string CALLED_FROM_ACCOUNT_UPDATER_RETURN = "Gateway::AccountUpdaterReturn";

        public const string CALLED_FROM_CC_CHARGEBACK_QUERY = "Gateway::CCChargebackQuery";
        public const string CALLED_FROM_QUERY_BATCH_SUMMARY = "Gateway::QueryBatchSummary";

        /**************************************************************************************
         *                          ACH METHOD CALLS
         **************************************************************************************/
        public const string CALLED_FROM_ACH_CREDIT = "Gateway::AchCredit";
        public const string CALLED_FROM_ACH_DEBIT = "Gateway::AchDebit";
        public const string CALLED_FROM_ACH_CREDIT_USING_VAULT = "Gateway::AchCreditUsingVault";
        public const string CALLED_FROM_ACH_DEBIT_USING_VAULT = "Gateway::AchDebitUsingVault";
        public const string CALLED_FROM_ACH_GET_CATEGORIES = "Gateway::AchGetCategories";
        public const string CALLED_FROM_ACH_CREATE_CATEGORY = "Gateway::AchCreateCategory";
        public const string CALLED_FROM_ACH_UPDATE_CATEGORY = "Gateway::AchUpdateCategory";
        public const string CALLED_FROM_ACH_DELETE_CATEGORY = "Gateway::AchDeleteCategory";
        public const string CALLED_FROM_ACH_SETUP_STORE = "Gateway::AchSetupStore";

        /**************************************************************************************
         *                          Vault METHOD CALLS
         **************************************************************************************/
        public const string CALLED_FROM_VAULT_CREATE_VAULT = "Gateway::VaultCreate";
        public const string CALLED_FROM_VAULT_DELETE_VAULT_AND_ALL_ASSC_DATA = "Gateway::VaultDeleteContainerAndAllAsscData";

        public const string CALLED_FROM_VAULT_CREATE_ACH = "Gateway::VaultCreateAch";
        public const string CALLED_FROM_VAULT_CREATE_CC = "Gateway::VaultCreateCreditCard";
        public const string CALLED_FROM_VAULT_CREATE_SHIPPING = "Gateway::VaultCreateShippingAdr";

        public const string CALLED_FROM_VAULT_DELETE_ACH = "Gateway::VaultDeleteAch";
        public const string CALLED_FROM_VAULT_DELETE_CC = "Gateway::VaultDeleteCreditCard";
        public const string CALLED_FROM_VAULT_DELETE_SHIPPING = "Gateway::VaultDeleteShippingAdr";

        public const string CALLED_FROM_VAULT_UPDATE_VAULT = "Gateway::VaultUpdateContainer";
        public const string CALLED_FROM_VAULT_UPDATE_ACH = "Gateway::VaultUpdateAch";
        public const string CALLED_FROM_VAULT_UPDATE_CC = "Gateway::VaultUpdateCreditCard";
        public const string CALLED_FROM_VAULT_UPDATE_SHIPPING = "Gateway::VaultUpdateShippingAdr";


        public const string CALLED_FROM_VAULT_QUERY_VAULT = "Gateway::VaultQueryVault";
        public const string CALLED_FROM_VAULT_QUERY_CC = "Gateway::VaultQueryCreditCard";
        public const string CALLED_FROM_VAULT_QUERY_ACH = "Gateway::VaultQueryAch";
        public const string CALLED_FROM_VAULT_QUERY_SHIPPING_ADDR = "Gateway::VaultQueryShippingAddr";

        public const string CALLED_FROM_FEE = "Gateway::ConvFee";


        /**************************************************************************************
         *                          Token METHOD CALLS
         **************************************************************************************/
        public const string CALLED_FROM_CREDIT_CARD_TO_CRYPTOGRAM = "Gateway::GenerateCryptogramFromCreditCard";
        public const string CALLED_FROM_CRYPTOGRAM_TO_CREDIT_CARD = "Gateway::GetCreditCardFromCryptogram";
        public const string CALLED_FROM_CRYPTOGRAM_TO_ACH = "Gateway::GetAchDataFromCryptogram";
        public const string CALLED_FROM_TOKEN_FOR_TRANSACTION = "Gateway::GenerateTokenForTransaction";
        public const string CALLED_FROM_CREDIT_CARD_TO_TOKEN = "Gateway::GenerateTokenFromCreditCard";
        public const string CALLED_FROM_CRYPTOGRAM_LOOKUP = "Gateway::GetPaymentDataFromCryptogram";

        /**************************************************************************************
         *                           fee METHOD CALLS
         **************************************************************************************/
        public const string CALLED_FROM_FEELOOKUP = "Gateway::Fee:Lookup";

       public string CalledFromCreditCardToCryptogram { get { return CALLED_FROM_CREDIT_CARD_TO_CRYPTOGRAM; } }
         public string CalledFromTokeToCreditCard { get { return CALLED_FROM_CRYPTOGRAM_TO_CREDIT_CARD; } }

        /**************************************************************************************
        *                          INTERNAL METHOD CALLS
        **************************************************************************************/
        public const string CALLED_FROM_MASTERPASS_AUTH = "Gateway::MasterPassAuth";


        /*****************************************************************
         *  Used by other fields as part of Gateway calls on Process
         *****************************************************************/

        /// <summary>
        /// The last order id used for a transaction that only provided partial payment.
        /// </summary>
        public const string OTHER_FIELD_KEY_PARTIAL_ORDER_ID = "partial_id";
        /// <summary>
        /// Was a magnetic card used to retrieve credit card info? 0 = no / 1 = yes
        /// </summary>
        public const string OTHER_FIELD_KEY_IS_SWIPED = "is_swiped";

        public const string OTHER_FIELD_KEY_MAG_DATA_ENC_TRACK_1 = "EncTrack1";
        public const string OTHER_FIELD_KEY_MAG_DATA_ENC_TRACK_2 = "EncTrack2";
        public const string OTHER_FIELD_KEY_MAG_KNS = "KSN";
        public const string OTHER_FIELD_KEY_MAG_HSM_FUTURE_X = "HSMFuturex";
        public const string OTHER_FIELD_KEY_MAG_ENC_MP = "EncMP";
        public const string OTHER_FIELD_KEY_MAG_ENC_STATUS = "EncStatus";
        public const string OTHER_FIELD_KEY_MAG_ENCRYPTION_BLOCK_TYPE = "EncryptionBlockType";
        public const string OTHER_FIELD_KEY_MAG_CONTACTLESS = "contactless";
        public const string OTHER_FIELD_KEY_MAG_DATA_IATS = "iats_swipe_data";


        public const string OTHER_FIELD_KEY_RECURRING = "processor_recurring";
        public const string OTHER_FIELD_KEY_RECURRING_IATS_SCHEDULE_DATE = "iats_recurring_schedule_date"; // If this is not passed iats will not enable recurring for the transaction
        public const string OTHER_FIELD_KEY_RECURRING_IATS_TYPE = "iats_recurring_type";
        public const string OTHER_FIELD_KEY_RECURRING_START_DATE = "recurring_start_date";
        public const string OTHER_FIELD_KEY_RECURRING_END_DATE = "recurring_end_date";

        public const string OTHER_FIELD_KEY_PREVENT_PARTIAL = "prevent_partial";


        /**************************************************************************************
        *                          MISC SERVICE CONSTANTS 
        **************************************************************************************/
        /// <summary>
        /// Tell payment service who called
        /// </summary>
        public const string SCRIPT = "RestGateway";

        // Just for convenience so we don't have to remember
        public const string JSON_CONTENT_TYPE = "application/json";

    }
}
