using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateway.Data.Queries
{
    public class CommandText :ICommandText
    {
   
        public string GetMerchantInfo => "dbo.gw_getMerchantInfo";

        public  string GetUpdatedCardDetails => "dbo.uspGetUpdatedCardDetails";

        public  string MerchantConfigWithMidTid => "SELECT mc.*, mt.id as midTidId, mt.ContactlessMSDEnabled from paymentech.dbo.merchant_config as mc inner join transcenter.dbo.config as c on c.storename = mc.storename inner join transcenter.dbo.mid_tid as mt on c.merchant_id = mt.merchant_id " +
                                                        " and mc.id = mt.merchant_config_id where mc.storename = '{0}' and mc.merchant_number = {1} and mc.terminal_number = {2}";
        public  string MerchantConfigWithAccountType => "SELECT mc.*, mt.id as midTidId, mt.ContactlessMSDEnabled from merchant_config as mc inner join transcenter.dbo.config as c on c.storename = mc.storename inner join transcenter.dbo.mid_tid as mt on c.merchant_id = mt.merchant_id " +
                                                            "and mc.id = mt.merchant_config_id where mc.storename = @StoreName and mt.disabled = 0 and mt.account_type = @AccountType and mt.primary_account = '1' and mt.processor = @Processor";
        public  string MerchantConfigAccTypeMoto => "SELECT mt.account_type as actual_account_type, mt.id as midTidId, mt.ContactlessMSDEnabled, mc.* from merchant_config as mc inner join transcenter.dbo.config as c on c.storename = mc.storename inner join transcenter.dbo.mid_tid as mt on c.merchant_id = mt.merchant_id " +
                                                        "and mc.id = mt.merchant_config_id where mc.storename =@StoreName  and mt.disabled = 0 and mt.account_type = 'moto' and mt.primary_account = '1' and mt.processor = @processor";
        public  string MerchantConfigAccTypeRetail => "SELECT mt.account_type as actual_account_type, mt.id as midTidId, mt.ContactlessMSDEnabled, mc.* from paymentech.dbo.merchant_config as mc inner join transcenter.dbo.config as c on c.storename = mc.storename inner join transcenter.dbo.mid_tid as mt on c.merchant_id = mt.merchant_id " +
                                                        "and mc.id = mt.merchant_config_id where mc.storename = @StoreName and mt.disabled = 0 and mt.account_type = 'retail' and mt.primary_account = '1' and mt.processor =@Processor ";
        public string GetOrderIdFoundCount => "dbo.gw_getOrderIdFoundCount";

        public string GetErrorValue => "SELECT EM.Id,EM.MessageCode,EM.MessageKey,EM.MessageValue from ErrorMessage EM WHERE EM.MessageCode= @MessageCode";
    }
}
