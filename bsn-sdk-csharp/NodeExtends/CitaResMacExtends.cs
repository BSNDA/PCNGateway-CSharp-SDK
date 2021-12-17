using bsn_sdk_csharp.Models;
using System.Text;

namespace bsn_sdk_csharp.NodeExtends
{
    public class CitaResMacExtends
    {
        public static string GetResHeaderMac(ResHeader header)
        {
            StringBuilder strRes = new StringBuilder();
            strRes.Append(header.code)
                      .Append(header.msg);
            return strRes.ToString();
        }
        /// <summary>
        /// get character string of user registration to sign verification
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetRegisterUserResMac(NodeApiResBody<CitaRegisterUserResBody> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.UserId)
                      .Append(res.body.UserAddress);
            return strRes.ToString();
        }
        /// <summary>
        /// get character string of get the block heioght to sign verification
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetBlockHeightResMac(NodeApiResBody<CitaBlockHeightResBody> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.Data);
            return strRes.ToString();
        }
        /// <summary>
        /// get character string of to get the block infomation to sign verification
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetCitaBlockInfoResMac(NodeApiResBody<CitaBlockData> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.BlockHash)
                      .Append(res.body.BlockNumber)
                      .Append(res.body.PrevBlockHash)
                      .Append(res.body.BlockTime)
                      .Append(res.body.QuotaUsed)
                      .Append(res.body.TransactionsRoot)
                      .Append(res.body.StateRoot)
                      .Append(res.body.ReceiptsRoot);
            if (res.body.Transactions != null && res.body.Transactions.Count > 0)
            {
                string transMac = string.Empty;
                foreach (CitaTransactionData t in res.body.Transactions)
                {
                    transMac += t.TxHash;
                    transMac += t.Data;
                    transMac += t.ChainId;
                    transMac += t.Quota;
                    transMac += t.From;
                    transMac += t.To;
                    transMac += t.Nonce;
                    transMac += t.ValidUntilBlock;
                    transMac += t.Version;
                }
                strRes.Append(transMac);
            }
            return strRes.ToString();
        }
        /// <summary>
        /// get character string of to get the transaction infomation to sign verification
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetCitaTxInfoResMac(NodeApiResBody<CitaTransactionData> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.TxHash)
                      .Append(res.body.Data)
                      .Append(res.body.ChainId)
                      .Append(res.body.Quota)
                      .Append(res.body.From)
                      .Append(res.body.To)
                      .Append(res.body.Nonce)
                      .Append(res.body.ValidUntilBlock)
                      .Append(res.body.Version);
            return strRes.ToString();
        }
        /// <summary>
        /// get character string of to get the transaction receipt to sign verification
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetCitaTxReceiptResMac(NodeApiResBody<CitaTxReceiptResBody> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.TransactionHash)
                      .Append(res.body.TransactionIndex)
                      .Append(res.body.BlockHash)
                      .Append(res.body.BlockNumber)
                      .Append(res.body.CumulativeGasUsed)
                      .Append(res.body.CumulativeQuotaUsed)
                      .Append(res.body.GasUsed)
                      .Append(res.body.QuotaUsed)
                      .Append(res.body.ContractAddress)
                      .Append(res.body.Root)
                      .Append(res.body.Status)
                      .Append(res.body.From)
                      .Append(res.body.To)
                      .Append(res.body.LogsBloom)
                      .Append(res.body.ErrorMessage)
                      .Append(res.body.TransactionIndexRaw)
                      .Append(res.body.BlockNumberRaw)
                      .Append(res.body.CumulativeGasUsedRaw)
                      .Append(res.body.CumulativeQuotaUsedRaw)
                      .Append(res.body.GasUsedRaw)
                      .Append(res.body.QuotaUsedRaw);
            if (res.body.Logs != null && res.body.Logs.Count > 0)
            {
                string transMac = string.Empty;
                foreach (CitaTranReceiptLogData t in res.body.Logs)
                {
                    transMac += t.Removed;
                    transMac += t.LogIndex;
                    transMac += t.TransactionIndex;
                    transMac += t.TransactionHash;
                    transMac += t.BlockHash;
                    transMac += t.BlockNumber;
                    transMac += t.Address;
                    transMac += t.Data;
                    transMac += t.TransactionLogIndex;
                    transMac += t.TransactionIndexRaw;
                    transMac += t.BlockNumberRaw;
                    transMac += t.LogIndexRaw;
                    if (t.Topics != null && t.Topics.Count > 0)
                    {
                        foreach (string ts in t.Topics)
                        { transMac += ts; }
                    }
                }
                strRes.Append(transMac);
            }
            return strRes.ToString();
        }
        /// <summary>
        /// character string to sign verification of transaction processing under key trust mode
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetCitaTransactionResMac(NodeApiResBody<CitaTransResBody> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.TxId)
                      .Append(res.body.Status)
                      .Append(res.body.Data);
            return strRes.ToString();
        }
        /// <summary>
        /// character string to sign verification to get the event registered
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetCitaEventRegisterResMac(NodeApiResBody<CitaRegisterEventResData> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.EventId);
            return strRes.ToString();
        }
        /// <summary>
        /// character string to sign verification to get the event removed
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetCitaQueryEventResMac(NodeApiResBody<CitaQueryEventResData> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header));

            if (res.body.BlockEvent != null && res.body.BlockEvent.Count > 0)
            {
                string eventMac = string.Empty;
                foreach (Event t in res.body.BlockEvent)
                {
                    eventMac += t.EventId;
                    eventMac += t.AppCode;
                    eventMac += t.UserCode;
                    eventMac += t.NotifyUrl;
                    eventMac += t.AttachArgs;
                    eventMac += t.CreateTime;
                }
                strRes.Append(eventMac);
            }
            if (res.body.ContractEvent != null && res.body.ContractEvent.Count > 0)
            {
                string eventMac = string.Empty;
                foreach (ContractEvent t in res.body.ContractEvent)
                {
                    eventMac += t.EventId;
                    eventMac += t.AppCode;
                    eventMac += t.UserCode;
                    eventMac += t.NotifyUrl;
                    eventMac += t.AttachArgs;
                    eventMac += t.CreateTime;
                    eventMac += t.ContractAddress;
                }
                strRes.Append(eventMac);
            }
            return strRes.ToString();
        }
    }
}