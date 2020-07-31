using bsn_sdk_csharp.Common;
using bsn_sdk_csharp.ETH;
using bsn_sdk_csharp.Lib;
using bsn_sdk_csharp.SM2;
using Nethereum.ABI.Model;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;

namespace bsn_sdk_csharp.Trans
{
    public class FiscoTrans
    {
        public static BigInteger GAS_PRICE = new BigInteger("22000000000");
        public static BigInteger GAS_LIMIT = new BigInteger("4300000");
        public static BigInteger ChainId = new BigInteger("1");

        public static ContractABI TransContract(string ContractAbi)
        {
            Nethereum.ABI.JsonDeserialisation.ABIDeserialiser json = new Nethereum.ABI.JsonDeserialisation.ABIDeserialiser();
            var aa = json.DeserialiseContract(ContractAbi);
            return aa;
        }

        public string GetSig(FunctionABI func)
        {
            List<Parameter> list = new List<Parameter>(func.OutputParameters);

            return string.Format("{0}({1})", func.Name, string.Join(',', list.ConvertAll(a => a.Type)));
        }

        public static Tuple<string, string> TransData(string contractabi, string contractAddress, string funcName, object[] args,
            BigInteger groupId, BigInteger blockLimit, byte[] extraData, bool smcrypto, ECKeyPair key)
        {
            try
            {
                if (string.IsNullOrEmpty(funcName))
                {
                    return new Tuple<string, string>("", "contract not has function");
                }
                Nethereum.ABI.JsonDeserialisation.ABIDeserialiser abijson = new Nethereum.ABI.JsonDeserialisation.ABIDeserialiser();
                var abi = abijson.DeserialiseContract(contractabi);
                var funcData = Pack(abi, funcName, args, smcrypto);
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
                var toAddress = Hex.Decode(contractAddress.Replace("0x", ""));
                var nonce = RandomHelper.GetRandomBytes(32);
                var tx = NewTransaction(nonce, toAddress, null, GAS_LIMIT, GAS_PRICE, blockLimit, funcData, ChainId, groupId, extraData, smcrypto);
                var dataByte = SignData(tx, key);
                return new Tuple<string, string>(Hex.ToHexString(dataByte), string.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FiscoTransModel NewTransaction(byte[] nonce, byte[] to, BigInteger amount, BigInteger gasLimit,
            BigInteger gasPrice, BigInteger blockLimit, byte[] data, BigInteger chainId, BigInteger groupId, byte[] extraData, bool smcrypto)
        {
            return new FiscoTransModel()
            {
                data = new txSign()
                {
                    AccountNonce = nonce,
                    Recipient = to,
                    Payload = data,
                    Amount = amount,
                    GasLimit = gasLimit,
                    BlockLimit = blockLimit,
                    Price = gasPrice,
                    ChainID = chainId,
                    GroupID = groupId,
                    ExtraData = extraData,
                },
                smcrypto = smcrypto
            };
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

        private static byte[] SignData(FiscoTransModel trans, ECKeyPair key)
        {
            byte[][] b = new byte[10][];
            b[0] = trans.data.AccountNonce;
            b[1] = LibraryHelper.FromBigInteger(trans.data.Price);
            b[2] = LibraryHelper.FromBigInteger(trans.data.GasLimit);
            b[3] = LibraryHelper.FromBigInteger(trans.data.BlockLimit);
            b[4] = trans.data.Recipient;
            b[5] = trans.data.Amount == null ? null : LibraryHelper.FromBigInteger(trans.data.Amount);
            b[6] = trans.data.Payload;
            b[7] = LibraryHelper.FromBigInteger(trans.data.ChainID);
            b[8] = LibraryHelper.FromBigInteger(trans.data.GroupID);
            b[9] = trans.data.ExtraData;

            var txb = Nethereum.RLP.RLP.EncodeElementsAndList(b);
            if (trans.smcrypto)
            {
                BigInteger r, s;
                SM2Utils.Sign(SM2Utils.SM3Hash(txb), key.prik, out r, out s);
                trans.data.R = LibraryHelper.FromBigInteger(r);
                trans.data.S = LibraryHelper.FromBigInteger(s);

                var pub1 = key.pubk.Q.XCoord.GetEncoded();
                var pub2 = key.pubk.Q.YCoord.GetEncoded();
                byte[] v = new byte[pub1.Length + pub2.Length];
                Array.Copy(pub1, 0, v, 0, pub1.Length);
                Array.Copy(pub2, 0, v, pub1.Length, pub2.Length);
                Console.WriteLine(string.Format("sign.V:{0}", Hex.ToHexString(v)));
                trans.data.V = v;
            }
            else
            {
                var bytes = EthUtils.ConvertSHA256byte(txb);
                var sign = EthUtils.Sign(bytes, key.prik);
                trans.data.R = sign.Item1;
                trans.data.S = sign.Item2;
                trans.data.V = sign.Item3;
            }
            byte[][] si = new byte[b.Length + 3][];
            Array.Copy(b, 0, si, 0, b.Length);
            si[b.Length] = trans.data.V;
            si[b.Length + 1] = trans.data.R;
            si[b.Length + 2] = trans.data.S;

            var txs = Nethereum.RLP.RLP.EncodeElementsAndList(si);

            return txs;
        }
    }
}