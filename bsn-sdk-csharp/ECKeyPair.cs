using Org.BouncyCastle.Crypto.Parameters;

namespace bsn_sdk_csharp
{
    /// <summary>
    /// ecdsa sign key
    /// </summary>
    public class ECKeyPair
    {
        /// <summary>
        /// private key
        /// </summary>
        public ECPrivateKeyParameters prik { get; set; }

        /// <summary>
        /// public key
        /// </summary>
        public ECPublicKeyParameters pubk { get; set; }
    }
}