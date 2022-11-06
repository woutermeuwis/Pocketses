using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Pocketses.Web.Areas.Identity.Models;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace Pocketses.Web.Areas.Identity.Controllers;

[Area("Identity")]
public class AccountController : Controller
{
    #region dependencies

    private readonly ILogger<AccountController> _logger;
    private readonly IEmailSender _emailSender;

    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    #endregion

    public AccountController(ILogger<AccountController> logger, IEmailSender emailSender, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _logger = logger;
        _emailSender = emailSender;

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

        var userId = await _userManager.GetUserIdAsync(user);

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var callbackUrl = Url.Action("RegisterConfirmation", "Account", values: new { area = "Identity", userId = userId, code = code, returnUrl = vm.ReturnUrl }, Request.Scheme);

        await _emailSender.SendEmailAsync(vm.Email, "Confirm your email", $"Please confirm your account by <a href={HtmlEncoder.Default.Encode(callbackUrl)}>clicking here</a>.");

        if (_userManager.Options.SignIn.RequireConfirmedAccount)
            return RedirectToAction("RegisterConfirm", new { email = vm.Email, returnUrl = vm.ReturnUrl });
        else
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(vm.ReturnUrl);
        }
    }

    #endregion


}
