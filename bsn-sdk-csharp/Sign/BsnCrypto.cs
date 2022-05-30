using System;
using System.Text;

namespace bsn_sdk_csharp.Sign
{
    public interface Crypto
    {
        public byte[] Hash(byte[] msg);

        public string Sign(string value);

        public byte[] Sign(byte[] value);

        public bool Verify(string mac, string value);
    }

    public class BsnCrypto : Crypto
    {
        public SignHandle sign;

        public BsnCrypto(SignHandle _sign)
        {
            sign = _sign;
        }

        public byte[] Hash(byte[] msg)
        {
            return sign.Hash(msg);
        }

        public string Sign(string value)
        {
            byte[] digest = Encoding.UTF8.GetBytes(value);
            var mac = sign.Sign(digest);
            return Convert.ToBase64String(mac);
        }
        public byte[] Sign(byte[] value)
        {
            var mac = sign.Sign(value);
            return mac;
        }

        public bool Verify(string mac, string value)
        {
            byte[] digest = Encoding.UTF8.GetBytes(value);
            byte[] macData = Convert.FromBase64String(mac);
            return sign.Verify(macData, digest);
        }
    }
}