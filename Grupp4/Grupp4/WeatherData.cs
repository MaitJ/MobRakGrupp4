using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Grupp4
{
    public class WeatherData
    {
        [JsonProperty("name")]
        public string Title { get; set; }

        [JsonProperty("coord")]
        public Coord Coord { get; set; }

        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("visibility")]
        public long Visibility { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }

        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("sys")]
        public Sys Sys { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("cod")]
        public long Cod { get; set; }
    }

    public class City
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coord")]
        public Coord coord { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("population")]
        public long Population { get; set; }
        [JsonProperty("timezone")]
        public long TimeZone { get; set; }
    }

    public class WeatherDataForecast
    {

        [JsonProperty("daily")]
        public DayForecast[] daily { get; set; }

    }

    public class DayForecast
    {
        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }
        [JsonProperty("sunset")]
        public long sunset { get; set; }
        [JsonProperty("temp")]
        public Temperature Temperature { get; set; }

    }

    public class Temperature
    {
        [JsonProperty("day")]
        public float Day { get; set; }
        [JsonProperty("min")]
        public float Min { get; set; }
        [JsonProperty("max")]
        public float Max { get; set; }
        [JsonProperty("night")]
        public float Night { get; set; }
        [JsonProperty("eve")]
        public float Evening { get; set; }
        [JsonProperty("morn")]
        public float Morning { get; set; }
    }


    public class Clouds
    {
        [JsonProperty("all")]
        public long All { get; set; }
    }

    public class Coord
    {
        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }
    }

    public class Main
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("pressure")]
        public long Pressure { get; set; }

        [JsonProperty("humidity")]
        public long Humidity { get; set; }

        [JsonProperty("temp_min")]
        public double TempMin { get; set; }

        [JsonProperty("temp_max")]
        public double TempMax { get; set; }
    }

    public class Sys
    {
        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("message")]
        public double Message { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }
    }

    public class Weather
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("main")]
        public string Visibility { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public long Deg { get; set; }
    }
}
