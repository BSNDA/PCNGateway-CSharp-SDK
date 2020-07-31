using Org.BouncyCastle.Crypto.Parameters;

namespace bsn_sdk_csharp
{
    public class ECKeyPair
    {
        public ECPrivateKeyParameters prik { get; set; }

        public ECPublicKeyParameters pubk { get; set; }
    }
}