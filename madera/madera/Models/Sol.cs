using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace madera.Models
{
    public class Sol
    {
        //[PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string image_sol { get; set; }
        public int longueur { get; set; }
        public int largeur { get; set; }
        public String list_point_sol { get; set; }
        public String created_at { get; set; }
        public String updated_at { get; set; }
        public String deleted_at { get; set; }

    }
}
