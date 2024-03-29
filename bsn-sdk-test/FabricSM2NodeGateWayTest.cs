﻿using bsn_sdk_csharp;
using bsn_sdk_csharp.Enum;
using bsn_sdk_csharp.Lib;
using bsn_sdk_csharp.NodeExtends;
using bsn_sdk_csharp.Trans;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace bsn_sdk_test
{
    [TestClass]
    public class FabricSM2NodeGateWayTest
    {
        /// <summary>
        /// get DApp service information
        /// </summary>
        [TestMethod]
        public void GetAppInfo()
        {
            var config = Config.NewSM2MockConfig();
            Console.WriteLine(JsonConvert.SerializeObject(config));
            Assert.IsNotNull(config);
        }

        /// <summary>
        /// register an user
        /// </summary>
        [TestMethod]
        public void RegisterUser()
        {
            var config = Config.NewSM2MockConfig();
            var res = new NodeServer(config).RegisterUser(new bsn_sdk_csharp.Models.RegisterUserReqBody()
            {
                name = "user20220525",
                secret = "123456",
                extendProperties= "{'key1':'abc'}"
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }

        /// <summary>
        ///
        /// </summary>
        [TestMethod]
        public void EnrollUser()
        {
            var config = Config.NewSM2MockConfig();
            var res = new NodeServer(config).EnrollUser(new bsn_sdk_csharp.Models.EnrollUserReqBody()
            {
                name = "user20220525",
                secret = "123456"
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }

        /// <summary>
        /// get the information of transaction
        /// </summary>
        [TestMethod]
        public void GetTransaction()
        {
            var config = Config.NewSM2MockConfig();
            var res = new NodeServer(config).GetTransaction(new bsn_sdk_csharp.Models.GetTransReqBody()
            {
                txId = "44876939fbea8adae1fe52901da410a0e957c0905450446ac17da242f591edab"
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }
        /// <summary>
        /// get the data of transaction
        /// </summary>
        [TestMethod]
        public void GetTransactionData()
        {
            var config = Config.NewMockConfig();
            var res = new NodeServer(config).GetTransactionData(new bsn_sdk_csharp.Models.GetTransReqBody()
            {
                txId = "44876939fbea8adae1fe52901da410a0e957c0905450446ac17da242f591edab"
            });
            if (res.Item3 != null)
            {
                var trans = Util.ProcessedTransactionConvert(res.Item3.transData);
                Console.WriteLine(JsonConvert.SerializeObject(trans));
            }
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }
        /// <summary>
        /// get the information of block
        /// </summary>
        [TestMethod]
        public void GetBlockInfo()
        {
            var config = Config.NewSM2MockConfig();
            var res = new NodeServer(config).GetBlockInfo(new bsn_sdk_csharp.Models.GetBlockReqBody()
            {
                blockNumber=2,
                //blockHash = "6081e24a259bed755747f35a7eb7df77247b1cba5c29a75d8857e90acdc78713"
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }
        /// <summary>
        /// get the data of block
        /// </summary>
        [TestMethod]
        public void GetBlockData()
        {
            var config = Config.NewSM2MockConfig();
            var req = new bsn_sdk_csharp.Models.GetBlockReqBody()
            {
                blockNumber=1,
                //blockHash = "6081e24a259bed755747f35a7eb7df77247b1cba5c29a75d8857e90acdc78713",
                //dataType = "json"
            };
            var res = new NodeServer(config).GetBlockData(req);
            if (res.Item3 != null)
            {
                if (req.dataType == "json")
                {
                    Console.WriteLine(res.Item3.blockData.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", ""));
                }
                else
                {
                    var b = Util.BlockConvert(res.Item3.blockData);
                    Console.WriteLine(JsonConvert.SerializeObject(b));
                }

            }
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }
        /// <summary>
        /// get ledger information
        /// </summary>
        [TestMethod]
        public void GetLedgerInfo()
        {
            var config = Config.NewSM2MockConfig();
            var res = new NodeServer(config).GetLedgerInfo();
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }

        /// <summary>
        /// add an on-chain event
        /// </summary>
        [TestMethod]
        public void EventRegister()
        {
            var config = Config.NewSM2MockConfig();
            var res = new NodeServer(config).EventRegister(new bsn_sdk_csharp.Models.EventRegisterReqBody()
            {
                attachArgs = "test",//parametes
                chainCode = "cc_cl1851016378620200413194550_00",
                eventKey = "test",
                notifyUrl = "http://127.0.0.1"
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }

        /// <summary>
        /// query event
        /// </summary>
        [TestMethod]
        public void EventQuery()
        {
            var config = Config.NewSM2MockConfig();
            var res = new NodeServer(config).EventQuery();
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }

        /// <summary>
        /// remove event
        /// </summary>
        [TestMethod]
        public void EventRemove()
        {
            var config = Config.NewSM2MockConfig();
            var res = new NodeServer(config).EventRemove(new bsn_sdk_csharp.Models.EventRemoveReqBody()
            {
                eventId = "f28581d709374b72b7d449df089ed93f"
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }

        /// <summary>
        /// invoke a transaction under key trust mode.
        /// </summary>
        [TestMethod]
        public void ReqChainCode()
        {
            var config = Config.NewSM2MockConfig();
            Dictionary<string, string> transdata = new Dictionary<string, string>();
            transdata.Add("test", "testtesttesttest");
            var res = new NodeServer(config).ReqChainCode(new bsn_sdk_csharp.Models.ReqChainCodeReqBody()
            {
                args = new string[] { "{\"baseKey\":\"test20200421\",\"baseValue\":\"this is string \"}" },
                nonce = RandomHelper.GetRandomNonce(),
                chainCode = "cc_cl1851016378620200413194550_00",
                funcName = "set",
                userName = "",
                transientData = transdata
            });
            Console.WriteLine(JsonConvert.SerializeObject(res));
            Assert.IsNotNull(res);
        }

        /// <summary>
        ///invoke a transaction under public key uoload mode
        /// </summary>
        [TestMethod]
        public void SdkTran()
        {
            var config = Config.NewSM2MockConfig();
            if (config.appInfo.CAType != EmCAType.Unmanaged)
            {
                Console.WriteLine("DApp in Key Trust mode cannot call this API");
                return;
            }
            List<string> args = new List<string>();
            args.Add("{\"baseKey\":\"test20220526153\",\"baseValue\":\"this is string \"}");
            var request = new TransRequest(config, "user20220525")
            {
                Args = args,
                ChaincodeId = "cc_app0001202205241457194715844_01",
                ChannelId = config.appInfo.ChannelId,
                Fcn = "set"
            };
            if (string.IsNullOrEmpty( request.PrivateKey)||string.IsNullOrEmpty(request.EnrollmentCertificate))
            {
                Console.WriteLine("user info error");
                return;
            }
            var s = new NodeServer(config).Trans(request);
            Console.WriteLine(JsonConvert.SerializeObject(s));
        }
        /// <summary>
        ///invoke a transaction under public key uoload mode
        /// </summary>
        [TestMethod]
        public void SdkTranQuery()
        {
            var config = Config.NewSM2MockConfig();
            if (config.appInfo.CAType != EmCAType.Unmanaged)
            {
                Console.WriteLine("DApp in Key Trust mode cannot call this API");
                return;
            }
            List<string> args = new List<string>();
            args.Add("test20220526");
            var request = new TransRequest(config, "user20220525")
            {
                Args = args,
                ChaincodeId = "cc_app0001202205241457194715844_01",
                ChannelId = config.appInfo.ChannelId,
                Fcn = "get"
            };
            if (string.IsNullOrEmpty(request.PrivateKey) || string.IsNullOrEmpty(request.EnrollmentCertificate))
            {
                Console.WriteLine("user info error");
                return;
            }
            var s = new NodeServer(config).Trans(request);
            Console.WriteLine(JsonConvert.SerializeObject(s));
        }
    }
}