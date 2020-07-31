using bsn_sdk_csharp.Common;
using bsn_sdk_csharp.Sign;
using System;

namespace bsn_sdk_csharp.Ecdsa
{
    public class ECDSAHandle : SignHandle
    {
        public ECKeyPair key { get; set; }

        public ECDSAHandle(string _prik, string _pubk)
        {
            key = new ECKeyPair();
            key.prik = LibraryHelper.loadprikey(_prik);
            key.pubk = LibraryHelper.loadpubkey(_pubk);
        }

        public byte[] Hash(byte[] msg)
        {
            throw new NotImplementedException();
        }

        public byte[] Sign(byte[] digest)
        {
            string curveName = "P-256";
            var nistCurve = Org.BouncyCastle.Asn1.Nist.NistNamedCurves.GetByName(curveName);

            var sign = nistCurve.Sign(key.prik, digest);
            return sign;
        }

        public bool Verify(byte[] sign, byte[] digest)
        {
            string curveName = "P-256";
            var nistCurve = Org.BouncyCastle.Asn1.Nist.NistNamedCurves.GetByName(curveName);

            return nistCurve.Verify(key.pubk, digest, sign);
        }
    }
}