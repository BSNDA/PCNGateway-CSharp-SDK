namespace bsn_sdk_csharp.Models
{
    /// <summary>
    /// content requested by registered user
    /// </summary>
    public class CitaRegisterReqBody
    {
        /// <summary>
        /// registered user id
        /// </summary>
        public string UserId { get; set; }
    }

    /// <summary>
    /// content requested by transaction under Key-Trust Mode
    /// </summary>
    public class CitaTransReqDataBody
    {
        /// <summary>
        /// user Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// contract name
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        /// function name
        /// </summary>
        public string FuncName { get; set; }

        /// <summary>
        /// function parameters
        /// </summary>
        public string FuncParam { get; set; }
    }

    /// <summary>
    /// content requested by transaction query
    /// </summary>
    public class CitaTxReqDataBody
    {
        /// <summary>
        /// transaction hash
        /// </summary>
        public string TxHash { get; set; }
    }

    /// <summary>
    /// content requested by block
    /// </summary>
    public class CitaBlockReqDataBody
    {
        /// <summary>
        /// block number cannot be empty at the same time
        /// </summary>
        public string BlockNumber { get; set; }

        /// <summary>
        /// block hash cannot be empty at the same time
        /// </summary>
        public string BlockHash { get; set; }
    }

    /// <summary>
    /// content requested by transaction under Public-Key-Upload Mode(system encapsulation)
    /// </summary>
    public class CitaTransReqBody
    {
        /// <summary>
        /// contract name
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        /// transation data
        /// </summary>
        public string TransData { get; set; }
    }

    /// <summary>
    /// content requested by transaction under Public-Key-Upload Mode(user input)
    /// </summary>
    public class CitaTransReq
    {
        /// <summary>
        /// contract
        /// </summary>
        public ContractData Contract { get; set; }

        /// <summary>
        /// function name
        /// </summary>
        public string FuncName { get; set; }

        /// <summary>
        /// user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// requested parameters
        /// </summary>
        public object[] Args { get; set; }
    }

    /// <summary>
    /// content requested by register event
    /// </summary>
    public class CitaRegisterEventReqDataBody
    {
        /// <summary>
        /// event type
        /// </summary>
        public int EventType { get; set; }

        /// <summary>
        /// contract address
        /// </summary>
        public string ContractAddress { get; set; }

        /// <summary>
        /// contract name
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        /// notification address of event
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// additional parameters
        /// </summary>
        public string AttachArgs { get; set; }
    }

    /// <summary>
    /// content requested by remove event
    /// </summary>
    public class CitaRemoveEventReqDataBody
    {
        /// <summary>
        /// event id
        /// </summary>
        public string EventId { get; set; }
    }
}