using System.Security.Cryptography;

namespace TrackrAPI.Services.Seguridad
{
    public class RsaService
    {
        private RSACryptoServiceProvider _rsa;
        
        public RsaService()
        {
            _rsa = new RSACryptoServiceProvider(2048);
        }

        public byte[] Decrypt(byte[] encryptedData)
        {
            return _rsa.Decrypt(encryptedData, true);
        }

        public string ExportPublicKey()
        {
            return Convert.ToBase64String(_rsa.ExportRSAPublicKey());
        }
    }
}
