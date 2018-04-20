using System;
using System.Collections.Generic;
using madera.Models;

namespace madera.Helpers
{
    public class Request {
        public List<Client> clients { get; set; }
        public List<Devis> devis { get; set; }
        public List<Constituer> constituers { get; set; }
        public Date date { get; set; }

        public Request(List<Client> clients, List<Devis> devis, List<Constituer> constituers, Date date) {
            this.clients = clients;
            this.devis = devis;
            this.constituers = constituers;
            this.date = date;
        }
    }
}