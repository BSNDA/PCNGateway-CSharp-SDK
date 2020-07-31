using System.Collections.Generic;
using System.Numerics;

namespace bsn_sdk_csharp.Models
{
    public class RegisterUserResDataBody
    {
        public string AccAddr { get; set; }
    }

    public class CallContractResDataBody
    {
        public string TxId { get; set; }

        public string QueryInfo { get; set; }
    }

    public class XuperchainTransaction
    {
        public string Txid { get; set; }

        public string Blockid { get; set; }

        public int Version { get; set; }

        public List<InvokeRequest> ContractRequests { get; set; }
        public BigInteger ReceivedTimestamp { get; set; }
    }

    public class InvokeRequest
    {
        public string ContractName { get; set; }
        public string MethodName { get; set; }
        public string Args { get; set; }
    }

    public class XuperchainBlock
    {
        public int Version { get; set; }
        public string Blockid { get; set; }

        public string PreHash { get; set; }

        public BigInteger Height { get; set; }

        public BigInteger Timestamp { get; set; }

        public List<XuperchainTransaction> Transactions { get; set; }

        public int TxCount { get; set; }

        public string NextHash { get; set; }
    }
}