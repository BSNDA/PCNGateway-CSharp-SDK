using bsn_sdk_csharp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace bsn_sdk_csharp.NodeExtends
{
    public class FiscoReqMacExtends
    {
        public static string GetRegisterUserReqMac(NodeApiReqBody<FiscoRegisterReqBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.UserId);
            return strBuilder.ToString();
        }

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

        public static string GetFiscoTxReceiptReqMac(NodeApiReqBody<FiscoTxReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.TxHash);
            return strBuilder.ToString();
        }

        public static string GetFiscoBlockInfoReqMac(NodeApiReqBody<FiscoBlockReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.BlockNumber)
                      .Append(req.body.BlockHash);
            return strBuilder.ToString();
        }

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

        public static string GetFiscoEventRemoveReqMac(NodeApiReqBody<RemoveReqDataBody> req)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(GetReqHeaderMac(req.header))
                      .Append(req.body.EventId);
            return strBuilder.ToString();
        }
    }
}