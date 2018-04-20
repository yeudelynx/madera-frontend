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
    public static class SyncDatas
    {
        public static async void  Process()
        {
            RequestSync requestSync = new RequestSync();
            String requestJson = requestSync.GetJsonRequest();
            //String requestJson ="";
            HttpContent content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            var response = await client.PostAsync("http://buyyourcity.ovh/api/sync", content);
            string responseJson = await response.Content.ReadAsStringAsync();
            ResponseSync responseSync = JsonConvert.DeserializeObject<ResponseSync>(responseJson);

            //Save datas in DB.
            LocalDatabase db = new LocalDatabase();
            db.WriteSync(responseSync);
            Console.WriteLine(db.tableDate.Where(s => s.id.Equals(1)));

            Thread.Sleep(1000);
            Console.WriteLine(requestJson);


        }
    }
}
