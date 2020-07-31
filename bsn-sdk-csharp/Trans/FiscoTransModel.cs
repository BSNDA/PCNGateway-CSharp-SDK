using Org.BouncyCastle.Math;

namespace bsn_sdk_csharp.Trans
{
    public class txSign
    {
        public byte[] AccountNonce { get; set; }

        public BigInteger Price { get; set; }

        public BigInteger GasLimit { get; set; }

        public BigInteger BlockLimit { get; set; }

        public byte[] Recipient { get; set; }

        public BigInteger Amount { get; set; }

        public byte[] Payload { get; set; }

        public BigInteger ChainID { get; set; }

        public BigInteger GroupID { get; set; }

        public byte[] ExtraData { get; set; }

        public byte[] Hash { get; set; }

        public byte[] V { get; set; }
        public byte[] R { get; set; }
        public byte[] S { get; set; }
    }

    public class FiscoTransModel
    {
        public txSign data { get; set; }

        public object hash { get; set; }

        public object size { get; set; }
        public object from { get; set; }

        public bool smcrypto { get; set; }
    }
}