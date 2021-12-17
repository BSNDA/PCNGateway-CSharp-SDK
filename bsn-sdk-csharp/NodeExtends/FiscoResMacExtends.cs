using bsn_sdk_csharp.Models;
using System.Text;

namespace bsn_sdk_csharp.NodeExtends
{
    public class FiscoResMacExtends
    {
        public static string GetResHeaderMac(ResHeader header)
        {
            StringBuilder strRes = new StringBuilder();
            strRes.Append(header.code)
                      .Append(header.msg);
            return strRes.ToString();
        }
        /// <summary>
        /// get character string of get the block heioght to sign verification
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetBlockHeightResMac(NodeApiResBody<GetBlockHeightResBody> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.Data);
            return strRes.ToString();
        }
        /// <summary>
        /// character string to sign verification of transaction processing 
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetFiscoTransactionResMac(NodeApiResBody<FiscoTransResBody> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.Constant.ToString().ToLower())
                      .Append(res.body.QueryInfo)
                      .Append(res.body.TxId)
                      .Append(res.body.BlockHash)
                      .Append(res.body.BlockNumber)
                      .Append(res.body.GasUsed)
                      .Append(res.body.Status)
                      .Append(res.body.From)
                      .Append(res.body.To)
                      .Append(res.body.Input)
                      .Append(res.body.Output)
                      .Append(res.body.Logs);
            return strRes.ToString();
        }
        /// <summary>
        /// get character string of user registration to sign verification
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetRegisterUserResMac(NodeApiResBody<FiscoRegisterUserResBody> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.UserId)
                      .Append(res.body.UserAddress);
            return strRes.ToString();
        }
        /// <summary>
        /// get character string of to get the transaction receipt to sign verification
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetFiscoTxReceiptResMac(NodeApiResBody<FiscoTxReceiptResBody> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.TxId)
                      .Append(res.body.BlockHash)
                      .Append(res.body.BlockNumber)
                      .Append(res.body.GasUsed)
                      .Append(res.body.From)
                      .Append(res.body.To)
                      .Append(res.body.ContractAddress);
            return strRes.ToString();
        }
        /// <summary>
        /// get character string of to get the transaction infomation to sign verification
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetFiscoTxInfoResMac(NodeApiResBody<FiscoTransactionData> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.TxId)
                      .Append(res.body.BlockHash)
                      .Append(res.body.BlockNumber)
                      .Append(res.body.GasUsed)
                      .Append(res.body.From)
                      .Append(res.body.To)
                      .Append(res.body.Value)
                      .Append(res.body.Input);
            return strRes.ToString();
        }
        /// <summary>
        /// get character string of to get the block infomation to sign verification
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetFiscoBlockInfoResMac(NodeApiResBody<FiscoBlockData> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.BlockHash)
                      .Append(res.body.BlockNumber)
                      .Append(res.body.ParentBlockHash)
                      .Append(res.body.BlockSize)
                      .Append(res.body.BlockTime)
                      .Append(res.body.Author);
            if (res.body.Transactions != null && res.body.Transactions.Count > 0)
            {
                string transMac = string.Empty;
                foreach (FiscoTransactionData t in res.body.Transactions)
                {
                    transMac += t.TxId;
                    transMac += t.BlockHash;
                    transMac += t.BlockNumber;
                    transMac += t.GasUsed;
                    transMac += t.From;
                    transMac += t.To;
                    transMac += t.Value;
                    transMac += t.Input;
                }
                strRes.Append(transMac);
            }
            return strRes.ToString();
        }
        /// <summary>
        /// character string to sign verification to get the event registered
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetFiscoEventRegisterResMac(NodeApiResBody<RegisterEventResData> res)
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
        public static string GetFiscoQueryEventResMac(NodeApiResBody<QueryEventResData> res)
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