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
}