using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace madera.Models
{
    //class LastSyncDate
    public class Date
    {
        //[PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public String date { get; set; }
        public String timezone_type { get; set; }
        public String timezone { get; set; }


    }
}
