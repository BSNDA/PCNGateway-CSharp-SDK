using System.Collections.Generic;

namespace bsn_sdk_csharp.Models
{
    /// <summary>
    /// Request class with body
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NodeApiReqBody<T> : NodeApiReq
    {
        /// <summary>
        /// Request body
        /// </summary>
        public T body { get; set; }
    }

    /// <summary>
    /// Request class
    /// </summary>
    public class NodeApiReq
    {
        /// <summary>
        /// Request head
        /// </summary>
        public ReqHeader header { get; set; }

        /// <summary>
        /// Request signature
        /// </summary>
        public string mac { get; set; }
    }

    /// <summary>
    /// Request head
    /// </summary>
    public class ReqHeader
    {
        /// <summary>
        /// user unique code
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// DApp unique code
        /// </summary>
        public string appCode { get; set; }

        /// <summary>
        /// user ID related to DApp
        /// </summary>
        public string tId { get; set; }
    }

    /// <summary>
    /// content requested by registered user
    /// </summary>
    public class RegisterUserReqBody
    {
        /// <summary>
        /// username  less than 20
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///if the user password is empty, a random password will be generated.
        /// </summary>
        public string secret { get; set; }
        /// <summary>
        /// extended properties
        /// </summary>
        public string extendProperties { get; set; }
    }

    /// <summary>
    /// content requested under Key-Trust Mode
    /// </summary>
    public class ReqChainCodeReqBody
    {
        /// <summary>
        /// user name
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// random character string
        /// 24 bits of random byte data encoded in base64
        /// </summary>
        public string nonce { get; set; }

        /// <summary>
        /// chainCode
        /// </summary>
        public string chainCode { get; set; }

        /// <summary>
        /// function
        /// </summary>
        public string funcName { get; set; }

        /// <summary>
        /// requested parameters
        /// </summary>
        public string[] args { get; set; }

        /// <summary>
        /// temporary data
        /// </summary>
        public Dictionary<string, string> transientData { get; set; }
    }

    /// <summary>
    /// content requested by user certificate registration
    /// </summary>
    public class EnrollUserReqBody
    {  /// <summary>
       /// username less than 20
       /// </summary>
        public string name { get; set; }

        /// <summary>
        ///user password
        /// </summary>
        public string secret { get; set; }

        /// <summary>
        /// cert application files
        /// </summary>
        public string csrPem { get; set; }
    }

    /// <summary>
    /// requests to get transaction information
    /// </summary>
    public class GetTransReqBody
    {
        /// <summary>
        /// transaction Id
        /// </summary>
        public string txId { get; set; }
    }

    /// <summary>
    /// requests to get block information
    /// </summary>
    public class GetBlockReqBody
    {
        /// <summary>
        /// block number cannot be empty at the same time
        /// </summary>
        public long blockNumber { get; set; }

        /// <summary>
        /// block hash cannot be empty at the same time
        /// </summary>
        public string blockHash { get; set; }

        /// <summary>
        /// transaction Id cannot be empty at the same time
        /// </summary>
        public string txId { get; set; }
        /// <summary>
        /// data type 
        /// Optional is' JSON '. If it is JSON, the formatted data will be returned; otherwise, it is Base64 string
        /// </summary>
        public string dataType { get; set; }
    }

    /// <summary>
    /// content requested by event chaincode
    /// </summary>
    public class EventRegisterReqBody
    {
        /// <summary>
        /// chainCode
        /// </summary>
        public string chainCode { get; set; }

        /// <summary>
        /// key
        /// </summary>
        public string eventKey { get; set; }

        /// <summary>
        ///notification address of event chaincode
        /// </summary>
        public string notifyUrl { get; set; }

        /// <summary>
        /// additional parameters
        /// </summary>
        public string attachArgs { get; set; }
    }

    /// <summary>
    /// content requested by event chaincode logout
    /// </summary>
    public class EventRemoveReqBody
    {
        /// <summary>
        /// event code
        /// </summary>
        public string eventId { get; set; }
    }

    /// <summary>
    /// notification of event chaincode
    /// </summary>
    public class EventNotifyReqBody
    {
        /// <summary>
        /// chainCode
        /// </summary>
        public string chainCode { get; set; }

        /// <summary>
        /// city code
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// key of registered chaincode
        /// </summary>
        public string eventKey { get; set; }

        /// <summary>
        /// event ID of registered chaincode
        /// </summary>
        public string eventId { get; set; }

        /// <summary>
        /// parameters of registered event chaincode
        /// additional parameters of registration
        /// </summary>
        public string attachArgs { get; set; }

        /// <summary>
        /// key of monitored event chaincode
        /// event name set in the chaincode
        /// </summary>
        public string eventName { get; set; }

        /// <summary>
        /// transaction Id of the current link code
        /// </summary>
        public string txId { get; set; }

        /// <summary>
        /// event value of monitored chaincode
        /// </summary>
        public string payload { get; set; }

        /// <summary>
        /// block height of current transaction
        /// </summary>
        public long blockNumber { get; set; }

        /// <summary>
        ///Response random code
        /// The business platform can determine whether it has received notifications based on this value, and the string remains the same for multiple notifications of the same business
        /// </summary>
        public string nonceStr { get; set; }
    }

    /// <summary>
    /// content requested by transaction under Public-Key-Upload Mode
    /// </summary>
    public class TransReqBody
    {
        /// <summary>
        /// transaction data
        /// </summary>
        public string transData { get; set; }
    }
}