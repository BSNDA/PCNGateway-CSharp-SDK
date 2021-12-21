using bsn_sdk_csharp;
using bsn_sdk_csharp.Lib;
using bsn_sdk_csharp.NodeExtends;
using bsn_sdk_csharp.Trans;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace bsn_sdk_test
{
    [TestClass]
    public class NodeGateWayTest
    {
        /// <summary>
        /// get DApp service information
        /// </summary>
        [TestMethod]
        public void GetAppInfo()
        {
            var config = Config.NewMockConfig();
            Console.WriteLine(JsonConvert.SerializeObject(config));
            Assert.IsNotNull(config);
        }

        /// <summary>
        /// register an user
        /// </summary>
        [TestMethod]
        public void RegisterUser()
        {
            var config = Config.NewMockConfig();
            var res = new NodeServer(config).RegisterUser(new bsn_sdk_csharp.Models.RegisterUserReqBody()
            {
                name = "test",
                secret = "123456"
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
            var config = Config.NewMockConfig();
            var res = new NodeServer(config).EnrollUser(new bsn_sdk_csharp.Models.EnrollUserReqBody()
            {
                name = "test",
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
            var config = Config.NewMockConfig();
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
                txId = "3b231f16376bc0ebcdd0dc82e8728746174f393f80ad108b6128f1ad1ccaebd5"
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
            var config = Config.NewMockConfig();
            var res = new NodeServer(config).GetBlockInfo(new bsn_sdk_csharp.Models.GetBlockReqBody()
            {
                blockHash = "1e7f2e4ec080890cae703694312b954a160f5955c2f0f7b09569ffbb9630b9e8"
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
            var config = Config.NewMockConfig();
            var res = new NodeServer(config).GetBlockData(new bsn_sdk_csharp.Models.GetBlockReqBody()
            {
                blockNumber = 3
                //blockHash = "6081e24a259bed755747f35a7eb7df77247b1cba5c29a75d8857e90acdc78713"
            });
            if (res.Item3 != null)
            {
                var b = Util.BlockConvert(res.Item3.blockData);
                Console.WriteLine(JsonConvert.SerializeObject(b));
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
            var config = Config.NewMockConfig();
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
            var config = Config.NewMockConfig();
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
            var config = Config.NewMockConfig();
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
            var config = Config.NewMockConfig();
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
            var config = Config.NewMockConfig();
            Dictionary<string, string> transdata = new Dictionary<string, string>();
            transdata.Add("test", "testtesttesttest");
            var res = new NodeServer(config).ReqChainCode(new bsn_sdk_csharp.Models.ReqChainCodeReqBody()
            {
                args = new string[] { "{\"baseKey\":\"test20200421\",\"baseValue\":\"this is string \"}" },
                nonce = RandomHelper.GetRandomNonce(),
                chainCode = "cc_app0001202008181046408059749_01",
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
            var config = Config.NewMockConfig();
            List<string> args = new List<string>();
            args.Add("{\"baseKey\":\"test202004212002\",\"baseValue\":\"this is string \"}");
            var request = new TransRequest(config, "test")
            {
                Args = args,
                ChaincodeId = "cc_cl1851016378620200413194550_00",
                ChannelId = config.appInfo.ChannelId,
                Fcn = "set"
            };
            var s = new NodeServer(config).Trans(request);
            Console.WriteLine(JsonConvert.SerializeObject(s));
        }

        /// <summary>
        /// data encrypt/dencrypt
        /// </summary>

        [TestMethod]
        public void testSecretKey()
        {
            var mingwen = "Hello world";

            string key = "8bdc1382852f11ea940dd4ae52c963a9";

            string prikey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent><P>/hf2dnK7rNfl3lbqghWcpFdu778hUpIEBixCDL5WiBtpkZdpSw90aERmHJYaW2RGvGRi6zSftLh00KHsPcNUMw==</P><Q>6Cn/jOLrPapDTEp1Fkq+uz++1Do0eeX7HYqi9rY29CqShzCeI7LEYOoSwYuAJ3xA/DuCdQENPSoJ9KFbO4Wsow==</Q><DP>ga1rHIJro8e/yhxjrKYo/nqc5ICQGhrpMNlPkD9n3CjZVPOISkWF7FzUHEzDANeJfkZhcZa21z24aG3rKo5Qnw==</DP><DQ>MNGsCB8rYlMsRZ2ek2pyQwO7h/sZT8y5ilO9wu08Dwnot/7UMiOEQfDWstY3w5XQQHnvC9WFyCfP4h4QBissyw==</DQ><InverseQ>EG02S7SADhH1EVT9DD0Z62Y0uY7gIYvxX/uq+IzKSCwB8M2G7Qv9xgZQaQlLpCaeKbux3Y59hHM+KpamGL19Kg==</InverseQ><D>vmaYHEbPAgOJvaEXQl+t8DQKFT1fudEysTy31LTyXjGu6XiltXXHUuZaa2IPyHgBz0Nd7znwsW/S44iql0Fen1kzKioEL3svANui63O3o5xdDeExVM6zOf1wUUh/oldovPweChyoAdMtUzgvCbJk1sYDJf++Nr0FeNW1RB1XG30=</D></RSAKeyValue>";
            string pubkey = @"<RSAKeyValue><Modulus>5m9m14XH3oqLJ8bNGw9e4rGpXpcktv9MSkHSVFVMjHbfv+SJ5v0ubqQxa5YjLN4vc49z7SVju8s0X4gZ6AzZTn06jzWOgyPRV54Q4I0DCYadWW4Ze3e+BOtwgVU1Og3qHKn8vygoj40J6U85Z/PTJu3hN1m75Zr195ju7g9v4Hk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            var aesE = SecretKeyHelper.AESEncrypt(mingwen, key);
            var aesD = SecretKeyHelper.AESDEncrypt(aesE, key);

            //RSAEncrypt
            var result = SecretKeyHelper.RSAEncrypt(pubkey, mingwen);

            //RSADecrypt
            var decrypt = SecretKeyHelper.RSADecrypt(prikey, result);

            //check result
            bool Isok = mingwen == decrypt;
        }
    }
}