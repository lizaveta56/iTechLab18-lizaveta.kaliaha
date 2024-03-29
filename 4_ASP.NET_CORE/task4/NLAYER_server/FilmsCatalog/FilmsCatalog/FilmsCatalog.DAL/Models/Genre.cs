﻿

using System.Collections.Generic;

namespace FilmsCatalog.DAL.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public virtual ICollection<FilmGenre> FilmGenres { get; set; }
    }
}
