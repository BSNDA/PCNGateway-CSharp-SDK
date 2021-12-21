using bsn_sdk_csharp.Lib;
using bsn_sdk_csharp.Sign;
using Org.BouncyCastle.Security;

namespace bsn_sdk_csharp.SM2
{
    public class SM2Handle : SignHandle
    {
        public ECKeyPair key;

        public SM2Handle(string _prik, string _pubk)
        {
            key = new ECKeyPair();
            key.prik = LibraryHelper.LoadPrikey(_prik);
            key.pubk = LibraryHelper.LoadPubkey(_pubk);
        }

        public byte[] Hash(byte[] msg)
        {
            return DigestUtilities.CalculateDigest("SM3", msg);
        }

        public byte[] Sign(byte[] digest)
        {
            return SM2Utils.Sign(Hash(digest), key.prik);
        }

        public bool Verify(byte[] sign, byte[] digest)
        {
            return SM2Utils.VerifyData(Hash(digest), sign, key.pubk);
        }
    }
}