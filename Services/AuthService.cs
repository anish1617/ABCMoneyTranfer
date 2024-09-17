using ABCMoneyTransfer.Models;
using Microsoft.AspNetCore.Identity;

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
            var user = new User { UserName = model.Email, Email = model.Email, CreatedAt = DateTime.UtcNow };
            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<SignInResult> LoginUser(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        }

        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
