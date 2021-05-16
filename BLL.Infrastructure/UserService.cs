using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Domain;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        public UserService(IUnitOfWork unitOfWork, IMapper automapper,
            UserManager<ApplicationUser> usermanager, SignInManager<ApplicationUser> signinManager)
        {
            unit = unitOfWork;
            mapper = automapper;
            userManager = usermanager;
            signInManager = signinManager;
        }

        public async Task<bool> Deactivate(int userId)
        {
            var user = await unit.UserProfiles.GetByIdAsync(userId);

            user.IsActive = false;
            unit.UserProfiles.Update(user);
            await unit.SaveChangesAsync();

            return true;
        }

        public async Task<UserModel> GetUserDetail(int userId)
        {
            var userProfile = await unit.UserProfiles.GetByIdAsync(userId);

            if (userProfile == null)
            {
                throw new Exception("Result is null");
            }

            var userModel = mapper.Map<UserProfile, UserModel>(userProfile);
            userModel.IsAdmin = await IsInRoleAsync(userId, "Admin");

            return userModel;
        }

        public async Task<SignedInUserModel> SignInAsync(LoginModel loginModel, string tokenKey, int tokenExpTime, string tokenAud, string tokenIssuer)
        {
            if (loginModel == null)
            {
                throw new ArgumentNullException("Arguments is null"); 
            }

            var result = await signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, false);

            if (!result.Succeeded)
            {
                throw new Exception("Login failed");
            }

            var user = await userManager.FindByNameAsync(loginModel.Username);
            string token = await GenerateJWTToken(user, tokenKey, tokenExpTime, tokenAud, tokenIssuer);
            var profiles = await unit.UserProfiles.GetAllAsync();
            var userProfile = profiles.FirstOrDefault(au => au.ApplicationUserId == user.Id);

            if (userProfile == null)
            {
                throw new Exception("Result is null");
            }

            var userModel = mapper.Map<UserProfile, UserModel>(userProfile);
            userModel.IsAdmin = await IsInRoleAsync(userModel.Id, "Admin");

            return new SignedInUserModel(userModel, token);
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<SignedInUserModel> SignUpAsync(RegistrationModel registrationModel, string tokenKey, int tokenExpTime, string tokenAud, string tokenIssuer)
        {
            if (registrationModel == null)
            {
                throw new ArgumentNullException("Exception here");
            }

            var appUser = mapper.Map<RegistrationModel, ApplicationUser>(registrationModel);

            var result = await userManager.CreateAsync(appUser, registrationModel.Password);

            if (!result.Succeeded)
            {
                throw new Exception("User creation failed");
            }

            var userProfile = new UserProfile
            {
                ApplicationUserId = appUser.Id,
                IsActive = true,
                RegistrationDate = DateTime.Now
            };

            await unit.UserProfiles.CreateAsync(userProfile);
            await unit.SaveChangesAsync();

            userProfile.ApplicationUser = appUser;

            await signInManager.SignInAsync(appUser, false);

            string token = await GenerateJWTToken(appUser, tokenKey, tokenExpTime, tokenAud, tokenIssuer);
            var userModel = mapper.Map<UserProfile, UserModel>(userProfile);

            return new SignedInUserModel(userModel, token);

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
                new Claim(JwtRegisteredClaimNames.Aud, tokenAudience),
                new Claim(JwtRegisteredClaimNames.Iss, tokenIssuer),
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
