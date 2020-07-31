namespace bsn_sdk_csharp.Models
{
    public class FiscoTransReq
    {
        public ContractData Contract { get; set; }
        public string FuncName { get; set; }
        public string UserName { get; set; }

        public object[] Args { get; set; }
    }

    public class ContractData
    {
        public string ContractName { get; set; }

        public string ContractAbi { get; set; }

        public string ContractAddress { get; set; }
    }

    public class FiscoTransReqBody
    {
        public string ContractName { get; set; }

        public string TransData { get; set; }
    }

    public class FiscoRegisterReqBody
    {
        public string UserId { get; set; }
    }

    public class FiscoTransReqDataBody
    {
        public string UserId { get; set; }
        public string ContractName { get; set; }

        public string FuncName { get; set; }

        public string FuncParam { get; set; }
    }

    public class FiscoTxReqDataBody
    {
        public string TxHash { get; set; }
    }

    public class FiscoBlockReqDataBody
    {
        public string BlockNumber { get; set; }

        public string BlockHash { get; set; }
    }

    public class FiscoRegisterReqDataBody
    {
        public int EventType { get; set; }

        public string ContractAddress { get; set; }

        public string ContractName { get; set; }

        public string NotifyUrl { get; set; }

        public string AttachArgs { get; set; }
    }

    public class RemoveReqDataBody
    {
        public string EventId { get; set; }
    }
}