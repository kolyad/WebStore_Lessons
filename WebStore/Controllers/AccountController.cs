using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        #region Register
        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken, ActionName("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _logger.LogInformation("Регистрация пользователя {UserName}", model.UserName);

            var user = new User
            {
                UserName = model.UserName
            };

            var registrationResult = await _userManager.CreateAsync(user, model.Password);
            if (registrationResult.Succeeded)
            {
                _logger.LogInformation("Пользователь {UserName} успешно зарегистрирован", model.UserName);

                await _userManager.AddToRoleAsync(user, Role.User);
                _logger.LogInformation("Пользователь {UserName} наделён ролью {UserRole}", model.UserName, Role.User);

                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            _logger.LogInformation("В процессе регистрации пользователя {UserName} произошли ошибки {ErrorDescriptionList}",
                model.UserName,
                string.Join(',', registrationResult.Errors.Select(s => s.Description)));

            foreach (var error in registrationResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
        #endregion

        #region Login
        [AllowAnonymous]
        public IActionResult Login(string returnUrl) => View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost, ValidateAntiForgeryToken, ActionName("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _logger.LogInformation("Попытка входа пользователя {UserName}", model.UserName);

            var login_result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe,
#if DEBUG
                false
#else
                true
#endif
                );

            if (login_result.Succeeded)
            {
                _logger.LogInformation("Вход пользователя {UserName} осуществлён успешно", model.UserName);

                return LocalRedirect(model.ReturnUrl ?? "/");
            }

            _logger.LogInformation("Вход пользователя {UserName} потерпел неудачу", model.UserName);

            ModelState.AddModelError("", "Неверное имя пользователя или пароль");

            return View(model);
        }
        #endregion

        [ActionName("Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

    }
}
