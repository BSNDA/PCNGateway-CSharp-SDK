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
            sign = SetAlgorithm(config.appInfo.AlgorithmType, config.appCert.AppPublicCert, config.appCert.UserAppPrivate);
        }

        public Client()
        {
        }

        public void SetConfig(AppSetting _config)
        {
            config = _config;
            sign = SetAlgorithm(config.appInfo.AlgorithmType, config.appCert.AppPublicCert, config.appCert.UserAppPrivate);
        }

        public Crypto SetAlgorithm(EmAlgorithmType algorithmType, string puk, string pri)
        {
            switch (algorithmType.Value)
            {
                case 1:
                    var handle = new SM2Handle(pri, puk);
                    return new BsnCrypto(handle);

                case 2:
                case 3:
                    var handleEcdsa = new ECDSAHandle(pri, puk);
                    return new BsnCrypto(handleEcdsa);

                default:
                    return null;
            }
        }
    }
}