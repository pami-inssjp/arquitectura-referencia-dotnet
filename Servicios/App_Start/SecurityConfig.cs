using Microsoft.Owin.Security.Jwt;
using Owin;
using Pami.DotNet.ReferenceArchitecture.Servicios.Common.Security.Utils;
using System;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security.Cryptography;
using System.Web;

namespace Pami.DotNet.ReferenceArchitecture.Servicios.App_Start
{
    
    /* Esta clase encapsula la configuración de seguridad de OWIN. En este caso se
     * utiliza la verificación de firma de JWT firmada RSA.
     * Se hace uso de una extensión de RSACryptoServiceProvider que permite levantar 
     * la clave pública desde un archivo DER.  
     */ 
    public class SecurityConfig
    {

        public static void Configure(IAppBuilder app)
        {
            #region Security configuration using JWT

            var issuer = ConfigurationManager.AppSettings["issuer"];
            var audience =  ConfigurationManager.AppSettings["audience"];
            var publicKeyCertFile = ConfigurationManager.AppSettings["publicKey"];

            var certFilePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "App_Data\\" + publicKeyCertFile;

            var certData = File.ReadAllBytes(certFilePath);

            RSACryptoServiceProvider rsaCsp = new RSACryptoServiceProvider();
            rsaCsp.LoadPublicKeyDER(certData);


            JwtBearerAuthenticationOptions authOptions = new JwtBearerAuthenticationOptions()
            {
                TokenHandler = new JwtSecurityTokenHandler(),
                Provider = new CustomOAuthBearerProvider(),
                TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new RsaSecurityKey(rsaCsp),
                    ValidAudience = audience,
                    ValidIssuer = issuer
                }
            };

            app.UseJwtBearerAuthentication(authOptions);

            #endregion
        }
    }
}