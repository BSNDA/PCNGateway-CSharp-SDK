using System.Numerics;

namespace bsn_sdk_csharp.Models
{
    /// <summary>
    /// Request parameters of user registration interface
    /// </summary>
    public class RegisterUserReqDataBody
    {
        /// <summary>
        /// user Id
        /// </summary>
        public string UserId { get; set; }
    }
    /// <summary>
    /// Request parameters of invoke the smart contract in Public Key Upload Mode interface
    /// </summary>
    public class CallContractReqDataReqDataBody
    {
        /// <summary>
        /// user Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// user address
        /// </summary>
        public string UserAddr { get; set; }
        /// <summary>
        /// Contract name
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// function name
        /// </summary>
        public string FuncName { get; set; }
        /// <summary>
        /// function params
        /// </summary>
        public string FuncParam { get; set; }
    }
    /// <summary>
    /// Request parameters of get block informaiton interface
    /// </summary>
    public class GetBlockInfoReqDataBody
    {
        /// <summary>
        /// Block height
        /// Note: Cannot be null when blockHeight is null
        /// </summary>
        public BigInteger BlockHeight { get; set; }
        /// <summary>
        /// Block hash
        /// Note: Cannot be null when blockHeight is null
        /// </summary>
        public string BlockHash { get; set; }
    }
    /// <summary>
    /// Request parameters of get transaction informaiton interface
    /// </summary>
    public class GetTxInfoReqDataBody
    {
        /// <summary>
        /// Transaction hash
        /// </summary>
        public string TxHash { get; set; }
    }
}