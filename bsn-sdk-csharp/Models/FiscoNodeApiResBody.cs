using System.Collections.Generic;
using System.Numerics;

namespace bsn_sdk_csharp.Models
{
    public class GetBlockHeightResBody
    {
        public string data { get; set; }
    }

    public class FiscoTransResBody
    {
        public bool Constant { get; set; }

        public string QueryInfo { get; set; }

        public string TxId { get; set; }
        public string BlockHash { get; set; }
        public BigInteger BlockNumber { get; set; }
        public BigInteger GasUsed { get; set; }
        public string Status { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }

        public string Logs { get; set; }
    }

    public class FiscoRegisterUserResBody
    {
        public string UserId { get; set; }

        public string UserAddress { get; set; }
    }

    public class FiscoTxReceiptResBody
    {
        public string TxId { get; set; }

        public string BlockHash { get; set; }

        public BigInteger BlockNumber { get; set; }
        public BigInteger GasUsed { get; set; }
        public string Status { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public string ContractAddress { get; set; }

        public string Logs { get; set; }
    }

    public class FiscoTransactionData
    {
        public string TxId { get; set; }

        public string BlockHash { get; set; }

        public BigInteger BlockNumber { get; set; }
        public BigInteger GasUsed { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public BigInteger Value { get; set; }

        public string Input { get; set; }
    }

    public class FiscoBlockData
    {
        public string BlockHash { get; set; }

        public BigInteger BlockNumber { get; set; }

        public string ParentBlockHash { get; set; }

        public BigInteger BlockSize { get; set; }

        public BigInteger BlockTime { get; set; }

        public string Author { get; set; }

        public List<FiscoTransactionData> Transactions { get; set; }
    }

    public class RegisterEventResData
    {
        public string EventId { get; set; }
    }

    public class Event
    {
        public string EventId { get; set; }

        public string AppCode { get; set; }

        public string UserCode { get; set; }

        public string NotifyUrl { get; set; }

        public string AttachArgs { get; set; }

        public string CreateTime { get; set; }
    }

    public class ContractEvent
    {
        public Event Event { get; set; }

        public string ContractAddress { get; set; }
    }

    public class QueryEventResData
    {
        public List<Event> BlockEvent { get; set; }

        public List<ContractEvent> ContractEvent { get; set; }
    }
}