using EcdsApp.Models.UserManage;
using EcdsApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EcdsApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
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
            [Required]
            [Display(Name = "First Name")]
            [StringLength(50)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            [StringLength(50)]
            public string LastName { get; set; }

            [Display(Name = "Date Of Birth")]
            [StringLength(20)]
            public string DateOfBirth { get; set; }

            [Display(Name = "Address")]
            [StringLength(500)]
            public string Address { get; set; }

            [Required]
            [Display(Name = "Mobile No")]
            [StringLength(11)]
            public string MobileNo { get; set; }

            [Display(Name = "Organization")]
            [StringLength(100)]
            public string Organization { get; set; }

            [Display(Name = "Designation")]
            [StringLength(50)]
            public string Designation { get; set; }

            [Required]
            [Display(Name = "UserName")]
            [StringLength(20, MinimumLength =4, ErrorMessage ="Username should be at least {2} characters long.")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
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
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    FullName = string.Concat(Input.FirstName, " ", Input.LastName),
                    Email = Input.Email,
                    MobileNo = Input.MobileNo,
                    DateOfBirth = Input.DateOfBirth,
                    Address = Input.Address,
                    Organization = Input.Organization,
                    Designation = Input.Designation,
                    UserName = Input.UserName
                };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {

                    //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var confirmationLink = Url.Action("ConfirmEmail", "Home", new { UserId = user.Id, token = token }, Request.Scheme);
                    //string emailBodyInHTML = "<h4>Dear User,</h4><p>Please click on the link below to confirm your registration<p>" + "<a href='" + confirmationLink + "'>Click here</a></br><p>Thanks<p><p>System Administrator, Disaster and Climate Risk Information Platform<p>";

                    //EmailSender email = new EmailSender();
                    //EmailCredentials emailCredentials = new EmailCredentials()
                    //{
                    //    subject = "Email Verification link.",
                    //    htmlBody = emailBodyInHTML,
                    //    receiver = user.Email
                    //};
                    //var status = email.SendMessage(emailCredentials);
                    //if (status == "succeed")
                    //{
                    //    _logger.LogInformation(confirmationLink);
                    //    _logger.LogInformation("User created a new account with password.");

                    //    ViewData["ConfirmEmail"] = true;
                    //    return Page();
                    //}




                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    bool state = await _emailSender.SendEmailAsync(new Models.ViewModels.EmailModel()
                    {
                        Subject = "Confirm your email",
                        To = Input.Email,
                        Msg = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                    });
                    if (state)
                    {
                        var msg = "Registered Successfully. Click the confirmation link from your email to activate your account.";

                        ModelState.AddModelError(string.Empty, msg);
                        return Page();
                        //return RedirectToPage("Register", new { msg = msg });
                    }


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
