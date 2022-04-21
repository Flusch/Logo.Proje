using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Logo.Proje.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using PasswordGenerator;

namespace Logo.Proje.Areas.Identity.Pages.Account
{
    public class AddUserModel : PageModel
    {
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly ILogger<AddUserModel> _logger;
        private readonly IEmailSender _emailSender;

        public AddUserModel(
            UserManager<CustomIdentityUser> userManager,
            SignInManager<CustomIdentityUser> signInManager,
            ILogger<AddUserModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "{0} alanı gereklidir.")]
            [Display(Name = "Kullanıcı Adı")]
            public string Username { get; set; }

            [Required(ErrorMessage = "{0} alanı gereklidir.")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required(ErrorMessage = "{0} alanı gereklidir.")]
            [Display(Name = "Ad")]
            public string Name { get; set; }
            [Required(ErrorMessage = "{0} alanı gereklidir.")]
            [Display(Name = "Soyad")]
            public string Surname { get; set; }
            [Required(ErrorMessage = "{0} alanı gereklidir.")]
            [Display(Name = "Telefon Numarası")]
            public string PhoneNumber { get; set; }
            [Display(Name = "TC Kimlik Numarası")]
            [MinLength(11, ErrorMessage = "TC Kimlik No 11 karakter olmalıdır.")]
            [MaxLength(11, ErrorMessage = "TC Kimlik No 11 karakter olmalıdır")]
            public long IdentityNumber { get; set; }
            [Display(Name = "Aracı Var Mı?")]
            public bool HasCar { get; set; }
            [Display(Name = "Araç Plakası")]
            public string CarPlate { get; set; }

            [Required(ErrorMessage = "{0} alanı gereklidir.")]
            [StringLength(100, ErrorMessage = "{0} en az {2}, en fazla {1} karakter olmalıdır.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Şifre")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Şifreyi Onayla")]
            [Compare("Password", ErrorMessage = "Girdiğiniz şifreler uyuşmuyor.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new CustomIdentityUser { UserName = Input.Username, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                await _userManager.AddToRoleAsync(user, Enums.Roles.Resident.ToString());
                //var password = new Password(6).Next(); //fix: generate random password
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    return LocalRedirect("/Resident");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
