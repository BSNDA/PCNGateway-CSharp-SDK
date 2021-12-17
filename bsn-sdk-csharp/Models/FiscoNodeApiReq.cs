namespace bsn_sdk_csharp.Models
{
    /// <summary>
    /// content requested by transaction
    /// user use
    /// </summary>
    public class FiscoTransReq
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
        /// parameter
        /// </summary>
        public object[] Args { get; set; }
    }
    /// <summary>
    /// data of contract
    /// </summary>
    public class ContractData
    {
        /// <summary>
        /// contract name
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// contract abi
        /// </summary>
        public string ContractAbi { get; set; }
        /// <summary>
        /// contract address
        /// </summary>
        public string ContractAddress { get; set; }
    }
    /// <summary>
    /// content requested by transaction
    /// call the gateway to use
    /// </summary>
    public class FiscoTransReqBody
    {
        /// <summary>
        /// contract name
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// transaction data
        /// </summary>
        public string TransData { get; set; }
    }
    /// <summary>
    /// content requested by register user
    /// </summary>
    public class FiscoRegisterReqBody
    {
        /// <summary>
        /// user Id
        /// </summary>
        public string UserId { get; set; }
    }
    /// <summary>
    /// content requested by transaction select
    /// </summary>
    public class FiscoTransReqDataBody
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
        /// function param
        /// </summary>
        public string FuncParam { get; set; }
    }
    /// <summary>
    /// content requested by transaction select
    /// </summary>
    public class FiscoTxReqDataBody
    {
        /// <summary>
        /// transaction hash
        /// </summary>
        public string TxHash { get; set; }
    }
    /// <summary>
    /// content requested by select blockdata 
    /// </summary>
    public class FiscoBlockReqDataBody
    {
        /// <summary>
        /// block number
        /// </summary>
        public string BlockNumber { get; set; }
        /// <summary>
        /// block hash
        /// </summary>
        public string BlockHash { get; set; }
    }
    /// <summary>
    /// content requested by register event
    /// </summary>
    public class FiscoRegisterReqDataBody
    {
        /// <summary>
        /// event type 
        /// 1:block event
        /// 2:contract event
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
        /// event notify url
        /// </summary>
        public string NotifyUrl { get; set; }
        /// <summary>
        /// additional parameters
        /// </summary>
        public string AttachArgs { get; set; }
    }
    /// <summary>
    /// content requested by event remove
    /// </summary>
    public class RemoveReqDataBody
    {
        /// <summary>
        /// eventId
        /// </summary>
        public string EventId { get; set; }
    }
}