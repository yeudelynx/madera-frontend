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

            //test insert in server DB
            db.db.Insert(new Client { id = 1000, adresse = "addr", nom = "nom", prenom = "prenom", mail = "mail@mail.mail", tel = "06.06.06.06.06", updated_at = "2018-05-20 12:36:00" });
            db.db.Insert(new Constituer { id = 1000, x_pos = 100, y_pos = 1000, z_pos = 999, etage_plan = 5, prix_module = 123456789, module_id = 1, devis_id = 1, couleur_id = 1, matiere_id = 1, unite_id = 1, updated_at = "2018-05-20 12:36:00" });
            db.db.Insert(new Devis { id = 1000, is_sync = true, orientation = 123, client_id = 1, user_id = 1, sol_id = 1, updated_at = "2018-05-20 17:36:00" });

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
