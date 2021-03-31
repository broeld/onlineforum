﻿using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Domain;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public class UserService : IUserService
    {
        private IUnitOfWork unit;
        private IMapper mapper;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private string defaultProfileImagePath;

        public UserService(IUnitOfWork unitOfWork, IMapper automapper,
            UserManager<ApplicationUser> usermanager, SignInManager<ApplicationUser> signinManager)
        {
            unit = unitOfWork;
            mapper = automapper;
            userManager = usermanager;
            signInManager = signinManager;
            defaultProfileImagePath = "";
        }

        public async Task<bool> Deactivate(int userId)
        {
            var user = await unit.UserProfiles.GetByIdAsync(userId);

            user.IsActive = false;
            unit.UserProfiles.Update(user);
            await unit.SaveChangesAsync();

            return true;

        }

        public Task<UserModel> GetUserDetail(int userId)
        {
            throw new NotImplementedException();
        }

        public Task SignInAsync()
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }

        public Task SignUpAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateImage(int userId, string profileImageName, string path, byte[] image)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsInRoleAsync(int id, string role)
        {
            var user = await unit.UserProfiles.GetByIdAsync(id);

            return await userManager.IsInRoleAsync(user.ApplicationUser, role);
        }

        public async Task<ICollection<string>> GetRolesAsync(int userId)
        {
            var user = await unit.UserProfiles.GetByIdAsync(userId);

            return await userManager.GetRolesAsync(user.ApplicationUser);
        }

        private async Task<string> GenerateJWTToken(ApplicationUser user, string tokenKey, int tokenLifetime, string tokenAudience, string tokenIssuer)
        {
            var utcNow = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);

            var roles = await userManager.GetRolesAsync(user);

            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claimsIdentity.Claims,
                notBefore: utcNow,
                expires: utcNow.AddMinutes(tokenLifetime),
                audience: tokenAudience,
                issuer: tokenIssuer
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
