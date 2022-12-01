/////////////////////////////////////////////////////////////////////
// Copyright (c) Autodesk, Inc. All rights reserved
// Written by Forge Partner Development
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
/////////////////////////////////////////////////////////////////////

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Autodesk.Forge;
using Newtonsoft.Json.Linq;
using bim360issues.Models;
using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace bim360issues.Controllers
{
    public class UserController : ControllerBase
    {
        const string HTTPADDRESS = "https://l7gh91e6x9.execute-api.us-east-1.amazonaws.com/Prod/";

        [HttpGet]
        [Route("api/forge/user/profile")]
        public async Task<JObject> GetUserProfileAsync()
        {
            Credentials credentials = await Credentials.FromSessionAsync(Request.Cookies, Response.Cookies);
            if (credentials == null)
            {
                return null;
            }

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

            UsuariosForge usuariosForge = new UsuariosForge
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
                DataInsercao = response.expiresAt
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(usuariosForge), Encoding.UTF8, "application/json");

            //var ususario = PegaUsuario(credentials);
            using (var cliente = new System.Net.Http.HttpClient())
            {
                await cliente.PostAsync(HTTPADDRESS + "api/userForge", stringContent);
            }


            return response;
        }
    }
}