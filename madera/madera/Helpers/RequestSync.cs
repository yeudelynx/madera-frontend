using System;
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
            //Get Database instance
            LocalDatabase db = new LocalDatabase();
            //Get LastSyncDate
            Date date = db.tableDate.OrderByDescending(v => v.id).FirstOrDefault();
            // Truncate to whole second
            String datetimestr = DateTime.ParseExact(date.date, "yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            //strftime('%s', date);

            db.db.Insert(new Client { id = 1000, adresse = "addr", nom = "nom", prenom = "prenom", mail = "mail@mail.mail", tel = "06.06.06.06.06", updated_at = "2018-04-20 12:36:00" });
            db.db.Insert(new Client { id = 1000, adresse = "addr", nom = "nom", prenom = "prenom", mail = "mail@mail.mail", tel = "06.06.06.06.06", updated_at = "2018-04-20 12:36:00" });
            db.db.Insert(new Client { id = 1000, adresse = "addr2", nom = "nom2", prenom = "prenom2", mail = "mail@mail.mail2", tel = "06.06.06.06.062", updated_at = "2018-04-20 17:36:00" });

            //Get all news or updated clients, devis & constituers, after LastSyncDate
            List<Client> clients = db.tableClient.ToList().Where(c => DateTime.Parse(c.updated_at) > DateTime.Parse(datetimestr)).ToList<Client>();
            List<Devis> devis = db.tableDevis.ToList().Where(d => DateTime.Parse(d.updated_at) > DateTime.Parse(datetimestr)).ToList<Devis>();
            List<Constituer> constituers = db.tableConstituer.ToList().Where(c => DateTime.Parse(c.updated_at) > DateTime.Parse(datetimestr)).ToList<Constituer>();

            foreach (var cli in clients)
            {
                Console.WriteLine(" ++++++ cli id nom" + cli.id + " : " + cli.nom);
            }



            this.request = new Request(clients, devis, constituers, date);
        }
        public String GetJsonRequest()
        {
            return JsonConvert.SerializeObject(this.request, Formatting.Indented);
        }
    }
}
