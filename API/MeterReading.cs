using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MeterReadingClient.API
{
    public class Reading
    {
        string Baseurl = "http://localhost:44344/api";
        public async Task<string> UploadMeterReadings(string readings)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                UseDefaultCredentials = true
            };

            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://localhost:44344/");
                var content = new StringContent(readings, Encoding.UTF8, "application/json");
                HttpResponseMessage result = await  client.PostAsync("api/meter-reading/meter-reading-uploads",content);
                string resultContent = result.Content.ReadAsStringAsync().Result;
                return resultContent;
               
            }
           

            /* var Api = new GetToken();
             var token = await Api.Token();*/


           /* using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var a = $"{{records\":[{{\"AccountId\": 2344,\"MeterReadingDateTime\": \"22-04-2019 09:24\",\"MeterReadValue\": \"01002\"}}]}}";
                var content = new StringContent(a, Encoding.UTF8, "application/json");

                //Sending request to find web api REST service resource Developers using HttpClient  
                HttpResponseMessage Res = await client.PostAsync($"meter-reading/meter-reading-uploads", content);


                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var result = Res.Content.ReadAsStringAsync().Result;

                    return result;

                }
                return "Fail";
            }*/
        }
    }
}