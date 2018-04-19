using System;
using System.Collections.Generic;
using System.Text;
using madera.Models;

namespace madera.Helpers
{
    public class ResponseSync
    {
        public Date date { get; set; }
        public List<Categorie> categories { get; set; }
        public List<Client> clients { get; set; }
        public List<Constituer> constituers { get; set; }
        public List<Couleur> couleurs { get; set; }
        public List<Devis> devis { get; set; }
        public List<Gamme> gammes { get; set; }
        public List<Magasin> magasins { get; set; }
        public List<Matiere> matieres { get; set; }
        public List<Module> modules { get; set; }
        public List<Remise> remises { get; set; }
        public List<Sol> sols { get; set; }
        public List<Unite> unites { get; set; }
        public List<User> users { get; set; }
    }
}
