using bsn_sdk_csharp.Lib;
using bsn_sdk_csharp.SM2;
using Nethereum.ABI.Model;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bsn_sdk_csharp.Trans
{
    public class CitaTrans
    {
        private static byte[] value = Enumerable.Repeat((byte)0x0, 32).ToArray();

        public static Tuple<string, string> TransData(string contractabi, string contractAddress, string funcName, object[] args,
            string channelId, ulong blockLimit, uint version, ulong quota, bool smcrypto, ECKeyPair key)
        {
            try
            {
                if (string.IsNullOrEmpty(funcName))
                {
                    return new Tuple<string, string>("", "contract not has function");
                }
                Nethereum.ABI.JsonDeserialisation.ABIDeserialiser abijson = new Nethereum.ABI.JsonDeserialisation.ABIDeserialiser();
                var abi = abijson.DeserialiseContract(contractabi);
                var funcData = Pack(abi, funcName, args, false);
                FunctionABI method = null;
                foreach (var item in abi.Functions)
                {
                    if (item.Name == funcName)
                    {
                        method = item;
                    }
                }
                if (method.Constant)
                {
                    return new Tuple<string, string>(Hex.ToHexString(funcData), "");
                }
                var nonce = RandomHelper.GetRandomNonce(32);
                var tx = NewTransaction(nonce, contractAddress, quota, version, blockLimit, funcData, channelId);
                Console.WriteLine(string.Format("tx:{0}", Hex.ToHexString(Util.Marshal(tx).ToByteArray())));
                var dataByte = SignData(tx, smcrypto, key);

                var trans = new UnverifiedTransaction()
                {
                    Transaction = tx,
                    Signature = Util.ConvertToByteString(dataByte),
                    Crypto = CitaCrypto.Default
                };
                return new Tuple<string, string>(Hex.ToHexString(Util.Marshal(trans).ToByteArray()), string.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CitaTransaction NewTransaction(string nonce, string to, ulong quota,
            uint version, ulong blockLimit, byte[] data, string chainId)
        {
            var transaction = new CitaTransaction()
            {
                Nonce = nonce,
                Quota = quota,
                Version = version,
                Value = Util.ConvertToByteString(value),
                Data = Util.ConvertToByteString(data),
                ValidUntilBlock = blockLimit
            };

            if (version == 0)
            {
                transaction.To = to;
                transaction.ChainId = uint.Parse(chainId);
            }
            else
            {
                transaction.ToV1 = Util.ConvertToByteString(hexStringToBytes(to));

                transaction.ChainIdV1 = Util.ConvertToByteString(ToBytes32(hexStringToBytes(chainId)));
            }
            return transaction;
        }

        public static byte[] Pack(ContractABI abi, string funcName, object[] args, bool sm)
        {
            if (sm)
            {
                Nethereum.ABI.FunctionEncoding.ParametersEncoder pe = new Nethereum.ABI.FunctionEncoding.ParametersEncoder();

                if (string.IsNullOrEmpty(funcName))
                {
                    return pe.EncodeParameters(abi.Constructor.InputParameters, args);
                }
                else
                {
                    byte[] arguments = null, id = null;
                    foreach (var item in abi.Functions)
                    {
                        if (item.Name == funcName)
                        {
                            id = GetMethodId(item);
                            arguments = pe.EncodeParameters(item.InputParameters, args);
                        }
                    }
                    byte[] hash = new byte[arguments.Length + id.Length];
                    Array.Copy(id, 0, hash, 0, id.Length);
                    Array.Copy(arguments, 0, hash, id.Length, arguments.Length);
                    return hash;
                }
            }
            else
            {
                Nethereum.ABI.FunctionEncoding.ParametersEncoder pe = new Nethereum.ABI.FunctionEncoding.ParametersEncoder();

                if (string.IsNullOrEmpty(funcName))
                {
                    return pe.EncodeParameters(abi.Constructor.InputParameters, args);
                }
                else
                {
                    byte[] arguments = null, id = null;
                    foreach (var item in abi.Functions)
                    {
                        if (item.Name == funcName)
                        {
                            id = Hex.Decode(item.Sha3Signature);
                            arguments = pe.EncodeParameters(item.InputParameters, args);
                        }
                    }
                    byte[] hash = new byte[arguments.Length + id.Length];
                    Array.Copy(id, 0, hash, 0, id.Length);
                    Array.Copy(arguments, 0, hash, id.Length, arguments.Length);
                    return hash;
                }
            }
        }

        public static byte[] GetMethodId(FunctionABI func)
        {
            List<Parameter> list = new List<Parameter>(func.InputParameters);
            byte[] hash = DigestUtilities.CalculateDigest
                ("SM3", System.Text.Encoding.UTF8.GetBytes(string.Format("{0}({1})", func.Name,
                string.Join(',', list.ConvertAll(a => a.Type)))));
            byte[] id = new byte[4];
            Array.Copy(hash, id, 4);
            return id;
        }

        private static byte[] SignData(CitaTransaction trans, bool sm, ECKeyPair key)
        {
            var tx = Util.MarshalByte(trans);
            if (sm)
            {
                var publicKeyStr = fillStr64(formatHexString(Hex.ToHexString(key.pubk.Q.XCoord.ToBigInteger().ToByteArray())))
                    + fillStr64(formatHexString(Hex.ToHexString(key.pubk.Q.YCoord.ToBigInteger().ToByteArray())));
                Console.WriteLine(string.Format("publicKeyStr:{0}", publicKeyStr));
                BigInteger r, s;
                SM2Utils.Sign(SM2Utils.SM3Hash(tx), key.prik, out r, out s);
                var signStr = fillStr64(formatHexString(Hex.ToHexString(r.ToByteArray()))) + fillStr64(formatHexString(Hex.ToHexString(s.ToByteArray())));
                var publicKeyBytes = Hex.Decode(publicKeyStr);
                var signBytes = Hex.Decode(signStr);
                var byteSource = new byte[signBytes.Length + publicKeyBytes.Length];
                signBytes.CopyTo(byteSource, 0);
                publicKeyBytes.CopyTo(byteSource, signBytes.Length);
                return byteSource;
            }
            return null;
        }

        private static byte[] ToBytes32(byte[] b)
        {
            byte[] value = Enumerable.Repeat((byte)0x0, 32).ToArray();
            var blenth = b.Length;
            if (blenth >= 32)
            {
                return b;
            }
            for (int i = 0; i < blenth; i++)
            {
                value[32 - (blenth - i)] = b[i];
            }
            return value;
        }

        private static byte[] hexStringToBytes(string src)
        {
            if (src.Substring(0, 2).ToLower() == "0x")
            {
                src = src.Substring(2, src.Length - 2);
            }
            src = (src.Length % 2 == 1 ? "0" : "") + src;

            return Hex.Decode(src);
        }

        private static string formatHexString(string str)
        {
            if (str.Substring(0, 2) == "00")
            {
                return str.Substring(2, str.Length - 2);
            }
            else
            {
                return str;
            }
        }

        private static string fillStr64(string str)
        {
            if (str.Length >= 64)
            {
                return str;
            }
            else
            {
                return str.PadLeft(64, '0');
            }
        }
    }
}