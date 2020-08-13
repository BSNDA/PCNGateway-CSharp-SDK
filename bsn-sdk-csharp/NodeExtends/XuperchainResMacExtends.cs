using bsn_sdk_csharp.Models;
using System.Text;

namespace bsn_sdk_csharp.NodeExtends
{
    public class XuperchainResMacExtends
    {
        public static string GetRegisterUserResMac(NodeApiResBody<RegisterUserResDataBody> res)
        {
            //assemble the original string to verify
            StringBuilder strRes = new StringBuilder();
            strRes.Append(GetResHeaderMac(res.header))
                      .Append(res.body.userId)
                      .Append(res.body.userAddr);
            return strRes.ToString();
        }

        public static string GetBlockInfoResMac(NodeApiResBody<XuperchainBlock> res)
        {
            //assemble the original string to verify
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetResHeaderMac(res.header))
                      .Append(res.body.Version)
                              .Append(res.body.Blockid)
                              .Append(res.body.PreHash)
                              .Append(res.body.Height)
                              .Append(res.body.Timestamp);
            if (res.body.Transactions != null && res.body.Transactions.Count > 0)
            {
                string transMac = string.Empty;
                foreach (XuperchainTransaction t in res.body.Transactions)
                {
                    transMac += t.Txid;
                    transMac += t.Blockid;
                    transMac += t.Version;
                    string contact = string.Empty;
                    if (t.ContractRequests != null && t.ContractRequests.Count > 0)
                    {
                        foreach (var c in t.ContractRequests)
                        {
                            contact += c.ContractName;
                            contact += c.MethodName;
                            contact += c.Args;
                        }
                        transMac += contact;
                    }
                    transMac += t.ReceivedTimestamp;
                }
                strBuilder.Append(transMac);
            }
            strBuilder.Append(res.body.TxCount)
                .Append(res.body.NextHash);
            return strBuilder.ToString();
        }

        public static string ReqChainCodeResMac(NodeApiResBody<CallContractResDataBody> res)
        {
            //assemble the original string to verify
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetResHeaderMac(res.header))
                .Append(res.body.TxId)
                .Append(res.body.QueryInfo);

            return strBuilder.ToString();
        }

        public static string GetTxInfoResMac(NodeApiResBody<XuperchainTransaction> res)
        {
            //assemble the original string to verify
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetResHeaderMac(res.header))
                      .Append(res.body.Txid)
                              .Append(res.body.Blockid)
                              .Append(res.body.Version);

            string contact = string.Empty;
            if (res.body.ContractRequests != null && res.body.ContractRequests.Count > 0)
            {
                foreach (var c in res.body.ContractRequests)
                {
                    contact += c.ContractName;
                    contact += c.MethodName;
                    contact += c.Args;
                }
                strBuilder.Append(contact);
            }
            strBuilder.Append(res.body.ReceivedTimestamp);
            return strBuilder.ToString();
        }

        public static string GetResHeaderMac(ResHeader header)
        {
            StringBuilder strRes = new StringBuilder();
            strRes.Append(header.code)
                      .Append(header.msg);
            return strRes.ToString();
        }
    }
}