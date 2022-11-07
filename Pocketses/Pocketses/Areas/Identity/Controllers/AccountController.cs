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
            ReturnUrl = returnUrl
        };
        return View(vm);
    }

    public ActionResult ConfirmEmail(string email)
    {
        var vm = new ConfirmEmailViewModel { Email = email };
        return View(vm);
    }

    public ActionResult RegisterConfirmation()
    {
        return View();
    }

    public ActionResult Login()
    {
        return View();
    }

    public ActionResult Logout()
    {
        return View();
    }

    #endregion

    #region Actions
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Register(RegisterViewModel vm)
    {
        vm.ReturnUrl ??= Url.Content("~/");

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
            return RedirectToAction(nameof(ConfirmEmail), "Account", new { vm.Email });
        else
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(vm.ReturnUrl);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> RequestEmailConfirmation(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        await SendConfirmationEmail(user, string.Empty);
        return RedirectToAction(nameof(Login), "Account");
    }

    #endregion

    #region Helpers

    private async Task SendConfirmationEmail(IdentityUser user, string returnUrl)
    {
        var userId = await _userManager.GetUserIdAsync(user);

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var callbackUrl = Url.Action("RegisterConfirmation", "Account", values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl }, Request.Scheme);
        if (callbackUrl is not null)
            await _emailService.SendMail(user.Email, "Confirm your email", $"Please confirm your account by <a href={HtmlEncoder.Default.Encode(callbackUrl)}>clicking here</a>.");
    }

    #endregion


}
