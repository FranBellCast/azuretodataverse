using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Identity;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.PowerPlatform.Dataverse.Client;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
 
namespace Company.Function
{
        public class getToken
        {
        public async Task<string>acquireToken()
        {
            string Resource = "https://org0ababdc0.api.crm4.dynamics.com";
            string Authority = "https://login.microsoftonline.com/29ac68ef-732b-42d7-863a-3e2cd060ad84";
            string ClientId = "999eaf8e-71e1-44b2-b18f-eacd2a516d2f";
            string ClientSecret = "-xv8Q~Mlow___Iqx8awZTvWEhvAu.gFl6BVuUcYO";
            string token = String.Empty;
            ClientCredential credentials = new ClientCredential(ClientId, ClientSecret);
            AuthenticationContext authenticationContext = new AuthenticationContext(Authority);
            AuthenticationResult authenticationResult = await authenticationContext.AcquireTokenAsync(Resource, credentials);
            token = authenticationResult.AccessToken;
            return token;
        }
        }

}
       