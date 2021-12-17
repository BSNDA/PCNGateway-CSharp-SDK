using bsn_sdk_csharp.Common;
using bsn_sdk_csharp.Ecdsa;
using bsn_sdk_csharp.Enum;
using bsn_sdk_csharp.Models;
using bsn_sdk_csharp.NodeExtends;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.IO;

namespace bsn_sdk_csharp
{
    public class CitaConfig

    {
        /// <summary>
        /// Get App info via URL
        /// </summary>
        private static string appInfoUrl = "/api/app/getAppInfo";

        /// <summary>
        /// Get DApp info
        /// </summary>
        /// <param name="req">request content</param>
        /// <param name="url">interface address</param>
        /// <param name="certPath">https cert path</param>
        /// <returns></returns>
        public static Tuple<bool, string, AppInfoResBody> GetAppInfo(AppSetting config)
        {
            try
            {
                //Get the unsigned signature of DApp
                NodeApiReq req = new NodeApiReq()
                {
                    header = new ReqHeader()
                    {
                        userCode = config.userCode,
                        appCode = config.appInfo.AppCode
                    }
                };
                NodeApiResBody<AppInfoResBody> res = SendHelper.SendPost<NodeApiResBody<AppInfoResBody>>(config.reqUrl + appInfoUrl, JsonConvert.SerializeObject(req), config.httpsCert);

                if (res != null)
                {
                    //Check the status codes in turn
                    if (res.header.code != 0) return new Tuple<bool, string, AppInfoResBody>(false, res.header.msg, null);

                    return new Tuple<bool, string, AppInfoResBody>(true, null, res.body);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new Tuple<bool, string, AppInfoResBody>(false, "failed to get DApp info", null);
        }

        /// <summary>
        /// Initialize DApp information
        /// </summary>
        /// <param name="a"></param>
        public static void Init(AppSetting a)
        {
            try
            {
                var res = GetAppInfo(a);
                // call to gateway Success
                if (res.Item1)
                {
                    a.appInfo.AppType = res.Item3.appType;
                    a.appInfo.CAType = EmCAType.From(res.Item3.caType);
                    a.appInfo.AlgorithmType = EmAlgorithmType.From(res.Item3.algorithmType);
                    a.appInfo.ChannelId = res.Item3.channelId;
                    a.appInfo.MspId = res.Item3.mspId;
                    a.appInfo.Version = res.Item3.version;
                }
                else//the call to gateway failed to return
                {
                    System.Console.WriteLine("failed to retrieve app info");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ECKeyPair GetUserKey(string userName, AppSetting a)
        {
            try
            {
                var prikurl = a.mspDir + "/" + userName + "@" + a.appInfo.AppCode + "_prik";
                var pubkurl = a.mspDir + "/" + userName + "@" + a.appInfo.AppCode + "_pubk";
                ECKeyPair key = new ECKeyPair();

                if (!File.Exists(prikurl))
                {
                    if (a.appInfo.AlgorithmType == EmAlgorithmType.SM2)
                    {
                        var sm2key = SM2.SM2Utils.GenerateKeyPair();

                        ECDSAStore.SavePriKey((ECPrivateKeyParameters)sm2key.Private, prikurl);
                        ECDSAStore.SavePubKey((ECPublicKeyParameters)sm2key.Public, pubkurl);
                        key.prik = (ECPrivateKeyParameters)sm2key.Private;
                        key.pubk = (ECPublicKeyParameters)sm2key.Public;
                    }
                    else
                    {
                        var k1key = Ecdsa.ECDSAUtils.GenerateSecP256k1KeyPair();
                        ECDSAStore.SavePriKey(k1key.Private, prikurl);
                        ECDSAStore.SavePubKey(k1key.Public, pubkurl);
                        key.prik = (ECPrivateKeyParameters)k1key.Private;
                        key.pubk = (ECPublicKeyParameters)k1key.Public;
                    }
                }
                else
                {
                    key.prik = LibraryHelper.LoadPrikey(prikurl);
                    key.pubk = LibraryHelper.LoadPubkey(pubkurl);
                }

                return key;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static AppSetting NewMockConfig()
        {
            var config = new AppSetting()
            {
                reqUrl = "http://192.168.1.43:17502",
                appCert = new AppCert()
                {
                    AppPublicCert = @"-----BEGIN PUBLIC KEY-----
MFkwEwYHKoZIzj0CAQYIKoEcz1UBgi0DQgAEIlh1C0iWAdcKnM/yAaZZT/42NVzT
Vyr31H9MDhHbPkp+/B3gsp5iZOr6OTAGO9jpN10/YMIrxt2IMg5auIEvMA==
-----END PUBLIC KEY-----",
                    UserAppPrivate = @"-----BEGIN PRIVATE KEY-----
MIGTAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBHkwdwIBAQQgfPng3pvsulMoOLNj
LT5IUX0wXZQ7RRIgxQ6VGSDneOKgCgYIKoZIzj0DAQehRANCAAS+iGu+3yofOh0H
74MQJQRivCXi6LtQGkrBe5NXAwL+8wAy+4iaESnIFsDFC2fr2qMgvd005UdvJeJu
VQTCefws
-----END PRIVATE KEY-----"
                },
                appInfo = new AppInfo()
                {
                    AppCode = "app0001202012111600499234472"
                },
                userCode = "USER0001202007101641243516163",
                mspDir = "D:/csharp/bsn-sdk-csharp/Certs",
                httpsCert = "D:/csharp/bsn-sdk-csharp/Certs/bsn_gateway_https.crt"
            };

            if (!Directory.Exists(config.mspDir))
            {
                Directory.CreateDirectory(config.mspDir);
            }
            Init(config);
            return config;
        }
    }
}