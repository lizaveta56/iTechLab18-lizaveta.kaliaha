﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server_task4.DAL.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public double AverageRating { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string Producer { get; set; }
        public string Poster { get; set; }
        public int Year { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
