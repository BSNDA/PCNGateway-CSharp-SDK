﻿using bsn_sdk_csharp.Ecdsa;
using bsn_sdk_csharp.Enum;
using bsn_sdk_csharp.Sign;
using bsn_sdk_csharp.SM2;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace bsn_sdk_csharp.Lib
{
    public class LibraryHelper
    {
        /// <summary>
        /// convert byte to BigInteger
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static BigInteger ToBigInteger(byte[] v)
        {
            byte[] r = new byte[v.Length + 1];
            Array.Copy(v.Reverse().ToArray(), 0, r, 0, v.Length);
            return new BigInteger(r);
        }

        /// <summary>
        /// convert BigInteger to byte
        /// </summary>
        /// <param name="bi"></param>
        /// <returns></returns>
        public static byte[] FromBigInteger(BigInteger bi)
        {
            byte[] array = bi.ToByteArray();
            if (array[0] == 0)
            {
                byte[] tmp = new byte[array.Length - 1];
                System.Array.Copy(array, 1, tmp, 0, tmp.Length);
                array = tmp;
            }
            return array;
        }

        /// <summary>
        /// load private key
        /// </summary>
        /// <param name="privateKey">private key or private key file path</param>
        /// <returns>private key</returns>
        public static ECPrivateKeyParameters LoadPrikey(string privateKey)
        {
            string prik;
            if (!privateKey.Contains("PRIVATE KEY"))
            {
                prik = ReadPK(privateKey);
            }
            else
            {
                prik = privateKey;
            }
            TextReader ptr = new StringReader(prik);
            Org.BouncyCastle.OpenSsl.PemReader pem = new Org.BouncyCastle.OpenSsl.PemReader(ptr);
            ECPrivateKeyParameters sm2PrivateKey = (ECPrivateKeyParameters)pem.ReadObject();

            return sm2PrivateKey;
        }

        /// <summary>
        /// load public key
        /// </summary>
        /// <param name="pkInfo">public key or public key file path</param>
        /// <returns>public key</returns>
        public static ECPublicKeyParameters LoadPubkey(string pkInfo)
        {
            string pub;
            if (!pkInfo.Contains("CERTIFICATE") && !pkInfo.Contains("PUBLIC KEY"))
            {
                pub = ReadPK(pkInfo);
            }
            else
            {
                pub = pkInfo;
            }
            TextReader ptr = new StringReader(pub);
            Org.BouncyCastle.OpenSsl.PemReader pem = new Org.BouncyCastle.OpenSsl.PemReader(ptr);
            ECPublicKeyParameters sm2PublicKey = (ECPublicKeyParameters)pem.ReadObject();

            return sm2PublicKey;
        }

        /// <summary>
        /// read key file
        /// </summary>
        /// <param name="keyUrl">file path</param>
        /// <returns>content of file</returns>
        public static string ReadPK(string keyUrl)
        {
            string pk = string.Empty;
            FileStream fileStream = new FileStream(keyUrl, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                pk = reader.ReadToEnd();
            }
            return pk;
        }

        /// <summary>
        /// read file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFile(string path)
        {
            string content = string.Empty;
            FileStream fileStream = new FileStream(path, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                content = reader.ReadToEnd();
            }
            return content;
        }

        /// <summary>
        /// set sign handle
        /// </summary>
        /// <param name="algorithmType"></param>
        /// <param name="puk"></param>
        /// <param name="pri"></param>
        /// <returns></returns>
        public static Crypto SetAlgorithm(EmAlgorithmType algorithmType, string puk, string pri)
        {
            switch (algorithmType.Value)
            {
                case 1:
                    var handle = new SM2Handle(pri, puk);
                    return new BsnCrypto(handle);

                case 2:
                case 3:
                    var handleEcdsa = new ECDSAHandle(pri, puk);
                    return new BsnCrypto(handleEcdsa);

                default:
                    return null;
            }
        }
    }
}