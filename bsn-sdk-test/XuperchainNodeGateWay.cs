using bsn_sdk_csharp;
using bsn_sdk_csharp.NodeExtends;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace bsn_sdk_test
{
    [TestClass]
    public class XuperchainNodeGateWay
    {
        [TestMethod]
        public void RegisterUser()
        {
            var config = XuperchainConfig.NewSM2MockConfig();
            var res = new XuperchainNodeServer(config).RegisterUser(new bsn_sdk_csharp.Models.RegisterUserReqDataBody()
            {
                UserId = "testuser1"
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetBlockInfo_ByBlockHash()
        {
            var config = XuperchainConfig.NewSM2MockConfig();
            var res = new XuperchainNodeServer(config).GetBlockInfo(new bsn_sdk_csharp.Models.GetBlockInfoReqDataBody()
            {
                BlockHash = "dff18e4d36f25d6df8617fc671a13cc6a6f0a792b85a88cbcb57f90f92838a44"
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetBlockInfo_ByBlockHeight()
        {
            var config = XuperchainConfig.NewSM2MockConfig();
            var res = new XuperchainNodeServer(config).GetBlockInfo(new bsn_sdk_csharp.Models.GetBlockInfoReqDataBody()
            {
                BlockHeight = 2
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetTxInfo()
        {
            var config = XuperchainConfig.NewSM2MockConfig();
            var res = new XuperchainNodeServer(config).GetTxInfo(new bsn_sdk_csharp.Models.GetTxInfoReqDataBody()
            {
                TxHash = "caf5b291a71b24f2369423c9bd7ce682a02db40a270f9a11be16cc4037b639ab"
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void ReqChainCode_Insert_Data()
        {
            var config = XuperchainConfig.NewSM2MockConfig();
            var res = new XuperchainNodeServer(config).ReqChainCode(new bsn_sdk_csharp.Models.CallContractReqDataReqDataBody()
            {
                UserId = "testuser1",
                UserAddr = "2BLNPpDPJiwoAPpmMzAaCUW7cqjWoMAWkz",
                ContractName = "cc_appxc_02",
                FuncName = "insert_data",
                FuncParam = "{\"base_key\":\"dev_0001\",\"base_value\":\"aaron1\"}"
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void ReqChainCode_Select_Data()
        {
            var config = XuperchainConfig.NewSM2MockConfig();
            var res = new XuperchainNodeServer(config).ReqChainCode(new bsn_sdk_csharp.Models.CallContractReqDataReqDataBody()
            {
                UserId = "testuser1",
                UserAddr = "2BLNPpDPJiwoAPpmMzAaCUW7cqjWoMAWkz",
                ContractName = "cc_appxc_02",
                FuncName = "select_data",
                FuncParam = "{\"base_key\":\"dev_0001\"}"
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void ReqChainCode_Update_Data()
        {
            var config = XuperchainConfig.NewSM2MockConfig();
            var res = new XuperchainNodeServer(config).ReqChainCode(new bsn_sdk_csharp.Models.CallContractReqDataReqDataBody()
            {
                UserId = "testuser1",
                UserAddr = "2BLNPpDPJiwoAPpmMzAaCUW7cqjWoMAWkz",
                ContractName = "cc_appxc_02",
                FuncName = "update_data",
                FuncParam = "{\"base_key\":\"dev_0001\",\"base_value\":\"aaaaadddd\"}"
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void ReqChainCode_Remove_Data()
        {
            var config = XuperchainConfig.NewSM2MockConfig();
            var res = new XuperchainNodeServer(config).ReqChainCode(new bsn_sdk_csharp.Models.CallContractReqDataReqDataBody()
            {
                UserId = "testuser1",
                UserAddr = "2BLNPpDPJiwoAPpmMzAaCUW7cqjWoMAWkz",
                ContractName = "cc_appxc_02",
                FuncName = "remove_data",
                FuncParam = "{\"base_key\":\"dev_0001\"}"
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }
    }
}