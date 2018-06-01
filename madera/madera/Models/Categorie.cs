using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace madera.Models
{
    public class Categorie
    {
        //[PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public String lib_categorie { get; set; }
        public String created_at { get; set; }
        public String updated_at { get; set; }
        public String deleted_at { get; set; }
    }
}
