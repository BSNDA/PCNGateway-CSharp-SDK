using System.Collections.Generic;

namespace bsn_sdk_csharp.Models
{
    /// <summary>
    /// ResponseEntity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NodeApiResBody<T>
    {
        /// <summary>
        /// Response headers
        /// </summary>
        public ResHeader header { get; set; }

        /// <summary>
        /// Response body
        /// </summary>
        public T body { get; set; }

        /// <summary>
        /// Response signature value
        /// </summary>
        public string mac { get; set; }
    }

    /// <summary>
    /// ResopnseEntity
    /// </summary>
    public class NodeApiRes
    {
        /// <summary>
        /// Response header
        /// </summary>
        public ResHeader header { get; set; }

        /// <summary>
        /// Response signature value
        /// </summary>
        public string mac { get; set; }
    }

    /// <summary>
    /// Response header
    /// </summary>
    public class ResHeader
    {
        /// <summary>
        /// Response identification（0：verification success，-1：verification failure）
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// Response information
        /// </summary>
        public string msg { get; set; }
    }

    /// <summary>
    /// Response body of DApp
    /// </summary>
    public class AppInfoResBody
    {
        /// <summary>
        /// Name of DApp
        /// </summary>
        public string appName { get; set; }

        /// <summary>
        /// Type of DApp
        /// </summary>
        public string appType { get; set; }

        /// <summary>
        /// Modes of key management1: Key-Trust Mode 2 Public-Key-Upload Mode
        /// </summary>
        public int caType { get; set; }

        /// <summary>
        /// Key format 1：SM2 2:ECDSA(secp256r1)
        /// </summary>
        public int algorithmType { get; set; }

        /// <summary>
        /// MSPID of the city
        /// </summary>
        public string mspId { get; set; }

        /// <summary>
        /// Change the DApp channel from Fabric to channelId, fisco togroupId
        /// </summary>
        public string channelId { get; set; }

        /// <summary>
        /// version
        /// </summary>
        public string version { get; set; }
    }

    /// <summary>
    /// Return contents of User Registration
    /// </summary>
    public class RegisterUserResBody
    {
        /// <summary>
        /// user id less than 20
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Return to a random password if null
        /// </summary>
        public string secret { get; set; }
    }

    /// <summary>
    /// Return content of certificate registration
    /// </summary>
    public class EnrollUserResBody
    {
        /// <summary>
        /// content of cert
        /// </summary>
        public string cert { get; set; }
    }

    /// <summary>
    /// response content of transaction processing under key management mode
    /// </summary>
    public class ReqChainCodeResBody
    {
        /// <summary>
        /// message block
        /// empty when code is not 0
        /// </summary>
        public Block blockInfo { get; set; }

        /// <summary>
        /// response results of chaincode
        /// empty when code is not 0
        /// </summary>
        public ccRes ccRes { get; set; }
    }

    /// <summary>
    /// message block
    /// </summary>
    public class Block
    {
        /// <summary>
        /// transaction Id
        /// </summary>
        public string txId { get; set; }

        /// <summary>
        /// block hash
        /// syncronization block hash of return interface
        /// </summary>
        public string blockHash { get; set; }

        /// <summary>
        /// Status value
        /// </summary>
        public int status { get; set; }
    }

    /// <summary>
    /// response results of chaincode
    /// empty when code is not 0
    /// </summary>
    public class ccRes
    {
        /// <summary>
        /// response status of chaincode
        /// 200：success 500：failure
        /// </summary>
        public string ccCode { get; set; }

        /// <summary>
        /// response results of chaincode
        /// </summary>
        public string ccData { get; set; }
    }

    /// <summary>
    /// get response body of transations
    /// </summary>
    public class GetTransResBody
    {
        /// <summary>
        /// block hash
        /// </summary>
        public string blockHash { get; set; }

        /// <summary>
        /// block number
        /// </summary>
        public string blockNumber { get; set; }

        /// <summary>
        /// transaction status
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// on-BSN user name
        /// </summary>
        public string createName { get; set; }

        /// <summary>
        ///seconds part of the timestamp
        /// </summary>
        public long timeSpanSec { get; set; }

        /// <summary>
        /// nanoseconds timestamp
        /// </summary>
        public long timeSpanNsec { get; set; }
    }

    /// <summary>
    /// return contents to get the block information
    /// </summary>
    public class GetBlockResBody
    {
        /// <summary>
        /// block hash
        /// </summary>
        public string blockHash { get; set; }

        /// <summary>
        /// block number
        /// </summary>
        public long blockNumber { get; set; }

        /// <summary>
        /// Hash of last block
        /// </summary>
        public string preBlockHash { get; set; }

        /// <summary>
        /// block size in byte
        /// </summary>
        public long blockSize { get; set; }

        /// <summary>
        /// number of transactions in the current block
        /// </summary>
        public int blockTxCount { get; set; }

        /// <summary>
        /// transaction details
        /// </summary>
        public List<TransactionData> transactions { get; set; }
    }

    /// <summary>
    /// return content to get account information
    /// </summary>
    public class GetLedgerResBody
    {
        /// <summary>
        /// Hash block
        /// </summary>
        public string blockHash { get; set; }

        /// <summary>
        /// block height
        /// </summary>
        public long height { get; set; }

        /// <summary>
        /// Hash of last block
        /// </summary>
        public string preBlockHash { get; set; }
    }

    /// <summary>
    /// return content of registered event chaincode
    /// </summary>
    public class EventRegisterResBody
    {
        /// <summary>
        /// event number
        /// </summary>
        public string eventId { get; set; }
    }

    /// <summary>
    /// return content of event chaincode query
    /// </summary>
    public class EventQueryResBody
    {
        /// <summary>
        /// event number
        /// </summary>
        public string eventId { get; set; }

        /// <summary>
        /// chain code of event key
        /// </summary>
        public string eventKey { get; set; }

        /// <summary>
        /// notification address of event chaincode
        /// </summary>
        public string notifyUrl { get; set; }

        /// <summary>
        /// additional paratemeters
        /// </summary>
        public string attachArgs { get; set; }

        /// <summary>
        /// creation time
        /// </summary>
        public string createTime { get; set; }

        /// <summary>
        /// city node
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// user unique identification (UID)
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// DApp UID
        /// </summary>
        public string appCode { get; set; }

        /// <summary>
        /// chain code
        /// </summary>
        public string chainCode { get; set; }
    }

    /// <summary>
    /// trading information
    /// </summary>
    public class TransactionData
    {
        /// <summary>
        /// transaction Id
        /// </summary>
        public string txId { get; set; }

        /// <summary>
        /// transaction status
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// transaction submitter
        /// </summary>
        public string createName { get; set; }

        /// <summary>
        /// seconds timestamp of transaction
        /// </summary>
        public long timeSpanSec { get; set; }

        /// <summary>
        /// nanoseconds timestamp of transaction
        /// </summary>
        public long timeSpanNsec { get; set; }
    }
}