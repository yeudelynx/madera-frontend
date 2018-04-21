using System;
using System.Collections.Generic;
using System.Text;
using madera.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;

namespace madera.Helpers
{
    public class SyncDatas
    {
        //TODO : add observable on syncOK
        bool syncOK = false;

        public async void Process()
        {
            RequestSync requestSync = new RequestSync();
            String requestJson = requestSync.GetJsonRequest();
            //String requestJson = "";
            HttpContent content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var response = await client.PostAsync("http://buyyourcity.ovh/api/sync", content);
            string responseJson = await response.Content.ReadAsStringAsync();
            ResponseSync responseSync = JsonConvert.DeserializeObject<ResponseSync>(responseJson);
           
            //Save datas in DB.
            LocalDatabase db = new LocalDatabase();
            syncOK = db.WriteSync(responseSync);
            Thread.Sleep(1000);
            Console.WriteLine("TRACE requestJson : " + requestJson);
            Console.WriteLine("TRACE responseJson : " + responseJson);
            Console.WriteLine("TRACE syncOK : " + syncOK);
        }
    }
}
