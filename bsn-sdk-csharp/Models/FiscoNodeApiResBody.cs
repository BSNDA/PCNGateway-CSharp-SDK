using System.Collections.Generic;
using System.Numerics;

namespace bsn_sdk_csharp.Models
{
    /// <summary>
    /// return contents of block height
    /// </summary>
    public class GetBlockHeightResBody
    {
        /// <summary>
        /// block height
        /// </summary>
        public string Data { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class FiscoTransResBody
    {
        /// <summary>
        /// Call type
        /// </summary>
        public bool Constant { get; set; }
        /// <summary>
        ///  Query information
        ///  Note: This field has value when Constant is true
        /// </summary>
        public string QueryInfo { get; set; }
        /// <summary>
        /// Transaction hash
        /// Note: This field has value and is valid when Constant is false
        /// </summary>
        public string TxId { get; set; }
        /// <summary>
        /// Block hash
        /// Note: This field has value and is valid when Constant is false
        /// </summary>
        public string BlockHash { get; set; }
        /// <summary>
        /// Block number
        /// Note: This field has value and is valid when Constant is false
        /// </summary>
        public BigInteger BlockNumber { get; set; }
        /// <summary>
        /// Gas used value
        /// Note: This field has value and is valid when Constant is false
        /// </summary>
        public BigInteger GasUsed { get; set; }
        /// <summary>
        /// Transaction status
        /// Note: Note: This field has value and is valid when Constant is false, 0x0 means success, the staus refers to transaction receipt status.
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Transaction sender
        /// Note: This field has value and is valid when Constant is false
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// Transaction receiver
        /// Note: This field has value and is valid when Constant is false
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Input
        /// Note: This field has value and is valid when Constant is false
        /// </summary>
        public string Input { get; set; }
        /// <summary>
        /// Output
        /// Note: This field has value and is valid when Constant is false
        /// </summary>
        public string Output { get; set; }
        /// <summary>
        /// Log
        /// Note: When contract contains event, this field returns the content of the event.
        /// </summary>
        public string Logs { get; set; }
    }
    /// <summary>
    /// Response parameters of user registration interface
    /// </summary>
    public class FiscoRegisterUserResBody
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// User address
        /// </summary>
        public string UserAddress { get; set; }
    }
    /// <summary>
    /// Response parameters of get transaction receipt interface
    /// </summary>
    public class FiscoTxReceiptResBody
    {
        /// <summary>
        /// Transaction ID
        /// </summary>
        public string TxId { get; set; }
        /// <summary>
        /// Block hash
        /// </summary>
        public string BlockHash { get; set; }
        /// <summary>
        /// Block number
        /// </summary>
        public BigInteger BlockNumber { get; set; }
        /// <summary>
        ///  Gas used
        /// </summary>
        public BigInteger GasUsed { get; set; }
        /// <summary>
        /// Transaction sender
        /// Note: The user address sending transactions when invoking the interface.
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// Transaction receiver
        /// Note: Contract address when invoking the interface.
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Contract address
        /// Note: The returned contract address when deploying the smart contract.
        /// </summary>
        public string ContractAddress { get; set; }
        /// <summary>
        /// Log
        /// Note: When contract contains event, this field returns the content of the event.
        /// </summary>
        public string Logs { get; set; }
    }
    /// <summary>
    /// Response parameters of get transaction information interface
    /// </summary>
    public class FiscoTransactionData
    {
        /// <summary>
        /// Transaction hash
        /// </summary>
        public string TxId { get; set; }
        /// <summary>
        /// Block hash
        /// </summary>
        public string BlockHash { get; set; }
        /// <summary>
        /// Block number
        /// </summary>
        public BigInteger BlockNumber { get; set; }
        /// <summary>
        /// Gas used
        /// </summary>
        public BigInteger GasUsed { get; set; }
        /// <summary>
        /// Transaction sender
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// Transaction receiver
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Transferred value
        /// </summary>
        public BigInteger Value { get; set; }
        /// <summary>
        /// Input of the transaction
        /// </summary>
        public string Input { get; set; }
    }
    /// <summary>
    /// Response parameters of get block information interface
    /// </summary>
    public class FiscoBlockData
    {
        /// <summary>
        /// Block hash
        /// </summary>
        public string BlockHash { get; set; }
        /// <summary>
        /// Block number
        /// </summary>
        public BigInteger BlockNumber { get; set; }
        /// <summary>
        /// Previous block hash
        /// </summary>
        public string ParentBlockHash { get; set; }
        /// <summary>
        /// Block size
        /// </summary>
        public BigInteger BlockSize { get; set; }
        /// <summary>
        /// Parameter: block time
        /// Note: Timestamp in milliseconds format
        /// </summary>
        public BigInteger BlockTime { get; set; }
        /// <summary>
        /// Author
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Transaction information
        /// </summary>
        public List<FiscoTransactionData> Transactions { get; set; }
    }
    /// <summary>
    /// Response parameters of event interface
    /// </summary>
    public class RegisterEventResData
    {
        /// <summary>
        /// Event ID
        /// </summary>
        public string EventId { get; set; }
    }
    /// <summary>
    /// Basic event information
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Event ID
        /// </summary>
        public string EventId { get; set; }
        /// <summary>
        /// Appcode
        /// </summary>
        public string AppCode { get; set; }
        /// <summary>
        /// UserCode
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// Event notify url
        /// </summary>
        public string NotifyUrl { get; set; }
        /// <summary>
        /// Attached args
        /// </summary>
        public string AttachArgs { get; set; }
        /// <summary>
        /// Created time
        /// UTC time
        /// </summary>
        public string CreateTime { get; set; }
    }
    /// <summary>
    /// Contract Event
    /// </summary>
    public class ContractEvent : Event
    {
        /// <summary>
        /// Contract Address
        /// </summary>
        public string ContractAddress { get; set; }
    }
    /// <summary>
    /// Response parameters of query  event interface
    /// </summary>
    public class QueryEventResData
    {
        /// <summary>
        /// Block Event
        /// </summary>
        public List<Event> BlockEvent { get; set; }
        /// <summary>
        /// Contract Event
        /// </summary>
        public List<ContractEvent> ContractEvent { get; set; }
    }
}