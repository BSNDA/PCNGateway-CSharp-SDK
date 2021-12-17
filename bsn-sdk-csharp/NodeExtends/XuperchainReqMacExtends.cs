using bsn_sdk_csharp.Models;
using System.Text;

namespace bsn_sdk_csharp.NodeExtends
{
    public class XuperchainReqMacExtends
    {
        /// <summary>
        /// character string to sign to get the user registered
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetRegisterUserReqMac(NodeApiReqBody<RegisterUserReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.UserId);
            return strBuilder.ToString();
        }
        /// <summary>
        /// character string to sign of transaction processing under password management mode
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string ReqChainCodeReqMac(NodeApiReqBody<CallContractReqDataReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                          .Append(req.body.UserId)
                          .Append(req.body.UserAddr)
                          .Append(req.body.ContractName)
                          .Append(req.body.FuncName)
                          .Append(req.body.FuncParam);

            return strBuilder.ToString();
        }
        /// <summary>
        /// character string to sign to get the block information 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetBlockInfoReqMac(NodeApiReqBody<GetBlockInfoReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.BlockHeight)
                          .Append(req.body.BlockHash);
            return strBuilder.ToString();
        }
        /// <summary>
        /// character string to sign to get the transaction information
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetTxInfoReqMac(NodeApiReqBody<GetTxInfoReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                          .Append(req.body.TxHash);

            return strBuilder.ToString();
        }

        public static string GetReqHeaderMac(ReqHeader header)
        {
            StringBuilder strRes = new StringBuilder();
            strRes.Append(header.userCode)
                      .Append(header.appCode);
            return strRes.ToString();
        }
    }
}