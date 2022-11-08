using Microsoft.AspNetCore.Authentication;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pocketses.Web.Areas.Identity.Models;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [DisplayName("Remember me?")]
    public bool RememberMe { get; set; }

    public string ReturnUrl { get; set; } = "";

    public IList<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();

}
