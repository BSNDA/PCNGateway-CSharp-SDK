using bsn_sdk_csharp.Ecdsa;
using bsn_sdk_csharp.Enum;
using bsn_sdk_csharp.Sign;
using bsn_sdk_csharp.SM2;

namespace bsn_sdk_csharp
{
    public class Client
    {
        public static AppSetting config { get; set; }

        public static Crypto sign { get; set; }

        public Client(AppSetting _config)
        {
            config = _config;
            sign = Lib.LibraryHelper.SetAlgorithm(config.appInfo.AlgorithmType, config.appCert.AppPublicCert, config.appCert.UserAppPrivate);
        }

        public Client()
        {
        }

        public void SetConfig(AppSetting _config)
        {
            config = _config;
            sign =Lib.LibraryHelper.SetAlgorithm(config.appInfo.AlgorithmType, config.appCert.AppPublicCert, config.appCert.UserAppPrivate);
        }

        
    }
}