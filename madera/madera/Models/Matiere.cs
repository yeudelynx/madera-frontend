﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace madera.Models
{
    public class Matiere
    {
        //[PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string lib_matiere { get; set; }
        public String created_at { get; set; }
        public String updated_at { get; set; }
        public String deleted_at { get; set; }

    }
}
