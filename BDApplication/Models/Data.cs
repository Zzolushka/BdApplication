using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BDApplication.Models
{
    public class Data
    {
        [JsonProperty("startX")]
        public float StartX { get; set; }
        [JsonProperty("startY")]
        public float StartY { get; set; }
        [JsonProperty("endX")]
        public float EndX { get; set; }
        [JsonProperty("endY")]
        public float EndY { get; set; }
        [JsonProperty("size")]
        public float Size { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
    }

    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}