﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using server_task4.DAL.Context;
using server_task4.DAL.Models;
using server_task4.Models;

namespace server_task4.Services
{
    public class UserService : IUserService
    {
        private dbContext db;

        public UserService(dbContext users)
        {
            this.db = users;
        }
        public async Task<User> DeleteUser(int id)
        {
           User user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await db.Users.ToListAsync();
        }
        public List<User> GetAllUsersSync()
        {
            return  db.Users.ToList();
        }
        public async Task<User> GetUserById(int id)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<LoginDTO> RegisterUser(LoginDTO user)
        {
            if (db.Users.Any(x => x.Email == user.Email))
            {
                return null;
            }
            User myUser = new User { Email = user.Email, Password = user.Password, Role = "user" };
            db.Users.Add(myUser);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<Object> LoginUser(User user)
        {
            var identity = GetIdentity(user.Email, user.Password);
            if (identity == null)
            {
                return null;
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                email = identity.Name
            };

            // сериализация ответа
      
            return response;
        }

        public async Task<User> UpdateUser(User user)
        {
            if (!db.Users.Any(x => x.Id == user.Id))
            {
                return null;
            }

            db.Users.Update(user);
            await db.SaveChangesAsync();
            return user;
        }

      public async Task<string> GetEmailById(int id)
        {
            User user = await GetUserById(id);
            if (user != null) return user.Email;
            else return null;
        }


        public async Task<int> GetIdByEmail(string email)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null) return user.Id;
            else return 0;
        }
        private ClaimsIdentity GetIdentity(string email, string password)
        {
            User user = db.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}