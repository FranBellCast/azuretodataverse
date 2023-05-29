
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using RestSharp;

namespace Company.Function
{
    public static class PruebaDog
    {   
        [FunctionName("PruebaDog")]
        
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            /*string ObjPerros = req.Query["ObjPerros"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            ObjPerros = ObjPerros ?? data?.ObjPerros;*/

            List<Dog> dogs = new List<Dog>();

           
            string ObjPerros = "caniche?pequeño.muesli, pastor alemán?grande.tango, labrador?mediano.laika";

            if (ObjPerros.IndexOf(",") != -1)
            {
                String[] perrosArray = ObjPerros.Split(',');
                foreach (var Perro in perrosArray)
                {
                    string raza = Perro.Substring(0, Perro.IndexOf("?"));
                    string tamaño = Perro.Substring(Perro.IndexOf("?") +1, Perro.IndexOf(".")- Perro.IndexOf("?")-1);
                    string nombre = Perro.Substring(Perro.IndexOf(".")+1, Perro.Length - Perro.IndexOf(".")-1);
                    var dog = new Dog
                    {
                        crd23_raza = raza,
                        crd23_tamano = tamaño,
                        crd23_nombre = nombre
                    };
                    dogs.Add(dog);
                }
            };

                 getToken getToken = new getToken();
            string token = "Bearer " + getToken.acquireToken().Result;
            var client = new RestClient("https://org0ababdc0.api.crm4.dynamics.com/api/data/v9.2/crd23_datosazureses");

            foreach (var dog in dogs)
                {
                    
                        var bodyPre = $@"""crd23_raza"": ""{dog.crd23_raza}"",""crd23_tamano"": {dog.crd23_tamano},""crd23_nombre"": ""{dog.crd23_nombre}""";;
                        var BodyReady = "{" + bodyPre + "}";
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("OData-Version", "4.0");
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Accept", "application/json");
                        request.AddHeader("OData-MaxVersion", "4.0");
                        request.AddHeader("Authorization", token);
                        request.AddParameter("application/json", BodyReady, ParameterType.RequestBody);
                        client.Execute(request);
                }

            var response = JsonConvert.SerializeObject("Objetos añadidos a Dataverse");
            return new OkObjectResult(response);
        }


        public class Dog
        {
            public string crd23_raza;
            public string crd23_tamano;
            public string crd23_nombre;
        }
    }
    
}
