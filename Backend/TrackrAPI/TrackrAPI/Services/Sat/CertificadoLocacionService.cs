using TrackrAPI.Dtos.SAT;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Transactions;

namespace TrackrAPI.Services.SAT
{
    public class CertificadoLocacionService
    {
        private ICertificadoLocacionRepository certificadoLocacionRepository;
        private IWebHostEnvironment hostingEnvironment;
        private SimpleAES simpleAES;

        public CertificadoLocacionService(
            ICertificadoLocacionRepository certificadoLocacionRepository,
            IWebHostEnvironment hostingEnvironment,
            SimpleAES simpleAES
        )
        {
            this.certificadoLocacionRepository = certificadoLocacionRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.simpleAES = simpleAES;
        }

        public void AgregarCertificados(CertificadoLocacionDto[] certificadosDto)
        {
            /*
             * Proceso:
                - Se obtienen los archivos de la FIEL (.cer y .key) en Base64.
                - La llave privada se importa mediante un objeto RSA
                - Se genera un certificado auxiliar, importando el archivo .cer en Base64
                - Se convierten a formato PEM la llave privada y el certificado auxiliar
                - Se crea el certificado final con la importación del certificado y la llave privada en formato PEM
                - Se exporta el certificado final a un archivo PFX y se almacena en el directorio.
             */

            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    // ValidarCertificados
                    List<CertificadoLocacionDto> certificadoDtoList = certificadosDto.ToList();
                    CertificadoLocacionDto certificadoDto = certificadoDtoList.Where(c => c.TipoMime == "application/x-x509-ca-cert").FirstOrDefault();
                    CertificadoLocacionDto clavePrivadaDto = certificadoDtoList.Where(c => c.TipoMime == "application/pkcs8").FirstOrDefault();

                    byte[] certificadoBase64 = Convert.FromBase64String(certificadoDto.ArchivoBase64);
                    byte[] clavePrivadaBase64 = Convert.FromBase64String(clavePrivadaDto.ArchivoBase64);
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(clavePrivadaDto.Contrasena);

                    // Procesamiento de RSA y Certificado auxiliar
                    RSA rsa = RSA.Create();
                    rsa.ImportEncryptedPkcs8PrivateKey(passwordBytes, clavePrivadaBase64, out int bytesRead);
                    X509Certificate2 certificadoAuxiliar = new X509Certificate2(certificadoBase64);

                    // Conversión a PEM
                    string pemKey = new(PemEncoding.Write("PRIVATE KEY", rsa.ExportPkcs8PrivateKey()));
                    string pemCer = new(PemEncoding.Write("CERTIFICATE", certificadoAuxiliar.RawData));

                    // Certificado final
                    X509Certificate2 certificado = X509Certificate2.CreateFromPem(pemCer, pemKey);

                    // Conversión a PFX
                    byte[] pfxData = certificado.Export(X509ContentType.Pfx, clavePrivadaDto.Contrasena);

                    // Registro CertificadoLocacion
                    CertificadoLocacion certificadoLocacionExistente = certificadoLocacionRepository.ConsultarPorLocacion(certificadoDto.IdLocacion);
                    string contrasenaEncriptada = simpleAES.EncryptToString(clavePrivadaDto.Contrasena);

                    if (certificadoLocacionExistente != null)
                    {
                        certificadoLocacionExistente.Contrasena = contrasenaEncriptada;
                        certificadoLocacionRepository.Editar(certificadoLocacionExistente);
                    }
                    else
                    {
                        CertificadoLocacion certificadoLocacion = new()
                        {
                            IdLocacion = certificadoDto.IdLocacion,
                            Nombre = certificadoDto.IdLocacion.ToString() + ".pfx",
                            Contrasena = contrasenaEncriptada,
                            TipoMime = "application/x-pkcs12"
                        };

                        certificadoLocacionRepository.Agregar(certificadoLocacion);
                    }

                    // Almacenar archivo .pfx
                    string path = Path.Combine(hostingEnvironment.ContentRootPath, "Archivos", "Certificados");

                    using (FileStream fs = File.Create(path + "/" + certificadoDto.IdLocacion + ".pfx", pfxData.Length))
                    {
                        fs.Write(pfxData, 0, pfxData.Length);
                    }
                }
                catch (Exception ex)
                {

                    throw new CdisException(ex.Message);
                }

                scope.Complete();
            }
        }
    }
}
