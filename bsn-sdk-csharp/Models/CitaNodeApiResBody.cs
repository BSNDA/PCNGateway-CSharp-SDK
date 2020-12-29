using System.Collections.Generic;
using System.Numerics;

namespace bsn_sdk_csharp.Models
{
    public class CitaRegisterUserResBody
    {
        public string UserId { get; set; }

        public string UserAddress { get; set; }
    }

    public class CitaTxReceiptResBody
    {
        public string TransactionHash { get; set; }
        public BigInteger TransactionIndex { get; set; }

        public string BlockHash { get; set; }

        public BigInteger BlockNumber { get; set; }
        public string CumulativeGasUsed { get; set; }

        public BigInteger CumulativeQuotaUsed { get; set; }
        public string GasUsed { get; set; }

        public BigInteger QuotaUsed { get; set; }

        public string ContractAddress { get; set; }

        public string Root { get; set; }
        public string Status { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string LogsBloom { get; set; }
        public string ErrorMessage { get; set; }
        public string TransactionIndexRaw { get; set; }
        public string BlockNumberRaw { get; set; }
        public string CumulativeGasUsedRaw { get; set; }
        public string CumulativeQuotaUsedRaw { get; set; }
        public string GasUsedRaw { get; set; }

        public string QuotaUsedRaw { get; set; }

        public List<CitaTranReceiptLogData> Logs { get; set; }
    }

    public class CitaTranReceiptLogData
    {
        public bool Removed { get; set; }
        public BigInteger LogIndex { get; set; }
        public BigInteger TransactionIndex { get; set; }
        public string TransactionHash { get; set; }
        public string BlockHash { get; set; }
        public BigInteger BlockNumber { get; set; }
        public string Address { get; set; }
        public string Data { get; set; }
        public string TransactionLogIndex { get; set; }
        public string TransactionIndexRaw { get; set; }
        public string BlockNumberRaw { get; set; }
        public string LogIndexRaw { get; set; }
        public List<string> Topics { get; set; }
    }

    public class CitaTransactionData
    {
        public string TxHash { get; set; }

        public string Data { get; set; }
        public string ChainId { get; set; }

        public string Quota { get; set; }

        public string From { get; set; }
        public string To { get; set; }

        public string Nonce { get; set; }

        public string ValidUntilBlock { get; set; }
        public string Version { get; set; }
    }

    public class CitaBlockData
    {
        public string BlockHash { get; set; }

        public BigInteger BlockNumber { get; set; }

        public string PrevBlockHash { get; set; }

        public string BlockTime { get; set; }

        public string QuotaUsed { get; set; }

        public string TransactionsRoot { get; set; }
        public string StateRoot { get; set; }
        public string ReceiptsRoot { get; set; }

        public List<CitaTransactionData> Transactions { get; set; }
    }

    public class CitaBlockHeightResBody
    {
        public string data { get; set; }
    }

    public class CitaTransResBody
    {
        public string TxId { get; set; }

        public string Status { get; set; }

        public string Data { get; set; }
    }

    public class CitaRegisterEventResData
    {
        public string EventId { get; set; }
    }

    public class CitaQueryEventResData
    {
        public List<Event> BlockEvent { get; set; }

        public List<ContractEvent> ContractEvent { get; set; }
    }
}