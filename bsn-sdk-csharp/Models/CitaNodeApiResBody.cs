using System.Collections.Generic;
using System.Numerics;

namespace bsn_sdk_csharp.Models
{
    /// <summary>
    /// return contents of block
    /// </summary>
    public class CitaBlockData
    {
        /// <summary>
        /// block hash 
        /// </summary>
        public string BlockHash { get; set; }

        /// <summary>
        /// block number
        /// </summary>
        public BigInteger BlockNumber { get; set; }

        /// <summary>
        /// block creation time
        /// </summary>
        public string BlockTime { get; set; }

        /// <summary>
        ///previous block hash
        /// </summary>
        public string PrevBlockHash { get; set; }

        /// <summary>
        /// quota quantity consumed by transaction
        /// </summary>
        public string QuotaUsed { get; set; }

        /// <summary>
        /// receipt root hash
        /// </summary>
        public string ReceiptsRoot { get; set; }

        /// <summary>
        /// status tree root hash
        /// </summary>
        public string StateRoot { get; set; }

        /// <summary>
        /// transaction data
        /// </summary>
        public List<CitaTransactionData> Transactions { get; set; }

        /// <summary>
        /// transactions root 
        /// </summary>
        public string TransactionsRoot { get; set; }
    }
    /// <summary>
    /// return height of block
    /// </summary>
    public class CitaBlockHeightResBody
    {
        public string Data { get; set; }
    }
    /// <summary>
    /// return event data
    /// </summary>
    public class CitaQueryEventResData
    {
        /// <summary>
        /// block event collection
        /// </summary>
        public List<Event> BlockEvent { get; set; }
        /// <summary>
        /// contract event collection
        /// </summary>
        public List<ContractEvent> ContractEvent { get; set; }
    }
    /// <summary>
    /// return eventId of register event
    /// </summary>
    public class CitaRegisterEventResData
    {
        public string EventId { get; set; }
    }

    /// <summary>
    /// return contents of user registration
    /// </summary>
    public class CitaRegisterUserResBody
    {
        /// <summary>
        /// user address
        /// </summary>
        public string UserAddress { get; set; }

        /// <summary>
        /// user Id
        /// </summary>
        public string UserId { get; set; }
    }

    /// <summary>
    /// return log data of transaction receipt 
    /// </summary>
    public class CitaTranReceiptLogData
    {
        /// <summary>
        /// contract address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// block hash
        /// </summary>
        public string BlockHash { get; set; }
        /// <summary>
        /// block number
        /// </summary>
        public BigInteger BlockNumber { get; set; }
        /// <summary>
        /// block number binary
        /// </summary>
        public string BlockNumberRaw { get; set; }
        /// <summary>
        /// log content filtered by index array
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// log sequence number
        /// </summary>
        public BigInteger LogIndex { get; set; }
        /// <summary>
        /// log sequence number binary
        /// </summary>
        public string LogIndexRaw { get; set; }
        /// <summary>
        /// delete or not
        /// </summary>
        public bool Removed { get; set; }
        /// <summary>
        /// the index array used to construct the filter
        /// </summary>
        public List<string> Topics { get; set; }
        /// <summary>
        /// transaction hash
        /// </summary>
        public string TransactionHash { get; set; }
        /// <summary>
        /// transaction number
        /// </summary>
        public BigInteger TransactionIndex { get; set; }
        /// <summary>
        /// transaction number binary
        /// </summary>
        public string TransactionIndexRaw { get; set; }
        /// <summary>
        /// transaction event number
        /// </summary>
        public string TransactionLogIndex { get; set; }
    }
    /// <summary>
    /// return data of transaction
    /// </summary>
    public class CitaTransactionData
    {
        /// <summary>
        /// chain Id
        /// </summary>
        public string ChainId { get; set; }
        /// <summary>
        /// transaction data
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// transaction sender
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// random number
        /// </summary>
        public string Nonce { get; set; }
        /// <summary>
        /// trading quota
        /// </summary>
        public string Quota { get; set; }
        /// <summary>
        /// transaction recipient
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// transaction hash
        /// </summary>
        public string TxHash { get; set; }
        /// <summary>
        /// maximum block height of transaction on the chain
        /// </summary>
        public string ValidUntilBlock { get; set; }
        /// <summary>
        /// version number
        /// </summary>
        public string Version { get; set; }
    }
    /// <summary>
    /// return transaction results
    /// </summary>
    public class CitaTransResBody
    {
        /// <summary>
        /// Query method returns
        /// transaction data
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// transaction status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// transaction Id
        /// </summary>
        public string TxId { get; set; }
    }

    /// <summary>
    /// return transaction receipt information 
    /// </summary>
    public class CitaTxReceiptResBody
    {
        /// <summary>
        /// block hash
        /// </summary>
        public string BlockHash { get; set; }
        /// <summary>
        /// block number
        /// </summary>
        public BigInteger BlockNumber { get; set; }
        /// <summary>
        /// block number binary
        /// </summary>
        public string BlockNumberRaw { get; set; }
        /// <summary>
        /// contract address
        /// </summary>
        public string ContractAddress { get; set; }
        /// <summary>
        /// accumulated gas used
        /// </summary>
        public string CumulativeGasUsed { get; set; }
        /// <summary>
        /// accumulated gas used binary
        /// </summary>
        public string CumulativeGasUsedRaw { get; set; }
        /// <summary>
        /// the total amount of quota consumed by all transactions before (including the transaction) in the block
        /// </summary>
        public BigInteger CumulativeQuotaUsed { get; set; }
        /// <summary>
        /// the total amount of quota consumed by all transactions before (including the transaction) in the block binary
        /// </summary>
        public string CumulativeQuotaUsedRaw { get; set; }
        /// <summary>
        /// error message
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// transaction sender
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// use gas
        /// </summary>
        public string GasUsed { get; set; }
        /// <summary>
        /// use gas binary
        /// </summary>
        public string GasUsedRaw { get; set; }
        /// <summary>
        /// collection of logs generated by transactions
        /// </summary>
        public List<CitaTranReceiptLogData> Logs { get; set; }
        /// <summary>
        /// logs filter
        /// </summary>
        public string LogsBloom { get; set; }
        /// <summary>
        /// the number of quota consumed by the transaction
        /// </summary>
        public BigInteger QuotaUsed { get; set; }
        /// <summary>
        /// the number of quota consumed by the transaction binary
        /// </summary>
        public string QuotaUsedRaw { get; set; }
        /// <summary>
        /// state tree root
        /// </summary>
        public string Root { get; set; }
        /// <summary>
        /// status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// transaction recipient
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Transaction hash
        /// </summary>
        public string TransactionHash { get; set; }
        /// <summary>
        /// transaction number
        /// </summary>
        public BigInteger TransactionIndex { get; set; }
        /// <summary>
        /// transaction number binary
        /// </summary>
        public string TransactionIndexRaw { get; set; }
    }
}