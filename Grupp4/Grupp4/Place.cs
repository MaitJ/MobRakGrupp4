using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Grupp4
{
    public class Place
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        public double Temperature { get; set; }

        public long Humidity { get; set; }

        public double Wind { get; set; }

        public string Visibility { get; set; }

        
    }
}
