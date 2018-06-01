using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace madera.Models
{
    public class Devis
    {
        //[PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public bool is_sync { get; set; }
        public int orientation { get; set; }
        public int client_id { get; set; }
        public int user_id { get; set; }
        public int sol_id { get; set; }
        public String created_at { get; set; }
        public String updated_at { get; set; }
        public String deleted_at { get; set; }

    }
}
