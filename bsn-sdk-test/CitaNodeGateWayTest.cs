using bsn_sdk_csharp;
using bsn_sdk_csharp.Enum;
using bsn_sdk_csharp.NodeExtends;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Org.BouncyCastle.Utilities.Encoders;
using System;

namespace bsn_sdk_test
{
    [TestClass]
    public class CitaNodeGateWayTest
    {
        /// <summary>
        /// get DApp service information
        /// </summary>
        [TestMethod]
        public void GetAppInfo()
        {
            var config = CitaConfig.NewMockConfig();
            Console.WriteLine(JsonConvert.SerializeObject(config));
            Assert.IsNotNull(config);
        }

        [TestMethod]
        public void RegisterUser()
        {
            var config = CitaConfig.NewMockConfig();
            var res = new CitaNodeServer(config).RegisterUser(new bsn_sdk_csharp.Models.CitaRegisterReqBody()
            {
                UserId = "test10281523"
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetBlockInfo()
        {
            var config = CitaConfig.NewMockConfig();
            var res = new CitaNodeServer(config).GetBlockInfo(new bsn_sdk_csharp.Models.CitaBlockReqDataBody()
            {
                //blockNumber = "5"
                BlockHash = "0xdcd3c2ee803f43497210cb91e5b3b16f9ebf65f3ef42e9bc43d3a94bc98373ca"
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetBlockHeight()
        {
            var config = CitaConfig.NewMockConfig();
            var res = new CitaNodeServer(config).GetBlockHeight();

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetTxReceiptByTxHash()
        {
            var config = CitaConfig.NewMockConfig();
            var res = new CitaNodeServer(config).GetTxReceiptByTxHash(new bsn_sdk_csharp.Models.CitaTxReqDataBody()
            {
                TxHash = "0x6d67b327831b237259b3f3c3ed96a51f2aab398578b6da78e13d47b6472b4410"
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetTxInfoByTxHash()
        {
            var config = CitaConfig.NewMockConfig();
            var res = new CitaNodeServer(config).GetTxInfoByTxHash(new bsn_sdk_csharp.Models.CitaTxReqDataBody()
            {
                TxHash = "0x5ce10a6c69e70a23f73aa828ddd668794e6f4e94b215d262a708154eedc1e2a1"
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void ReqChainCode_Insert()
        {
            var config = CitaConfig.NewMockConfig();
            object[] args = new object[2];
            args[0] = Hex.ToHexString(System.Text.Encoding.Default.GetBytes("test1023")).PadLeft(64, '0');

            args[1] = Hex.ToHexString(System.Text.Encoding.Default.GetBytes("abcdftyyyy"));

            var res = new CitaNodeServer(config).ReqChainCode(new bsn_sdk_csharp.Models.CitaTransReqDataBody()
            {
                UserId = "testcurel",
                ContractName = "CitaBsnBaseContract",
                FuncName = "insert",
                FuncParam = JsonConvert.SerializeObject(args)
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void ReqChainCode_Get()
        {
            var config = CitaConfig.NewMockConfig();
            object[] args = new object[2];
            args[0] = Hex.ToHexString(System.Text.Encoding.Default.GetBytes("test10281456")).PadLeft(64, '0');

            args[1] = Hex.ToHexString(System.Text.Encoding.Default.GetBytes("abcdf"));
            var res = new CitaNodeServer(config).ReqChainCode(new bsn_sdk_csharp.Models.CitaTransReqDataBody()
            {
                UserId = "zhmtest1",
                ContractName = "CitaBsnBaseContract",
                FuncName = "retrieve",
                FuncParam = JsonConvert.SerializeObject(args)
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void ReqChainCode_Update()
        {
            var config = CitaConfig.NewMockConfig();
            object[] args = new object[2];
            args[0] = Hex.ToHexString(System.Text.Encoding.Default.GetBytes("test10281456")).PadLeft(64, '0');

            args[1] = Hex.ToHexString(System.Text.Encoding.Default.GetBytes("abcdffff"));
            var res = new CitaNodeServer(config).ReqChainCode(new bsn_sdk_csharp.Models.CitaTransReqDataBody()
            {
                UserId = "zhmtest1",
                ContractName = "CitaBsnBaseContract",
                FuncName = "update",
                FuncParam = JsonConvert.SerializeObject(args)
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void ReqChainCode_Remove()
        {
            var config = CitaConfig.NewMockConfig();
            object[] args = new object[2];
            args[0] = Hex.ToHexString(System.Text.Encoding.Default.GetBytes("test1027")).PadLeft(64, '0');

            args[1] = Hex.ToHexString(System.Text.Encoding.Default.GetBytes("abcdf"));
            var res = new CitaNodeServer(config).ReqChainCode(new bsn_sdk_csharp.Models.CitaTransReqDataBody()
            {
                UserId = "test1026",
                ContractName = "CitaBsnBaseContract",
                FuncName = "remove",
                FuncParam = JsonConvert.SerializeObject(args)
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void Trans()
        {
            var config = CitaConfig.NewMockConfig();
            if (config.appInfo.CAType == EmCAType.Trusteeship)
            {
                System.Console.WriteLine("the trusteeship application cannot call the api");
                return;
            }
            object[] args = new object[2];
            args[0] = getByte32("12345678");

            args[1] = System.Text.Encoding.Default.GetBytes("test12151554");
            var res = new CitaNodeServer(config).SDKTrans(new bsn_sdk_csharp.Models.CitaTransReq()
            {
                Contract = new bsn_sdk_csharp.Models.ContractData()
                {
                    ContractAbi = "[{\"constant\":false,\"inputs\":[{\"name\":\"baseKey\",\"type\":\"bytes32\"},{\"name\":\"baseValue\",\"type\":\"bytes\"}],\"name\":\"update\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"index\",\"type\":\"uint256\"}],\"name\":\"keyAtIndex\",\"outputs\":[{\"name\":\"\",\"type\":\"bytes32\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"baseKey\",\"type\":\"bytes32\"}],\"name\":\"remove\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"baseKey\",\"type\":\"bytes32\"},{\"name\":\"baseValue\",\"type\":\"bytes\"}],\"name\":\"insert\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"baseKey\",\"type\":\"bytes32\"}],\"name\":\"retrieve\",\"outputs\":[{\"name\":\"\",\"type\":\"bytes\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"}]",
                    ContractAddress = "0x2b93131c6008d3a1c7d9a42ea1482d2b51e0cc2c",
                    ContractName = "CitaBsnBaseContract"
                },
                UserName = "test0611",
                FuncName = "insert",
                Args = args
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void EventRegister()
        {
            var config = CitaConfig.NewMockConfig();
            var res = new CitaNodeServer(config).EventRegister(new bsn_sdk_csharp.Models.CitaRegisterEventReqDataBody()
            {
                AttachArgs = "abc=123",
                EventType = 2,
                //contractAddress = "0x866aefc204b8f8fdc3e45b908fd43d76667d7f76",
                ContractName = "GetTopic",
                NotifyUrl = "http://192.168.6.78:1808/api/cita/test"
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void EventQuery()
        {
            var config = CitaConfig.NewMockConfig();
            var res = new CitaNodeServer(config).EventQuery();

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void EventRemove()
        {
            var config = CitaConfig.NewMockConfig();
            var res = new CitaNodeServer(config).EventRemove(new bsn_sdk_csharp.Models.CitaRemoveEventReqDataBody()
            {
                EventId = "7dd1687b2221973cc949e8605de504363509d9815d96995416ee8bfc0e29cbe4"
            });

            Assert.IsNotNull(res);
        }

        private static byte[] getByte32(string str)
        {
            var b = System.Text.Encoding.Default.GetBytes(str);
            if (b.Length >= 32)
            {
                return b;
            }
            else
            {
                var bb = new byte[32];
                for (int i = 0; i < b.Length; i++)
                {
                    bb[32 - (b.Length - i)] = b[i];
                }
                return bb;
            }
        }
    }
}