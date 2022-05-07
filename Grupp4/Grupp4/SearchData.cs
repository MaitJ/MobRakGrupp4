using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Grupp4
{
    public class Datum
    {
        public int id { get; set; }
        public string wikiDataId { get; set; }
        public string type { get; set; }
        public string city { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public string region { get; set; }
        public string regionCode { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int population { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
    }

    public class Metadata
    {
        public int currentOffset { get; set; }
        public int totalCount { get; set; }
    }

    public class SearchData
    {
        public List<Datum> data { get; set; }
        public List<Link> links { get; set; }
        public Metadata metadata { get; set; }
    }

}