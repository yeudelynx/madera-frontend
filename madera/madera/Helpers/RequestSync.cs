using System;
using System.Collections.Generic;
using madera.Models;
using Newtonsoft.Json;

namespace madera.Helpers
{
    public class RequestSync
    {
        public Request request;
        public RequestSync()
        {
            LocalDatabase db = new LocalDatabase();
            List<Client> clients;
            List<Devis> devis;
            List<Constituer> constituers;
            Date date;
            //Get Database instance
            //Get Date
            var d = db.tableDate;

            //Get all news or updated clients, devis & constituers, after Date
            var tableDate = db.tableDate;
            var tableClient = db.tableClient;
            var tableDevis = db.tableDevis;
            var tableConstituer = db.tableConstituer;



            request = new Request(clients, devis, constituers , date);
        }
        public String GetJsonRequest() {
            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }
    }
}
