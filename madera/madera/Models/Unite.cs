using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace madera.Models
{
    public class Unite
    {
        //[PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string symbole { get; set; }
        public string lib_unite{ get; set; }
        public String created_at { get; set; }
        public String updated_at { get; set; }
        public String deleted_at { get; set; }

    }
}
