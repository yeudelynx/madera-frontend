using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace madera.Models
{
    public class Couleur
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string lib_couleur { get; set; }
        public string code_couleur { get; set; }
        public String created_at { get; set; }
        public String updated_at { get; set; }
        public String deleted_at { get; set; }

    }
}
