using bsn_sdk_csharp.Models;
using System.Text;

namespace bsn_sdk_csharp.NodeExtends
{
    public class FiscoReqMacExtends
    {
        /// <summary>
        /// character string to sign to get the user registered
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetRegisterUserReqMac(NodeApiReqBody<FiscoRegisterReqBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.UserId);
            return strBuilder.ToString();
        }
        /// <summary>
        /// character string to sign of transaction processing under key upload mode
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetFiscoTransReqMac(NodeApiReqBody<FiscoTransReqBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.ContractName)
                      .Append(req.body.TransData);
            return strBuilder.ToString();
        }

        public static string GetReqHeaderMac(ReqHeader header)
        {
            StringBuilder strRes = new StringBuilder();
            strRes.Append(header.userCode)
                      .Append(header.appCode);
            return strRes.ToString();
        }
        /// <summary>
        /// character string to sign of transaction processing under key trust mode
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetFiscoTransReqMac(NodeApiReqBody<FiscoTransReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.UserId)
                      .Append(req.body.ContractName)
                      .Append(req.body.FuncName)
                      .Append(req.body.FuncParam);
            return strBuilder.ToString();
        }
        /// <summary>
        /// character string to sign to get the transacion receipt 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetFiscoTxReceiptReqMac(NodeApiReqBody<FiscoTxReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.TxHash);
            return strBuilder.ToString();
        }
        /// <summary>
        /// character string to sign to get the block information
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetFiscoBlockInfoReqMac(NodeApiReqBody<FiscoBlockReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.BlockNumber)
                      .Append(req.body.BlockHash);
            return strBuilder.ToString();
        }
        /// <summary>
        /// character string to sign to register event 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetFiscoEventRegisterReqMac(NodeApiReqBody<FiscoRegisterReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.EventType)
                      .Append(req.body.ContractAddress)
                      .Append(req.body.ContractName)
                      .Append(req.body.NotifyUrl)
                      .Append(req.body.AttachArgs);
            return strBuilder.ToString();
        }
        /// <summary>
        /// character string to sign to logout event 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetFiscoEventRemoveReqMac(NodeApiReqBody<RemoveReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.EventId);
            return strBuilder.ToString();
        }
    }
}