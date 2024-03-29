﻿using System.Collections.Generic;

namespace FilmsCatalog.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public string Role { get; set; }
    }
}
