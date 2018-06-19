using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace madera.Models
{
    public class User
    {
        //[PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string login { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string ville { get; set; }
        public string password { get; set; }
        public string mail { get; set; }
        public int magasin_id { get; set; }
        public bool admin { get; set; }
        public String created_at { get; set; }
        public String updated_at { get; set; }
        public String deleted_at { get; set; }
    }
}
