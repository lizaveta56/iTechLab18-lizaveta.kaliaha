﻿using FilmsCatalog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsCatalog.DAL.Interfaces
{
    public interface IGenreRepository
    {
        Task<Genre> CreateGenre(Genre genre);
        Task<Genre> DeleteGenre(Genre genre);
        Task<List<Genre>> GetAllGenres();
        IQueryable<Genre> GetQueryableAllGenres();
        Task<Genre> GetGenreByNameWithFilms(string name);
        Task<Genre> GetGenreById(int id);
        Task<Genre> UpdateGenre(Genre genre);
    }
}
