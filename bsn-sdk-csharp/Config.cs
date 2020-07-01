using bsn_sdk_csharp.Enum;
using bsn_sdk_csharp.NodeExtends;
using System;
using System.IO;

namespace bsn_sdk_csharp
{
    public class Config
    {
        /// <summary>
        /// Initialize DApp information 
        /// </summary>
        /// <param name="a"></param>
        public static void Init(AppSetting a)
        {
            try
            {
                var res = NodeServer.GetAppInfo(a);
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
        ///initialize the configuration info 
        /// </summary>
        /// <returns></returns>
        public static AppSetting NewMockConfig()
        {
            var config = new AppSetting()
            {
                reqUrl = "https://quanzhounode.bsngate.com:17602",
                appCert = new AppCert()
                {
                    AppPublicCert = @"D:/csharp/bsn-sdk-csharp/Certs/gateway_public_cert.pem",
                    UserAppPrivate = @"-----BEGIN PRIVATE KEY-----
MIGHAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBG0wawIBAQQgHX/6SmzxMrQCCQZg
aInjUENx5zcaPUd+af9EX8WlGrKhRANCAATCFSZlYUREYsgHdQePEenfnv6YuiKB
b6nD3mDaLxvv/xidH0sz14oHXS15E4AvtSra8sUBugtqrgMcg0gUmNAz
-----END PRIVATE KEY-----
"
                },
                appInfo = new AppInfo()
                {
                    AppCode = "CL1851016378620191011150518",
                },
                userCode = "reddate",
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