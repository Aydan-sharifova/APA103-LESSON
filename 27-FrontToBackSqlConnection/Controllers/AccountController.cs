using _27_FrontToBackSqlConnection.Models;
using _27_FrontToBackSqlConnection.Utilities.Constants;
using _27_FrontToBackSqlConnection.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _27_FrontToBackSqlConnection.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register(string? returnUrl = null)
    {
        return View(new RegisterVM { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM registerVM, string? returnUrl = null)
    {
        registerVM.ReturnUrl ??= returnUrl;

        if (!ModelState.IsValid)
        {
            return View(registerVM);
        }

        AppUser user = new()
        {
            UserName = registerVM.UserName.Trim(),
            Email = registerVM.Email.Trim()
        };

        IdentityResult createResult = await _userManager.CreateAsync(user, registerVM.Password);

        if (!createResult.Succeeded)
        {
            AddIdentityErrors(createResult);
            return View(registerVM);
        }

        await EnsureRolesAsync();

        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, AppRoles.Member);

        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            AddIdentityErrors(roleResult);
            return View(registerVM);
        }

        await _signInManager.SignInAsync(user, isPersistent: false);

        if (!string.IsNullOrWhiteSpace(registerVM.ReturnUrl) && Url.IsLocalUrl(registerVM.ReturnUrl))
        {
            return Redirect(registerVM.ReturnUrl);
        }

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        return View(new LoginVM { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM loginVM, string? returnUrl = null)
    {
        loginVM.ReturnUrl ??= returnUrl;

        if (!ModelState.IsValid)
        {
            return View(loginVM);
        }

        AppUser? user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail.Trim())
            ?? await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail.Trim());

        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Username/email or password is incorrect");
            return View(loginVM);
        }

        Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(
            user,
            loginVM.Password,
            loginVM.RememberMe,
            lockoutOnFailure: false);

        if (!signInResult.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Username/email or password is incorrect");
            return View(loginVM);
        }

        if (!string.IsNullOrWhiteSpace(loginVM.ReturnUrl) && Url.IsLocalUrl(loginVM.ReturnUrl))
        {
            return Redirect(loginVM.ReturnUrl);
        }

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }

    private async Task EnsureRolesAsync()
    {
        foreach (string role in AppRoles.All)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private void AddIdentityErrors(IdentityResult identityResult)
    {
        foreach (IdentityError error in identityResult.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
