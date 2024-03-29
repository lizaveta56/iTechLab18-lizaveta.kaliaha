﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FilmsCatalog.Business.Interfaces;
using FilmsCatalog.Business.Models;
using FilmsCtalog.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmsCtalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;
        private readonly IMapper _mapper;
        public FilmController(IFilmService filmService, IMapper mapper)
        {
            this._filmService = filmService;
            this._mapper = mapper;
        }

        // GET: api/Film
        [HttpGet]
        public async Task<IList<Models.Film>> Get()
        {
            return _mapper.Map<IList<FilmsCatalog.Business.Models.Film>, IList<Models.Film>>(await _filmService.GetAllFilms());
        }

        // GET: api/Film
        [HttpGet("lazy/{page}")]
        public async Task<IList<Models.Film>> GetLazy(int page)
        {
            return _mapper.Map<IList<FilmsCatalog.Business.Models.Film>, IList<Models.Film>>(await _filmService.GetAllFilmsLazy(page,10));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.FilmWithGenres>> GetWithGenres(int id)
        {
            Models.FilmWithGenres film = _mapper.Map<FilmsCatalog.Business.Models.FilmWithGenres, Models.FilmWithGenres>(await _filmService.GetFilmByWithGenres(id));
            if (film == null) return BadRequest(new { message = "Film with this id {" + id + "} not found" });
            return Ok(film);
        }
    }
}