using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace madera.Models
{
    public class Module
    {
        //[PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public double prix { get; set; }
        public double longueur { get; set; }
        public double hauteur { get; set; }
        public double largueur { get; set; }
        public double distance_sol { get; set; }
        public int gamme_id { get; set; }
        public int categorie_id { get; set; }
        public String created_at { get; set; }
        public String updated_at { get; set; }
        public String deleted_at { get; set; }

    }
}
