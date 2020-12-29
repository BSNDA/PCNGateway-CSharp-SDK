using bsn_sdk_csharp.Enum;

namespace bsn_sdk_csharp
{
    public class AppSetting
    {
        /// <summary>
        /// user Code
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// cert storage path
        /// </summary>
        public string mspDir { get; set; }

        /// <summary>
        /// request URL gateway
        /// </summary>
        public string reqUrl { get; set; }

        /// <summary>
        /// https cert
        /// </summary>
        public string httpsCert { get; set; }

        /// <summary>
        /// DApp cert
        /// </summary>
        public AppCert appCert { get; set; }

        /// <summary>
        /// DApp info
        /// </summary>
        public AppInfo appInfo { get; set; }

        public bool isInit { get; set; }

        public AppSetting()
        {
            appInfo = new AppInfo();
            appCert = new AppCert();
        }
    }

    /// <summary>
    /// DApp cert
    /// </summary>
    public class AppCert
    {
        //bsn DApp public cert
        public string AppPublicCert { get; set; }

        //user private cert
        public string UserAppPrivate { get; set; }
    }

    /// <summary>
    /// DApp info
    /// </summary>
    public class AppInfo
    {
        /// <summary>
        /// DApp name
        /// </summary>
        public string AppCode { get; set; }

        /// <summary>
        /// DApp type
        /// </summary>
        public string AppType { get; set; }

        /// <summary>
        /// Types of key under managed or unmanaged mode
        /// </summary>
        public EmCAType CAType { get; set; }

        /// <summary>
        /// key type ECDSA/SM2
        /// </summary>
        public EmAlgorithmType AlgorithmType { get; set; }

        /// <summary>
        /// MSPID of the city
        /// </summary>
        public string MspId { get; set; }

        /// <summary>
        /// DApp channel
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// Version number
        /// </summary>
        public string Version { get; set; }
    }
}