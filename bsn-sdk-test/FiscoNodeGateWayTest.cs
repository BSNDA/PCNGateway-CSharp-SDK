using bsn_sdk_csharp;
using bsn_sdk_csharp.NodeExtends;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Numerics;

namespace bsn_sdk_test
{
    [TestClass]
    public class FiscoNodeGateWayTest
    {
        /// <summary>
        /// get DApp service information
        /// </summary>
        [TestMethod]
        public void GetAppInfo()
        {
            var config = FiscoConfig.NewMockTestFiscoSMConfig();
            Console.WriteLine(JsonConvert.SerializeObject(config));
            Assert.IsNotNull(config);
        }

        [TestMethod]
        public void TransSM()
        {
            var config = FiscoConfig.NewMockTestFiscoSMConfig();
            object[] args = new object[3];
            args[0] = "s0604";
            args[1] = BigInteger.Parse("5");
            args[2] = "aa";
            var res = new FiscoNodeServer(config).GetTrans(new bsn_sdk_csharp.Models.FiscoTransReq()
            {
                Contract = new bsn_sdk_csharp.Models.ContractData()
                {
                    ContractAbi = "[{\"constant\":false,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"},{\"name\":\"base_key\",\"type\":\"int256\"},{\"name\":\"base_value\",\"type\":\"string\"}],\"name\":\"update\",\"outputs\":[{\"name\":\"\",\"type\":\"int256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"},{\"name\":\"base_key\",\"type\":\"int256\"}],\"name\":\"remove\",\"outputs\":[{\"name\":\"\",\"type\":\"int256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"},{\"name\":\"base_key\",\"type\":\"int256\"},{\"name\":\"base_value\",\"type\":\"string\"}],\"name\":\"insert\",\"outputs\":[{\"name\":\"\",\"type\":\"int256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"}],\"name\":\"select\",\"outputs\":[{\"name\":\"\",\"type\":\"string[]\"},{\"name\":\"\",\"type\":\"int256[]\"},{\"name\":\"\",\"type\":\"string[]\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"}]",
                    ContractAddress = "0xc206db9e77e547b015e2cb39d23ff8b0314746a4",
                    ContractName = "BsnBaseContract"
                },
                UserName = "test0623",
                FuncName = "insert",
                Args = args
            });
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void TransK1()
        {
            var config = FiscoConfig.NewMockTestFiscoK1Config();
            object[] args = new object[3];
            args[0] = "s0604";
            args[1] = BigInteger.Parse("5");
            args[2] = "aa";
            var res = new FiscoNodeServer(config).GetTrans(new bsn_sdk_csharp.Models.FiscoTransReq()
            {
                Contract = new bsn_sdk_csharp.Models.ContractData()
                {
                    ContractAbi = "[{\"constant\":false,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"},{\"name\":\"base_key\",\"type\":\"int256\"},{\"name\":\"base_value\",\"type\":\"string\"}],\"name\":\"update\",\"outputs\":[{\"name\":\"\",\"type\":\"int256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"},{\"name\":\"base_key\",\"type\":\"int256\"}],\"name\":\"remove\",\"outputs\":[{\"name\":\"\",\"type\":\"int256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"},{\"name\":\"base_key\",\"type\":\"int256\"},{\"name\":\"base_value\",\"type\":\"string\"}],\"name\":\"insert\",\"outputs\":[{\"name\":\"\",\"type\":\"int256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"}],\"name\":\"select\",\"outputs\":[{\"name\":\"\",\"type\":\"string[]\"},{\"name\":\"\",\"type\":\"int256[]\"},{\"name\":\"\",\"type\":\"string[]\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"}]",
                    ContractAddress = "0x866aefc204b8f8fdc3e45b908fd43d76667d7f76",
                    ContractName = "BsnBaseContractk1"
                },
                UserName = "test0611",
                FuncName = "insert",
                Args = args
            });

            Assert.IsNotNull(config);
        }

        [TestMethod]
        public void Trans_Query()
        {
            var config = FiscoConfig.NewMockTestFiscoK1Config();
            object[] args = new object[1];
            args[0] = "s0604";
            var res = new FiscoNodeServer(config).GetTrans(new bsn_sdk_csharp.Models.FiscoTransReq()
            {
                Contract = new bsn_sdk_csharp.Models.ContractData()
                {
                    ContractAbi = "[{\"constant\":false,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"},{\"name\":\"base_key\",\"type\":\"int256\"},{\"name\":\"base_value\",\"type\":\"string\"}],\"name\":\"update\",\"outputs\":[{\"name\":\"\",\"type\":\"int256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"},{\"name\":\"base_key\",\"type\":\"int256\"}],\"name\":\"remove\",\"outputs\":[{\"name\":\"\",\"type\":\"int256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"},{\"name\":\"base_key\",\"type\":\"int256\"},{\"name\":\"base_value\",\"type\":\"string\"}],\"name\":\"insert\",\"outputs\":[{\"name\":\"\",\"type\":\"int256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"}],\"name\":\"select\",\"outputs\":[{\"name\":\"\",\"type\":\"string[]\"},{\"name\":\"\",\"type\":\"int256[]\"},{\"name\":\"\",\"type\":\"string[]\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"}]",
                    ContractAddress = "0x866aefc204b8f8fdc3e45b908fd43d76667d7f76",
                    ContractName = "BsnBaseContractk1"
                },
                UserName = "test0611",
                FuncName = "select",
                Args = args
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void Trans_QuerySM()
        {
            var config = FiscoConfig.NewMockTestFiscoSMConfig();
            object[] args = new object[1];
            args[0] = "s0604";
            var res = new FiscoNodeServer(config).GetTrans(new bsn_sdk_csharp.Models.FiscoTransReq()
            {
                Contract = new bsn_sdk_csharp.Models.ContractData()
                {
                    ContractAbi = "[{\"constant\":false,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"},{\"name\":\"base_key\",\"type\":\"int256\"},{\"name\":\"base_value\",\"type\":\"string\"}],\"name\":\"update\",\"outputs\":[{\"name\":\"\",\"type\":\"int256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"},{\"name\":\"base_key\",\"type\":\"int256\"}],\"name\":\"remove\",\"outputs\":[{\"name\":\"\",\"type\":\"int256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"},{\"name\":\"base_key\",\"type\":\"int256\"},{\"name\":\"base_value\",\"type\":\"string\"}],\"name\":\"insert\",\"outputs\":[{\"name\":\"\",\"type\":\"int256\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"base_id\",\"type\":\"string\"}],\"name\":\"select\",\"outputs\":[{\"name\":\"\",\"type\":\"string[]\"},{\"name\":\"\",\"type\":\"int256[]\"},{\"name\":\"\",\"type\":\"string[]\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"}]",
                    ContractAddress = "0xc206db9e77e547b015e2cb39d23ff8b0314746a4",
                    ContractName = "BsnBaseContract"
                },
                UserName = "test0623",
                FuncName = "select",
                Args = args
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void RegisterUserSM()
        {
            var config = FiscoConfig.NewMockTestFiscoSMConfig();
            var res = new FiscoNodeServer(config).RegisterUser(new bsn_sdk_csharp.Models.FiscoRegisterReqBody()
            {
                UserId = "test0730"
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void RegisterUserK1()
        {
            var config = FiscoConfig.NewMockTestFiscoK1Config();
            var res = new FiscoNodeServer(config).RegisterUser(new bsn_sdk_csharp.Models.FiscoRegisterReqBody()
            {
                UserId = "test0730"
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void ReqChainCode_QueryK1()
        {
            var config = FiscoConfig.NewMockTestFiscoK1Config();
            object[] args = new object[1];
            args[0] = "s0604";
            var res = new FiscoNodeServer(config).ReqChainCode(new bsn_sdk_csharp.Models.FiscoTransReqDataBody()
            {
                UserId = "test0730",
                ContractName = "BsnBaseContractk1",
                FuncName = "select",
                FuncParam = JsonConvert.SerializeObject(args)
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void ReqChainCode_Insertk1()
        {
            var config = FiscoConfig.NewMockTestFiscoK1Config();
            object[] args = new object[3];
            args[0] = "s0604";
            args[1] = BigInteger.Parse("12");
            args[2] = "aa";
            var res = new FiscoNodeServer(config).ReqChainCode(new bsn_sdk_csharp.Models.FiscoTransReqDataBody()
            {
                UserId = "test0730",
                ContractName = "BsnBaseContractk1",
                FuncName = "insert",
                FuncParam = JsonConvert.SerializeObject(args)
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetBlockInfo()
        {
            var config = FiscoConfig.NewMockTestFiscoK1Config();
            var res = new FiscoNodeServer(config).GetBlockInfo(new bsn_sdk_csharp.Models.FiscoBlockReqDataBody()
            {
                BlockNumber = "5"
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetBlockHeight()
        {
            var config = FiscoConfig.NewMockTestFiscoK1Config();
            var res = new FiscoNodeServer(config).GetBlockHeight();

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetTxCount()
        {
            var config = FiscoConfig.NewMockTestFiscoK1Config();
            var res = new FiscoNodeServer(config).GetTxCount();

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetTxCountByBlockNumber()
        {
            var config = FiscoConfig.NewMockTestFiscoK1Config();
            var res = new FiscoNodeServer(config).GetTxCountByBlockNumber("40");

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetTxReceiptByTxHash()
        {
            var config = FiscoConfig.NewMockTestFiscoK1Config();
            var res = new FiscoNodeServer(config).GetTxReceiptByTxHash(new bsn_sdk_csharp.Models.FiscoTxReqDataBody()
            {
                TxHash = "0xf73662808ca6c51a4136232bdace2e0814fcba9103d491fa92bd82d8c04fd448"
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetTxInfoByTxHash()
        {
            var config = FiscoConfig.NewMockTestFiscoK1Config();
            var res = new FiscoNodeServer(config).GetTxInfoByTxHash(new bsn_sdk_csharp.Models.FiscoTxReqDataBody()
            {
                TxHash = "0xf73662808ca6c51a4136232bdace2e0814fcba9103d491fa92bd82d8c04fd448"
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void EventRegister()
        {
            var config = FiscoConfig.NewMockTestFiscoK1Config();
            var res = new FiscoNodeServer(config).EventRegister(new bsn_sdk_csharp.Models.FiscoRegisterReqDataBody()
            {
                AttachArgs = "abc=123",
                EventType = 1,
                ContractAddress = "0x866aefc204b8f8fdc3e45b908fd43d76667d7f76",
                ContractName = "BsnBaseContractk1",
                NotifyUrl = "http://192.168.6.78:18080/api/fiscobcos/test"
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void EventQuery()
        {
            var config = FiscoConfig.NewMockTestFiscoK1Config();
            var res = new FiscoNodeServer(config).EventQuery();

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void EventRemove()
        {
            var config = FiscoConfig.NewMockTestFiscoK1Config();
            var res = new FiscoNodeServer(config).EventRemove(new bsn_sdk_csharp.Models.RemoveReqDataBody()
            {
                EventId = "525bee97e9324e62807396ee53570cab"
            });

            Assert.IsNotNull(res);
        }
    }
}