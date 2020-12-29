using bsn_sdk_csharp.Models;
using System.Text;

namespace bsn_sdk_csharp.NodeExtends
{
    public class CitaReqMacExtends
    {
        public static string GetRegisterUserReqMac(NodeApiReqBody<CitaRegisterReqBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.UserId);
            return strBuilder.ToString();
        }

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

        public static string GetCitaTxReceiptReqMac(NodeApiReqBody<CitaTxReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.TxHash);
            return strBuilder.ToString();
        }

        public static string GetCitaBlockInfoReqMac(NodeApiReqBody<CitaBlockReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.BlockNumber)
                      .Append(req.body.BlockHash);
            return strBuilder.ToString();
        }

        public static string GetSDKTransReqMac(NodeApiReqBody<CitaTransReqBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.ContractName)
                      .Append(req.body.TransData);
            return strBuilder.ToString();
        }

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