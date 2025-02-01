using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talabat.Core.Application.Abstraction.Models.Auth;
using Talabat.Core.Application.Abstraction.Models.Common;
using Talabat.Core.Application.Abstraction.Services.Auth;
using Talabat.Core.Application.Exceptions;
using Talabat.Core.Application.Extensions;
using Talabat.Core.Domain.Entities.Identity;

namespace Talabat.Core.Application.Services.Auth
{
    public class AuthService(
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<JwtSettings> jwtOptions
        ) : IAuthService
    {
        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null)
                throw new UnAuthorizedException("The data you entered is invalid. Please make sure you enter valid data and try again.");

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);

            if (result.IsNotAllowed)
                throw new UnAuthorizedException("Account is not confirmed yet.");

            if (result.IsLockedOut)
                throw new UnAuthorizedException("Account is locked yet.");

            if (!result.Succeeded)
                throw new UnAuthorizedException("The data you entered is invalid. Please make sure you enter valid data and try again.");

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateTokenAsync(user),
            };
            return response;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto model)
        {
         
            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                throw new ValidationException()
                {
                    Errors = result.Errors.Select(E => E.Description)
                };
            var response = new UserDto()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                Id = user.Id,
                Token = await GenerateTokenAsync(user),
            };
            return response;
        }


        public async Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email!);

            return new UserDto()
            {
                Id = user!.Id,
                Email = user.Email!,
                DisplayName = user!.DisplayName,
                Token = await GenerateTokenAsync(user),
            };
        }

        public async Task<AddressDto?> GetUserAddress(ClaimsPrincipal claimsPrincipal)
        {
            var user = await userManager.FindUserWithAddress(claimsPrincipal);

            var address = mapper.Map<AddressDto>(user!.Address);

            return address;
        }

        public async Task<AddressDto> UpdateUserAddress(ClaimsPrincipal claimsPrincipal, AddressDto model)
        {
            var updatedAddress = mapper.Map<Address>(model);

            var user = await userManager.FindUserWithAddress(claimsPrincipal);

            if (user!.Address is not null)
                updatedAddress.Id = user.Address.Id;

            user!.Address = updatedAddress;
            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded) throw new BadRequestException(result.Errors.Select(error => error.Description).Aggregate((x, y) => $"{x}, {y}"));

            return model;
        }

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);

            var roles = await userManager.GetRolesAsync(user);

            var roleAsClaims = new List<Claim>();

            foreach (var role in roles)
                roleAsClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.PrimarySid,user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.GivenName,user.DisplayName),
            }.Union(userClaims).Union(roleAsClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));

            var signInCredintials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms
                .HmacSha256);

            var tokenObj = new JwtSecurityToken(
                issuer: jwtOptions.Value.Issuer,
                audience: jwtOptions.Value.Audience,
                expires: DateTime.UtcNow.AddMinutes(jwtOptions.Value.DurationMinutes),
                claims: claims,
                signingCredentials: signInCredintials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenObj);
        }

        public async Task<bool> CheckEmail(string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;
        }
    }
}
