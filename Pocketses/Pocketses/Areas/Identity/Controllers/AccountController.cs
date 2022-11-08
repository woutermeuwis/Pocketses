using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Pocketses.Core.Services.Interfaces;
using Pocketses.Web.Areas.Identity.Models;
using System.Text;
using System.Text.Encodings.Web;

namespace Pocketses.Web.Areas.Identity.Controllers;

[Area("Identity")]
public class AccountController : Controller
{
    #region dependencies

    private readonly ILogger<AccountController> _logger;
    private readonly IEmailService _emailService;

    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    #endregion

    public AccountController(ILogger<AccountController> logger, IEmailService emailService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _logger = logger;
        _emailService = emailService;

        _userManager = userManager;
        _signInManager = signInManager;
    }

    #region Views

    public ActionResult Register(string? returnUrl = null)
    {
        var vm = new RegisterViewModel
        {
            ReturnUrl = returnUrl ?? Url.Content("~/")
        };
        return View(vm);
    }

    public async Task<ActionResult> RegisterConfirmation(string email)
    {
        if (email is null)
            return RedirectToAction(nameof(LogIn));

        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return RedirectToAction(nameof(LogIn));

        return View();
    }

    public async Task<ActionResult> LogIn(string? email, string? returnUrl)
    {
        // clear any external provider cookie to ensure clean login flow
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        // fetch external providers
        var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();

        var vm = new LoginViewModel
        {
            Email = email,
            ReturnUrl = returnUrl ?? Url.Content("~/"),
            ExternalLogins = externalProviders.ToList()
        };
        return View(vm);
    }

    public ActionResult LogOut()
    {
        return View();
    }

    public ActionResult ForgotPassword()
    {
        return View();
    }

    public ActionResult ResendConfirmation()
    {
        return View();
    }

    #endregion

    #region Actions

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Register(RegisterViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var user = new IdentityUser
        {
            UserName = vm.Email,
            Email = vm.Email
        };
        var creationResult = await _userManager.CreateAsync(user, vm.Password);

        if (!creationResult.Succeeded)
        {
            foreach (var error in creationResult.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(vm);
        }

        _logger.LogInformation("User created a new account with password");

        await SendConfirmationEmail(user, vm.ReturnUrl);

        if (_userManager.Options.SignIn.RequireConfirmedAccount)
            return RedirectToAction(nameof(RegisterConfirmation), "Account", new { vm.Email });
        else
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(vm.ReturnUrl);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ConfirmEmail(string userId, string code, string? returnUrl)
    {
        if (userId is null || code is null)
            return RedirectToAction(nameof(LogIn));

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return RedirectToAction(nameof(LogIn));

        var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
            return RedirectToAction(nameof(LogIn));

        await _signInManager.SignInAsync(user, isPersistent: false);

        returnUrl ??= Url.Content("~/");
        return LocalRedirect(returnUrl);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> LogIn(LoginViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View(vm);
        }

        _logger.LogInformation("User logged in.");
        return LocalRedirect(vm.ReturnUrl);

    }

    #endregion

    #region Helpers

    private async Task SendConfirmationEmail(IdentityUser user, string returnUrl)
    {
        var userId = await _userManager.GetUserIdAsync(user);

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var callbackUrl = Url.Action("ConfirmEmail", "Account", values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl }, Request.Scheme);
        if (callbackUrl is not null)
            await _emailService.SendMail(user.Email, "Confirm your email", $"Please confirm your account by <a href={HtmlEncoder.Default.Encode(callbackUrl)}>clicking here</a>.");
    }

    #endregion


}
