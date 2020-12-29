namespace bsn_sdk_csharp.Models
{
    public class CitaRegisterReqBody
    {
        public string UserId { get; set; }
    }

    public class CitaTransReqDataBody
    {
        public string UserId { get; set; }
        public string ContractName { get; set; }

        public string FuncName { get; set; }

        public string FuncParam { get; set; }
    }

    public class CitaTxReqDataBody
    {
        public string TxHash { get; set; }
    }

    public class CitaBlockReqDataBody
    {
        public string BlockNumber { get; set; }

        public string BlockHash { get; set; }
    }

    public class CitaTransReqBody
    {
        public string ContractName { get; set; }

        public string TransData { get; set; }
    }

    public class CitaTransReq
    {
        public ContractData Contract { get; set; }
        public string FuncName { get; set; }
        public string UserName { get; set; }

        public object[] Args { get; set; }
    }

    public class CitaRegisterEventReqDataBody
    {
        public int EventType { get; set; }

        public string ContractAddress { get; set; }

        public string ContractName { get; set; }

        public string NotifyUrl { get; set; }

        public string AttachArgs { get; set; }
    }

    public class CitaRemoveEventReqDataBody
    {
        public string EventId { get; set; }
    }
}