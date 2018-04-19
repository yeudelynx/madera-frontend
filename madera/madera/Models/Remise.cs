using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace madera.Models
{
    public class Remise
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public double valeur_remise { get; set; }
        public string lib_remise{ get; set; }
        public String created_at { get; set; }
        public String updated_at { get; set; }
        public String deleted_at { get; set; }

    }
}
