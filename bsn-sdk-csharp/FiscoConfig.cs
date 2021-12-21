using bsn_sdk_csharp.Lib;
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
    public class FiscoConfig
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
                        ECDSAStore.SavePubKey(sm2key.Public, pubkurl);
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
        public static AppSetting NewMockTestFiscoSMConfig()
        {
            var config = new AppSetting()
            {
                reqUrl = "http://192.168.1.43:17502",
                appCert = new AppCert()
                {
                    AppPublicCert = @"-----BEGIN PUBLIC KEY-----
MFkwEwYHKoZIzj0CAQYIKoEcz1UBgi0DQgAECwJ5ftuqndO9H3ks1hD8cB6IA9lx
/b0Z2hnFZ77rgRm9Q4lY1aqIhkM63Lh6X7uwPsoRC1xkS0PMp5x/jnRWcw==
-----END PUBLIC KEY-----",
                    UserAppPrivate = @"-----BEGIN PRIVATE KEY-----
MIGTAgEAMBMGByqGSM49AgEGCCqBHM9VAYItBHkwdwIBAQQg3ail5qa1WdSCaE4l
NDtKsH43sn4oLU2Q4Ag9g1zmEuWgCgYIKoEcz1UBgi2hRANCAATnkyph+Ukd5mSX
Dnr0d0JNH5lzMCYlFIf/8e3LOb8R1qvYEI/ePU6TVX7UcEbCAnVPlDMlv/oesYsn
j8PiaBZv
-----END PRIVATE KEY-----
"
                },
                appInfo = new AppInfo()
                {
                    AppCode = "app0001202006221045063821068",
                },
                userCode = "USER0001202005281426464614357",
                mspDir = "D:/csharp/bsn-sdk-csharp/Certs",
                httpsCert = "D:/csharp/bsn-sdk-csharp/Certs/bsn_gateway_https.crt",
                isInit = true
            };

            if (!Directory.Exists(config.mspDir))
            {
                Directory.CreateDirectory(config.mspDir);
            }
            if (config.isInit)
            {
                Init(config);
            }

            return config;
        }

        public static AppSetting NewMockTestFiscoK1Config()
        {
            var config = new AppSetting()
            {
                reqUrl = "http://192.168.1.43:17502",
                appCert = new AppCert()
                {
                    AppPublicCert = @"-----BEGIN PUBLIC KEY-----
MFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAEh4WlY4pCv814i3WY5aRhtR3PoiIXOM1I
5xBGylyQTedo6DzJUdLfYZSZLs4py70D8FJtNICMVQCfezA7whHzUw==
-----END PUBLIC KEY-----",
                    UserAppPrivate = @"-----BEGIN PRIVATE KEY-----
MIGNAgEAMBAGByqGSM49AgEGBSuBBAAKBHYwdAIBAQQgs9DOx+bq2PlWVFRESHAM
VBKjDU9co5TIUzY203/utIugBwYFK4EEAAqhRANCAAR2T4i+jP7Tw1kFcHwGttKT
OMD7p1OHVE/evqTNlHRkYgDxEKBFE5Yoc/SsgStHhn9P9Isdz1xXYoiIzvPm9cFQ
-----END PRIVATE KEY-----
"
                },
                appInfo = new AppInfo()
                {
                    AppCode = "app0001202006042323057101002",
                },
                userCode = "USER0001202006042321579692440",
                mspDir = "D:/csharp/bsn-sdk-csharp/Certs",
                httpsCert = ""//D:/csharp/bsn-sdk-csharp/Certs/bsn_gateway_https.crt
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