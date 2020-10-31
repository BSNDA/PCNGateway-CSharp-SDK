using bsn_sdk_csharp;
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
                //BlockNumber = "5"
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
                TxHash = "0x12c9a0a9e8121bc2b77d4d3687b5268aead0e07c7488ffc3b432b62a6da16525"
            });

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void GetTxInfoByTxHash()
        {
            var config = CitaConfig.NewMockConfig();
            var res = new CitaNodeServer(config).GetTxInfoByTxHash(new bsn_sdk_csharp.Models.CitaTxReqDataBody()
            {
                TxHash = "0x12c9a0a9e8121bc2b77d4d3687b5268aead0e07c7488ffc3b432b62a6da16525"
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
                UserId = "zhm1030",
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
    }
}