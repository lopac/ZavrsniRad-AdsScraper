#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using AdsScraper.DAL.Models;
using AdsScraper.Web.ViewModels;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Car = AdsScraper.DAL.Models.Car;

#endregion

namespace AdsScraper.Web.Controllers.WebApi
{
    public class CarsController : ApiController
    {
        private readonly AppDbContext db = new AppDbContext();

        [HttpGet]
        public IHttpActionResult GetCarModels(int id)
        {
            var models = db.Models.Where(x => x.Manufacturer.Id.Equals(id)).DistinctBy(x => x.Name).OrderBy(x => x.Name)
                .Select(x => new {id = x.Id , value = x.Name});

            return Ok(models);
        }

        [HttpPost]
        public IHttpActionResult GetCarScore([FromBody] CarViewModel car)
        {
            var score = FetchScore(new Car
            {
                Id = 0,
                Model = db.Models.Find(car.ModelId),
                Kilometers = car.Kilometers,
                Year = car.Year,
                OwnersCount = car.OwnersCountId,
                IsRegistred = car.IsRegistred,
                PriceEuro = 0,
                Engine = new Engine
                {
                    HasGasInstallation = car.HasGasInstallation,
                    Power = car.Power,
                    Volume = car.Volume,
                    GearboxType = (GearboxType) car.GearboxTypeId,
                    Fuel = db.Fuels.Find(car.EngineTypeId),
                    GearsCount = car.GearsCount
                }
            });

            return Ok(score);
        }

        private string FetchScore(Car car)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, StringTable>
                    {
                        {
                            "car",
                            new StringTable
                            {
                                ColumnNames = new[]
                                {
                                    "Id", "Manufacturer", "ModelName", "MakeYear", "IsRegistred", "OwnersCount",
                                    "Kilometers", "Volume", "Power", "GearboxType", "FuelType", "GearsCount",
                                    "HasGasInstallation", "PriceEuro"
                                },
                                Values = new[]
                                {
                                    new[]
                                    {
                                        car.Id.ToString(), car.Model.Manufacturer.Name, car.Model.Name,
                                        car.Year.ToString(), car.IsRegistred.ToString(), car.OwnersCount.ToString(),
                                        car.Kilometers.ToString(), car.Engine.Volume.ToString(),
                                        car.Engine.Power.ToString(), Convert.ToInt32(car.Engine.GearboxType).ToString(),
                                        (car.Engine.Fuel.Id - 1).ToString(), car.Engine.GearsCount.ToString(),
                                        car.Engine.HasGasInstallation.ToString(), car.PriceEuro.ToString()
                                    }
                                }
                            }
                        }
                    },
                    GlobalParameters = new Dictionary<string, string>()
                };
                const string apiKey =
                    "p+e/a5dwU/hGtDHN82W7EAhMMG/KxtRnAmihcsBzWh2aQc4awSm21GkfTJ4RU+6x9UaNuBSu3arTm+ZnOeuApQ=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress =
                    new Uri(
                        "https://ussouthcentral.services.azureml.net/workspaces/87bde9f7d3264ac3bb7077a1d91b5d30/services/e8fba702d6274099a026628da519c32a/execute?api-version=2.0&details=true");


                var response = client.PostAsJsonAsync("", scoreRequest).Result;

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiResponse>(response.Content.ReadAsStringAsync().Result)
                        .Results.car.value.Values[0].LastOrDefault();
                }

            }

            return null;
        }
    }
}