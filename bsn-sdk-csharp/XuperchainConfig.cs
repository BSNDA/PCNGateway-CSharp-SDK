using bsn_sdk_csharp.Enum;
using bsn_sdk_csharp.Models;
using bsn_sdk_csharp.NodeExtends;
using Newtonsoft.Json;
using System;
using System.IO;

namespace bsn_sdk_csharp
{
    public class XuperchainConfig
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

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static AppSetting NewSM2MockConfig()
        {
            var config = new AppSetting()
            {
                reqUrl = "http://192.168.1.41:17502",
                appCert = new AppCert()
                {
                    AppPublicCert = @"-----BEGIN PUBLIC KEY-----
MFkwEwYHKoZIzj0CAQYIKoEcz1UBgi0DQgAECwJ5ftuqndO9H3ks1hD8cB6IA9lx
/b0Z2hnFZ77rgRm9Q4lY1aqIhkM63Lh6X7uwPsoRC1xkS0PMp5x/jnRWcw==
-----END PUBLIC KEY-----",
                    UserAppPrivate = @"-----BEGIN PRIVATE KEY-----
MIGTAgEAMBMGByqGSM49AgEGCCqBHM9VAYItBHkwdwIBAQQg1gO6YNUqCPw7YQ28
qxkVEshMXJU7Hq0AXGDhJYxQxwGgCgYIKoEcz1UBgi2hRANCAARD9tF1S+8Y2fww
d56Eu/26kBOOyv6ydvLzD/wNqUOG4PCs2INAXXOIxm2aIWwEL5fa1kqIlQ+brAkL
XqsmEDTz
-----END PRIVATE KEY-----
"
                },
                appInfo = new AppInfo()
                {
                    AppCode = "app0001202007301533448387181",
                },
                userCode = "USER0001202007301532165759414",
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