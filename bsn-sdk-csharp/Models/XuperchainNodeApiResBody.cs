using System.Collections.Generic;
using System.Numerics;

namespace bsn_sdk_csharp.Models
{
    /// <summary>
    /// Response parameters of user registration interface
    /// </summary>
    public class RegisterUserResDataBody
    {
        /// <summary>
        /// User ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// User address
        /// </summary>
        public string UserAddr { get; set; }
    }
    /// <summary>
    /// Response parameters of invoke the smart contract in Key Trust Mode interface
    /// </summary>
    public class CallContractResDataBody
    {
        /// <summary>
        /// Transaction ID
        /// Note: Invoke contract, cannot be null
        /// </summary>
        public string TxId { get; set; }
        /// <summary>
        /// Query information
        /// Query contract, cannot be null
        /// </summary>
        public string QueryInfo { get; set; }
    }
    /// <summary>
    /// Response parameters of get transaction informaiton interface
    /// </summary>
    public class XuperchainTransaction
    {
        /// <summary>
        /// Transaction hash
        /// </summary>
        public string TxId { get; set; }
        /// <summary>
        /// Block hash
        /// </summary>
        public string BlockId { get; set; }
        /// <summary>
        /// Transaction version
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// Contract's request data
        /// </summary>
        public List<InvokeRequest> ContractRequests { get; set; }
        /// <summary>
        /// Timestamp when receiving the transaction
        /// </summary>
        public BigInteger ReceivedTimestamp { get; set; }
    }
    /// <summary>
    /// Response parameters of get transaction information interface
    /// </summary>
    public class InvokeRequest
    {
        /// <summary>
        /// Contact name
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// Method name
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// Parameters
        /// </summary>
        public string Args { get; set; }
    }
    /// <summary>
    /// Response parameters of get block informaiton interface
    /// </summary>
    public class XuperchainBlock
    {
        /// <summary>
        /// version
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// Block hash
        /// </summary>
        public string BlockId { get; set; }
        /// <summary>
        /// Previous block hash
        /// </summary>
        public string PreHash { get; set; }
        /// <summary>
        /// Block height
        /// </summary>
        public BigInteger Height { get; set; }
        /// <summary>
        /// Timestamp of the block
        /// </summary>
        public BigInteger Timestamp { get; set; }
        /// <summary>
        /// Transactions
        /// </summary>
        public List<XuperchainTransaction> Transactions { get; set; }
        /// <summary>
        /// The number of transactions in the block
        /// </summary>
        public int TxCount { get; set; }
        /// <summary>
        /// Next block hash
        /// </summary>
        public string NextHash { get; set; }
    }
}