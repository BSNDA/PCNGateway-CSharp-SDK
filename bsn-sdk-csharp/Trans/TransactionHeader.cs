using bsn_sdk_csharp.Ecdsa;
using bsn_sdk_csharp.Lib;

namespace bsn_sdk_csharp.Trans
{
    public class TransactionHeader
    {
        public string Id { get; set; }

        public Google.Protobuf.ByteString Creator { get; set; }
        public Google.Protobuf.ByteString Nonce { get; set; }
        public string ChannelID { get; set; }

        public TransactionHeader(string mspid, string cert, string channelId)
        {
            if (cert.Contains(".pem"))
            {
                cert = ECDSAHelper.ReadPK(cert);
            }
            this.SetCreator(mspid, cert);
            SetTxId(RandomHelper.GetRandomNonceByte());
            this.ChannelID = channelId;
        }

        private void SetCreator(string mspid, string cert)
        {
            var serializedIdentity = new Msp.SerializedIdentity()
            {
                IdBytes = Util.ConvertToByteString(cert),
                Mspid = mspid
            };
            this.Creator = Util.Marshal(serializedIdentity);
        }

        private void SetTxId(byte[] nonce)
        {
            this.Nonce = Util.ConvertToByteString(nonce);
            this.Id = Util.ConvertSHA256String(nonce, this.Creator.ToByteArray());
        }
    }
}