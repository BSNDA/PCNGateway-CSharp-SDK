using bsn_sdk_csharp.Models;
using System.Text;

namespace bsn_sdk_csharp.NodeExtends
{
    public class CitaReqMacExtends
    {
        /// <summary>
        /// character string to sign to get the user registered
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetRegisterUserReqMac(NodeApiReqBody<CitaRegisterReqBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.UserId);
            return strBuilder.ToString();
        }
        /// <summary>
        /// character string to sign of transaction processing under key trust mode
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetCitaTransReqMac(NodeApiReqBody<CitaTransReqDataBody> req)
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
        public static string GetCitaTxReceiptReqMac(NodeApiReqBody<CitaTxReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.TxHash);
            return strBuilder.ToString();
        }
        /// <summary>
        /// character string to sign to get the block infomation 
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetCitaBlockInfoReqMac(NodeApiReqBody<CitaBlockReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.BlockNumber)
                      .Append(req.body.BlockHash);
            return strBuilder.ToString();
        }
        /// <summary>
        /// character string to sign of  transaction processing under key upload mode
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetSDKTransReqMac(NodeApiReqBody<CitaTransReqBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.ContractName)
                      .Append(req.body.TransData);
            return strBuilder.ToString();
        }
        /// <summary>
        /// character string to sign to get the event registered
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetCitaEventRegisterReqMac(NodeApiReqBody<CitaRegisterEventReqDataBody> req)
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
        /// character string to sign to get the event removed
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string GetCitaEventRemoveReqMac(NodeApiReqBody<CitaRemoveEventReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.EventId);
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