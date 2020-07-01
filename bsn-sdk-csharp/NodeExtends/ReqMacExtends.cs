using bsn_sdk_csharp.Models;
using System.Text;

namespace bsn_sdk_csharp.NodeExtends
{
    /// <summary>
    /// Request parameters of character string concatentation and extension to sign
    /// </summary>
    public class ReqMacExtends
    {
        /// <summary>
        /// character string to sign to get the user registered 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetRegisterUserReqMac(NodeApiReqBody<RegisterUserReqBody> req)
        {
            //assemble the original string to sign
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.name)
                      .Append(req.body.secret);
            return strBuilder.ToString();
        }

        /// <summary>
        /// character string to sign to get user certificate request 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetEnrollUserReqMac(NodeApiReqBody<EnrollUserReqBody> req)
        {
            //assemble the original string to sign
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.name)
                      .Append(req.body.secret)
                      .Append(req.body.csrPem);
            return strBuilder.ToString();
        }

        /// <summary>
        /// character string to sign to get the transaction information
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetTransactionReqMac(NodeApiReqBody<GetTransReqBody> req)
        {
            //assemble the original string to sign 
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.txId);
            return strBuilder.ToString();
        }

        /// <summary>
        /// character string to sign to get the block information
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetBlockInfoReqMac(NodeApiReqBody<GetBlockReqBody> req)
        {
            //assemble the original character string to sign 
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.blockNumber)
                          .Append(req.body.blockHash)
                          .Append(req.body.txId);
            return strBuilder.ToString();
        }

        /// <summary>
        /// character string to sign to register event chaincode
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string EventRegisterReqMac(NodeApiReqBody<EventRegisterReqBody> req)
        {
            //assemble the original character string 
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.chainCode)
                          .Append(req.body.eventKey)
                          .Append(req.body.notifyUrl)
                          .Append(req.body.attachArgs);
            return strBuilder.ToString();
        }

        /// <summary>
        /// character string to sign to logout event chaincode
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string EventRemoveReqMac(NodeApiReqBody<EventRemoveReqBody> req)
        {
            //assemble original character string 
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.eventId);
            return strBuilder.ToString();
        }

        /// <summary>
        ///character string to sign of transaction processing under password management mode 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string ReqChainCodeReqMac(NodeApiReqBody<ReqChainCodeReqBody> req)
        {
            //assemble the original string to sign 
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                          .Append(req.body.userName)
                          .Append(req.body.nonce)
                          .Append(req.body.chainCode)
                          .Append(req.body.funcName);
            if (req.body.args != null && req.body.args.Length > 0)
            {
                for (int i = 0; i < req.body.args.Length; i++)
                {
                    strBuilder.Append(req.body.args[i]);
                }
            }

            if (req.body.transientData != null && req.body.transientData.Count > 0)
            {
                foreach (var t in req.body.transientData)
                {
                    strBuilder.Append(t.Key);
                    strBuilder.Append(t.Value);
                }
            }

            return strBuilder.ToString();
        }

        /// <summary>
        /// character string to sign of transaction processing under random password mode 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string TransReqMac(NodeApiReqBody<TransReqBody> req)
        {
            //assemble the original character string 
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                          .Append(req.body.transData);

            return strBuilder.ToString();
        }

        /// <summary>
        /// concatenate the character string in the header 
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public static string GetReqHeaderMac(ReqHeader header)
        {
            StringBuilder strRes = new StringBuilder();
            strRes.Append(header.userCode)
                      .Append(header.appCode);
            return strRes.ToString();
        }
    }
}