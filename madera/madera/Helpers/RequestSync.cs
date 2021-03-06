﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SQLite;
using madera.Models;
using Newtonsoft.Json;

namespace madera.Helpers
{
    public class RequestSync
    {
        public Request request;

        public RequestSync()
        {
            try
            {
                 
                //Get Database instance
                LocalDatabase db = new LocalDatabase();

                //Get LastSyncDate
                Date date = db.tableDate.OrderByDescending(v => v.id).Last();
                // Truncate to whole second
                Console.WriteLine("madate" +date.date);
                String datetimestr = DateTime.ParseExact(date.date, "yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                Console.WriteLine("madate1"+ datetimestr);
                //Get all news or updated clients, devis & constituers, after LastSyncDate
                List<Client> clients = db.tableClient.ToList().Where(c => DateTime.Parse(c.updated_at) > DateTime.Parse(datetimestr)).ToList<Client>();
                List<Devis> devis = db.tableDevis.ToList().Where(d => DateTime.Parse(d.updated_at) > DateTime.Parse(datetimestr)).ToList<Devis>();
                List<Constituer> constituers = db.tableConstituer.ToList().Where(c => DateTime.Parse(c.updated_at) > DateTime.Parse(datetimestr)).ToList<Constituer>();

                this.request = new Request(clients, devis, constituers, date);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Request sync error : " + ex);
            }
        }
        public String GetJsonRequest()
        {
            return JsonConvert.SerializeObject(this.request, Formatting.Indented);
        }
    }
}
