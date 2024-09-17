using ABCMoneyTransfer.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ABCMoneyTransfer.Services
{
    public class AuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterUser(RegisterViewModel model)
        {
            var user = new User { 
                UserName = model.Email, 
                Email = model.Email, 
                FirstName = model.FirstName,
                MiddleName = string.IsNullOrEmpty(model.MiddleName) ? "" : model.MiddleName,
                LastName = model.LastName,
                Address = model.Address,
                Country = model.Country,
                CreatedAt = DateTime.UtcNow 
            };
            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<SignInResult> LoginUser(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is not null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("FirstName", user.FirstName),
                        new Claim("LastName", user.LastName),
                        new Claim("MiddleName", string.IsNullOrEmpty(user.MiddleName)? "": user.MiddleName),
                        new Claim("Address", user.Address),
                        new Claim("Country", user.Country)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
                    await _signInManager.SignInWithClaimsAsync(user, isPersistent: model.RememberMe, claims);

                    return SignInResult.Success;
                }

            }
            return SignInResult.Failed;
        }

        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
