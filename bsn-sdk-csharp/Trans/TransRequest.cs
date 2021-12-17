using bsn_sdk_csharp.Enum;
using System.Collections.Generic;

namespace bsn_sdk_csharp.Trans
{
    public class TransRequest
    {
        public string ChannelId { get; set; }

        public string ChaincodeId { get; set; }

        public string Fcn { get; set; }

        public List<string> Args { get; set; }

        public Dictionary<string, string> TransientMap { get; set; }

        /// <summary>
        ///user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// user cert under unmanaged user mode
        /// </summary>
        public string EnrollmentCertificate { get; set; }

        /// <summary>
        /// Private key under Public-Key-Upload Mode
        /// </summary>
        public string PrivateKey { get; set; }

        public TransRequest(AppSetting config, string userName)
        {
            this.UserName = userName;
            if (config.appInfo.CAType == EmCAType.Unmanaged)
            {
                EnrollmentCertificate = string.Format("{0}/{1}@{2}_cert.pem", config.mspDir, userName, config.appInfo.AppCode);
                PrivateKey = string.Format("{0}/{1}@{2}_sk.pem", config.mspDir, userName, config.appInfo.AppCode);
            }
        }
    }

    /// <summary>
    /// user info under Public-Key-Upload Mode
    /// </summary>
    public class User
    {
        /// <summary>
        /// user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// user cert under Public-Key-Upload Mode
        /// </summary>
        public string EnrollmentCertificate { get; set; }

        /// <summary>
        /// Private key under Public-Key-Upload Mode
        /// </summary>
        public string PrivateKey { get; set; }
    }
}