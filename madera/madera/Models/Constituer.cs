using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace madera.Models
{
    public class Constituer
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public double x_pos { get; set; }
        public double y_pos { get; set; }
        public double z_pos { get; set; }
        public int etage_plan { get; set; }
        public double prix_module { get; set; }
        public int module_id { get; set; }
        public int devis_id { get; set; }
        public int couleur_id { get; set; }
        public int matiere_id { get; set; }
        public int unite_id { get; set; }
        public String created_at { get; set; }
        public String updated_at { get; set; }
        public String deleted_at { get; set; }

    }
}
