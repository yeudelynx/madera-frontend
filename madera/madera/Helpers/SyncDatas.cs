using System;
using System.Collections.Generic;
using System.Text;
using madera.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace madera.Helpers
{
    public class SyncDatas
    {
        public async Task<ResponseSync> Process()
        {
            RequestSync requestSync = new RequestSync();
            String requestJson = requestSync.GetJsonRequest();
            HttpContent content = new StringContent(requestJson);
            HttpClient client = new HttpClient();
            var response = await client.PostAsync("http://buyyourcity.ovh/api/sync", content);
            string responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseSync>(responseJson);
        }
    }
}
