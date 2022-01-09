using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MeterReadingClient.Models;
using MeterReadingClient.API;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeterReadingClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> UploadMeterReadings(string path)
        {
            try
            {
                var result = "";
                var readings = GetJSONFromCSV(path);
                var objReading = new Reading(); HttpClientHandler handler = new HttpClientHandler()
                {
                    UseDefaultCredentials = true
                };

                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri("https://localhost:44344/");
                    var content = new StringContent(readings, Encoding.UTF8, "application/json");
                    HttpResponseMessage res = await client.PostAsync("api/meter-reading/meter-reading-uploads", content);
                    string resultContent = res.Content.ReadAsStringAsync().Result;
                    result = resultContent;

                }
                return this.Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return this.Json("failure", JsonRequestBehavior.AllowGet);
            }
        }

        public string GetJSONFromCSV(string path)
        {
            try
            {
                var lines = System.IO.File.ReadAllLines(path);
                int i = 0;
                string json = $"{{\"records\":[";
                foreach (string line in lines)
                {
                    if (i != 0)
                    {
                        MeterReading reading = new MeterReading();
                        var record = line.Split(',');
                        reading.AccountId = record[0];
                        reading.MeterReadingDateTime = record[1];
                        reading.MeterReadValue = record[2];

                        json += new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(reading);
                        if (i != lines.Count() - 1)
                        {
                            json += ",";
                        }

                    }

                    i++;
                }
                json += "]}";
                return json;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

      


    }
}