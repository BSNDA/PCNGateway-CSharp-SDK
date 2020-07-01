using System.IO;

namespace bsn_sdk_csharp.Csr
{
    /// <summary>
    /// save cert 
    /// </summary>
    public class CertStore
    {
        /// <summary>
        /// save cert
        /// </summary>
        /// <param name="certInfo">content of a cert</param>
        /// <param name="certUrl">cert url</param>
        public static void SaveCert(string certInfo, string certUrl)
        {
            byte[] priInfoByte = System.Text.Encoding.UTF8.GetBytes(certInfo);
            FileStream fs = new FileStream(certUrl, FileMode.Create, FileAccess.Write);
            fs.Write(priInfoByte, 0, priInfoByte.Length);
            fs.Close();
        }
    }
}