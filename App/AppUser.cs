using bim360issues.Controllers;
using System.Threading.Tasks;
using Autodesk.Forge;
using Newtonsoft.Json.Linq;
using bim360issues.Models;
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace bim360issues.App
{
    public static class AppUser
    {
        public static UsuariosForge UsuariosForge { get; private set; }


        const string HTTPADDRESS = "https://l7gh91e6x9.execute-api.us-east-1.amazonaws.com/Prod/";
        public static async Task<UsuariosForge> GetUserAsync(Credentials credentials)
        {
           
            // the API SDK
            UserProfileApi userApi = new UserProfileApi();
            userApi.Configuration.AccessToken = credentials.TokenInternal;


            // get the user profile
            dynamic userProfile = await userApi.GetUserProfileAsync();

            // prepare a response with name & picture
            dynamic response = new JObject();
            response.name = string.Format("{0} {1}", userProfile.firstName, userProfile.lastName);
            response.picture = userProfile.profileImages.sizeX40;
            response.userId = userProfile.userId;
            response.emailId = userProfile.emailId;
            

            return new UsuariosForge
            {
                Id = Guid.NewGuid().ToString(),
                FORGE_CLIENT_ID = Credentials.GetAppSetting("FORGE_CLIENT_ID"),
                TokenInternal = credentials.TokenInternal,
                TokenPublic = credentials.TokenPublic,
                RefreshToken = credentials.RefreshToken,
                UserId = response.userId,
                EmailId = response.emailId,
                Name = response.name,
                Picture = response.picture,
                DataInsercao = credentials.ExpiresAt
            };

           // var stringContent = new StringContent(JsonConvert.SerializeObject(UsuariosForge), Encoding.UTF8, "application/json");

            //var ususario = PegaUsuario(credentials);
           /* using (var cliente = new System.Net.Http.HttpClient())
            {
                await cliente.PostAsync(HTTPADDRESS + "api/userForge", stringContent);
            }*/


            
        }
    }
}
