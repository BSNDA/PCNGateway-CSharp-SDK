using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace bsn_sdk_csharp.Models
{
    public class RegisterUserReqDataBody
    {
        public string UserId { get; set; }
    }

    public class CallContractReqDataReqDataBody
    {
        public string UserId { get; set; }
        public string UserAddr { get; set; }

        public string ContractName { get; set; }

        public string FuncName { get; set; }

        public string FuncParam { get; set; }
    }

    public class GetBlockInfoReqDataBody
    {
        public BigInteger BlockHeight { get; set; }
        public string BlockHash { get; set; }
    }

    public class GetTxInfoReqDataBody
    {
        public string TxHash { get; set; }
    }
}