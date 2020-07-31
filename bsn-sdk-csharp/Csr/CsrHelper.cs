using bsn_sdk_csharp.Ecdsa;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.IO;

namespace bsn_sdk_csharp.Csr
{
    public class CsrHelper
    {
        public static Tuple<string, AsymmetricKeyParameter> GetCsr(string issuerName)
        {
            //generate KeyPair
            var keyGenerator = new ECKeyPairGenerator();
            ECKeyGenerationParameters pa = new ECKeyGenerationParameters(SecObjectIdentifiers.SecP256r1, new SecureRandom());
            keyGenerator.Init(pa);
            var keypair = keyGenerator.GenerateKeyPair();

            //domain name of CSR file
            X509Name principal = new X509Name(string.Format("CN={0}", string.IsNullOrEmpty(issuerName) ? "test02@app0001202004161020152918451" : issuerName));

            //load public key
            SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(keypair.Public);

            CertificationRequestInfo info = new CertificationRequestInfo(principal, subjectPublicKeyInfo, new DerSet());
            //signature
            byte[] bs = ECDSAHelper.CsrSignData(info.GetEncoded(Asn1Encodable.Der), keypair.Private, pa.DomainParameters.N);
            //generate csr object
            Pkcs10CertificationRequest p10 = new Pkcs10CertificationRequest(new CertificationRequest
                (info, new AlgorithmIdentifier(X9ObjectIdentifiers.ECDsaWithSha256),
                new DerBitString(bs)).GetEncoded());
            //generate csr string
            Org.BouncyCastle.Utilities.IO.Pem.PemObject pemCSR = new Org.BouncyCastle.Utilities.IO.Pem.PemObject("CERTIFICATE REQUEST", p10.GetEncoded());

            StringWriter str = new StringWriter();
            Org.BouncyCastle.Utilities.IO.Pem.PemWriter pemCsr = new Org.BouncyCastle.Utilities.IO.Pem.PemWriter(str);
            pemCsr.WriteObject(pemCSR);
            pemCsr.Writer.Flush();

            return new Tuple<string, AsymmetricKeyParameter>(str.ToString(), keypair.Private);
        }

        public static Tuple<string, AsymmetricKeyParameter> GetSMCsr(string issuerName)
        {
            //generate KeyPair
            var keyGenerator = new ECKeyPairGenerator();
            ECKeyGenerationParameters pa = new ECKeyGenerationParameters(GMObjectIdentifiers.sm2p256v1, new SecureRandom());
            keyGenerator.Init(pa);
            var keypair = keyGenerator.GenerateKeyPair();

            //domain name of CSR file
            X509Name principal = new X509Name(string.Format("CN={0},OU=client,O=BSN", string.IsNullOrEmpty(issuerName) ? "test02@app0001202004161020152918451" : issuerName));

            //load public key
            SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(keypair.Public);

            CertificationRequestInfo info = new CertificationRequestInfo(principal, subjectPublicKeyInfo, new DerSet());
            //signature
            byte[] bs = ECDSAHelper.CsrSignData(info.GetEncoded(Asn1Encodable.Der), keypair.Private, pa.DomainParameters.N);
            //generate csr object
            Pkcs10CertificationRequest p10 = new Pkcs10CertificationRequest(new CertificationRequest
                (info, new AlgorithmIdentifier(GMObjectIdentifiers.sm2sign_with_sm3),
                new DerBitString(bs)).GetEncoded());
            //generate csr string
            Org.BouncyCastle.Utilities.IO.Pem.PemObject pemCSR = new Org.BouncyCastle.Utilities.IO.Pem.PemObject("CERTIFICATE REQUEST", p10.GetEncoded());

            StringWriter str = new StringWriter();
            Org.BouncyCastle.Utilities.IO.Pem.PemWriter pemCsr = new Org.BouncyCastle.Utilities.IO.Pem.PemWriter(str);
            pemCsr.WriteObject(pemCSR);
            pemCsr.Writer.Flush();

            return new Tuple<string, AsymmetricKeyParameter>(str.ToString(), keypair.Private);
        }
    }
}