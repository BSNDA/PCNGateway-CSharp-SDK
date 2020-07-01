using bsn_sdk_csharp.Models;
using System.Collections.Generic;
using System.Text;

namespace bsn_sdk_csharp.NodeExtends
{
    /// <summary>
    ///return parameters of concatenated and extended character string to sign 
    /// </summary>
    public class ResMacExtends
    {
        /// <summary>
        /// get character string of user registration to sign 
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetRegisterUserResMac(NodeApiResBody<RegisterUserResBody> res)
        {
            //assemble the original string to verify 
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.name)
                      .Append(res.body.secret);
            return strRes.ToString();
        }

        /// <summary>
        /// character string of user cert request to sign 
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetEnrollUserResMac(NodeApiResBody<EnrollUserResBody> res)
        {
            //assemble the original string to verify 
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetResHeaderMac(res.header))
                      .Append(res.body.cert);
            return strBuilder.ToString();
        }

        /// <summary>
        /// character string to sign to get the transaction information 
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetTransactionResMac(NodeApiResBody<GetTransResBody> res)
        {
            //assemble the original string to verify 
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetResHeaderMac(res.header))
                      .Append(res.body.blockHash)
                              .Append(res.body.blockNumber)
                              .Append(res.body.status)
                              .Append(res.body.createName)
                              .Append(res.body.timeSpanSec)
                              .Append(res.body.timeSpanNsec);
            return strBuilder.ToString();
        }

        /// <summary>
        /// character string to sign to get the block information 
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetBlockInfoResMac(NodeApiResBody<GetBlockResBody> res)
        {
            //assemble the original string to verify 
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetResHeaderMac(res.header))
                      .Append(res.body.blockHash)
                              .Append(res.body.blockNumber)
                              .Append(res.body.preBlockHash)
                              .Append(res.body.blockSize)
                              .Append(res.body.blockTxCount);
            if (res.body.transactions != null && res.body.transactions.Count > 0)
            {
                string transMac = string.Empty;
                foreach (TransactionData t in res.body.transactions)
                {
                    transMac += t.txId;
                    transMac += t.status;
                    transMac += t.createName;
                    transMac += t.timeSpanSec;
                    transMac += t.timeSpanNsec;
                }
                strBuilder.Append(transMac);
            }
            return strBuilder.ToString();
        }

        /// <summary>
        /// character string to sign to get the ledger information 
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public static string GetLedgerInfoResMac(NodeApiResBody<GetLedgerResBody> res)
        {
            //assemble the orginal string to verify 
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetResHeaderMac(res.header))
                      .Append(res.body.blockHash)
                              .Append(res.body.height)
                              .Append(res.body.preBlockHash);

            return strBuilder.ToString();
        }

        /// <summary>
        /// character string to sign to register event chaincode 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string EventRegisterResMac(NodeApiResBody<EventRegisterResBody> res)
        {
            //assemble the orginal string to verify 
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetResHeaderMac(res.header))
                      .Append(res.body.eventId);
            return strBuilder.ToString();
        }

        /// <summary>
        /// character string of chaincode query to sign
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string EventQueryResMac(NodeApiResBody<List<EventQueryResBody>> res)
        {
            //assemble the original string to verify
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetResHeaderMac(res.header));
            if (res.body != null && res.body.Count > 0)
            {
                string strmac = string.Empty;
                foreach (var s in res.body)
                {
                    strmac += s.eventId;
                    strmac += s.eventKey;
                    strmac += s.notifyUrl;
                    strmac += s.attachArgs;
                    strmac += s.createTime;
                    strmac += s.orgCode;
                    strmac += s.userCode;
                    strmac += s.appCode;
                    strmac += s.chainCode;
                }
                strBuilder.Append(strmac);
            }
            return strBuilder.ToString();
        }

        /// <summary>
        ///character string to sign of transaction processing under password management mode
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string ReqChainCodeResMac(NodeApiResBody<ReqChainCodeResBody> res)
        {
            //assemble the original string to verify 
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetResHeaderMac(res.header));
            if (res.body.blockInfo != null)
            {
                strBuilder.Append(res.body.blockInfo.txId)
                    .Append(res.body.blockInfo.blockHash)
                    .Append(res.body.blockInfo.status);
            }
            if (res.body.ccRes != null)
            {
                strBuilder.Append(res.body.ccRes.ccCode)
                        .Append(res.body.ccRes.ccData);
            }
            return strBuilder.ToString();
        }

        /// <summary>
        /// concatenate the request string in the header
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public static string GetResHeaderMac(ResHeader header)
        {
            StringBuilder strRes = new StringBuilder();
            strRes.Append(header.code)
                      .Append(header.msg);
            return strRes.ToString();
        }
    }
}