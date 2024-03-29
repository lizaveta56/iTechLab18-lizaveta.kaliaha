﻿using System.Threading.Tasks;
using AutoMapper;
using FilmsCatalog.Business.Interfaces;
using FilmsCatalog.Business.Models;
using FilmsCtalog.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmsCtalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            this._userService = userService;
            this._mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Models.Login user)
        {
            if (ModelState.IsValid)
            {
                var login = await _userService.LoginUser(_mapper.Map<Models.Login, FilmsCatalog.Business.Models.Login>(user));
                if (login == null) return BadRequest(new { message = "Invalid email or password" });
                else
                {
                    return Ok(login);
                }

            }
            else return BadRequest();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Models.Login model)
        {
            if (ModelState.IsValid)
            {
                var myUser = await _userService.RegisterUser(_mapper.Map<Models.Login, FilmsCatalog.Business.Models.Login>(model));
                if (myUser == null) return BadRequest(new { message = "This email used by other user" });
                return Ok(model);
            }
            else return BadRequest();
        }
    }
}