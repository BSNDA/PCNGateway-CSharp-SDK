using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.IO;
using System.Text;

namespace bsn_sdk_csharp.SM2
{
    public class SM2Utils
    {
        /// <summary>
        /// 生成国密公私钥对
        /// </summary>
        public static AsymmetricCipherKeyPair GenerateKeyPair()
        {
            var keyGenerator = new ECKeyPairGenerator();

            ECKeyGenerationParameters pa = new ECKeyGenerationParameters(GMObjectIdentifiers.sm2p256v1, new SecureRandom());
            keyGenerator.Init(pa);
            return keyGenerator.GenerateKeyPair();
        }

        public static void SaveKey(AsymmetricKeyParameter key, string url)
        {
            Org.BouncyCastle.Asn1.Pkcs.PrivateKeyInfo privateKeyInfo = Org.BouncyCastle.Pkcs.PrivateKeyInfoFactory.CreatePrivateKeyInfo(key);
            Org.BouncyCastle.Utilities.IO.Pem.PemObject pemObj = new Org.BouncyCastle.Utilities.IO.Pem.PemObject("PRIVATE KEY", privateKeyInfo.ToAsn1Object().GetEncoded());
            TextWriter textkey = new StringWriter();
            PemWriter pemkey = new PemWriter(textkey);
            pemkey.WriteObject(pemObj);
            pemkey.Writer.Flush();
            string pri = textkey.ToString();
            pemkey.WriteObject(key);
            pemkey.Writer.Flush();
            byte[] priInfoByte = System.Text.Encoding.UTF8.GetBytes(textkey.ToString());
            FileStream fs = new FileStream(url, FileMode.Create, FileAccess.Write);
            fs.Write(priInfoByte, 0, priInfoByte.Length);
            fs.Close();
        }

        public static byte[] SM3Hash(byte[] message)
        {
            return DigestUtilities.CalculateDigest("SM3", message);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Encrypt(byte[] publicKey, byte[] data)
        {
            if (null == publicKey || publicKey.Length == 0)
            {
                return null;
            }
            if (data == null || data.Length == 0)
            {
                return null;
            }

            byte[] source = new byte[data.Length];
            Array.Copy(data, 0, source, 0, data.Length);

            Cipher cipher = new Cipher();
            SM2 sm2 = SM2.Instance;

            ECPoint userKey = sm2.ecc_curve.DecodePoint(publicKey);

            ECPoint c1 = cipher.Init_enc(sm2, userKey);
            cipher.Encrypt(source);

            byte[] c3 = new byte[32];
            cipher.Dofinal(c3);

            string sc1 = Encoding.ASCII.GetString(Hex.Encode(c1.GetEncoded()));
            string sc2 = Encoding.ASCII.GetString(Hex.Encode(source));
            string sc3 = Encoding.ASCII.GetString(Hex.Encode(c3));

            return (sc1 + sc2 + sc3).ToUpper();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="encryptedData"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] privateKey, byte[] encryptedData)
        {
            if (null == privateKey || privateKey.Length == 0)
            {
                return null;
            }
            if (encryptedData == null || encryptedData.Length == 0)
            {
                return null;
            }

            string data = Encoding.ASCII.GetString(Hex.Encode(encryptedData));

            byte[] c1Bytes = Hex.Decode(Encoding.ASCII.GetBytes(data.Substring(0, 130)));
            int c2Len = encryptedData.Length - 97;
            byte[] c2 = Hex.Decode(Encoding.ASCII.GetBytes(data.Substring(130, 2 * c2Len)));
            byte[] c3 = Hex.Decode(Encoding.ASCII.GetBytes(data.Substring(130 + 2 * c2Len, 64)));

            SM2 sm2 = SM2.Instance;
            BigInteger userD = new BigInteger(1, privateKey);

            ECPoint c1 = sm2.ecc_curve.DecodePoint(c1Bytes);
            Cipher cipher = new Cipher();
            cipher.Init_dec(userD, c1);
            cipher.Decrypt(c2);
            cipher.Dofinal(c3);

            return c2;
        }

        /// <summary>
        ///  使用私钥进行签名
        /// </summary>
        /// <param name="macdata">签名原串</param>
        /// <param name="privateKey">私钥</param>
        /// <returns></returns>
        public static byte[] Sign(byte[] macdata, AsymmetricKeyParameter privateKey, out BigInteger R, out BigInteger S)
        {
            Org.BouncyCastle.Crypto.Signers.SM2Signer signer = new Org.BouncyCastle.Crypto.Signers.SM2Signer();
            signer.Init(true, privateKey);
            signer.BlockUpdate(macdata, 0, macdata.Length);
            byte[] sign = signer.GenerateSignature();

            Asn1Sequence sequence = Asn1Sequence.GetInstance(sign);
            DerInteger r = (DerInteger)sequence[0];
            DerInteger s = (DerInteger)sequence[1];
            R = r.Value;
            S = s.Value;
            BigInteger[] bigs = new BigInteger[] { r.Value, s.Value };

            byte[] bs;
            using (MemoryStream ms = new MemoryStream())
            {
                DerSequenceGenerator seq = new DerSequenceGenerator(ms);
                seq.AddObject(new DerInteger(bigs[0]));
                seq.AddObject(new DerInteger(bigs[1]));
                seq.Close();
                bs = ms.ToArray();
            }
            return bs;
        }

        /// <summary>
        ///  使用私钥进行签名
        /// </summary>
        /// <param name="macdata">签名原串</param>
        /// <param name="privateKey">私钥</param>
        /// <returns></returns>
        public static byte[] Sign(byte[] macdata, AsymmetricKeyParameter privateKey)
        {
            Org.BouncyCastle.Crypto.Signers.SM2Signer signer = new Org.BouncyCastle.Crypto.Signers.SM2Signer();
            signer.Init(true, privateKey);
            signer.BlockUpdate(macdata, 0, macdata.Length);
            byte[] sign = signer.GenerateSignature();
            return sign;
            Asn1Sequence sequence = Asn1Sequence.GetInstance(sign);
            DerInteger r = (DerInteger)sequence[0];
            DerInteger s = (DerInteger)sequence[1];

            BigInteger[] bigs = new BigInteger[] { r.Value, s.Value };

            byte[] bs;
            using (MemoryStream ms = new MemoryStream())
            {
                DerSequenceGenerator seq = new DerSequenceGenerator(ms);
                seq.AddObject(new DerInteger(bigs[0]));
                seq.AddObject(new DerInteger(bigs[1]));
                seq.Close();
                bs = ms.ToArray();
            }
            return bs;
        }

        /// <summary>
        /// 使用公钥对待验签数据进行验签
        /// </summary>
        /// <param name="data">参与验签字符</param>
        /// <param name="signature">要验签的签名串</param>
        /// <param name="pkInfo">公钥信息</param>
        /// <returns>返回验签结果</returns>
        public static bool VerifyData(byte[] data, byte[] signature, AsymmetricKeyParameter pkInfo)
        {
            Org.BouncyCastle.Crypto.Signers.SM2Signer signer = new Org.BouncyCastle.Crypto.Signers.SM2Signer();

            signer.Init(false, pkInfo);
            signer.BlockUpdate(data, 0, data.Length);
            return signer.VerifySignature(signature);
        }
    }
}