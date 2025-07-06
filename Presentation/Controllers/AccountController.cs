using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using Presentation.Models;
using Presentation.Models.UserVM;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Presentation.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationUrl = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);

                    await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                        $"Please confirm your account by clicking <a href='{confirmationUrl}'>here</a>.");

                    SetFlashMessage("Registration successful! Please check your email to confirm your account.", "success");
                    return RedirectToAction("Login");
                }

                var errorMessages = string.Join("<br>", result.Errors.Select(e => e.Description));
                SetFlashMessage(errorMessages, "error");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest("Invalid confirmation request.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                SetFlashMessage("Email confirmed successfully!", "success");
                return RedirectToAction("Login");
            }

            SetFlashMessage("Failed to confirm email. Please try again.", "error");
            return RedirectToAction("Index");
        }



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                SetFlashMessage("Invalid login attempt!", "error");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
            {
                SetFlashMessage("Sorry could not find user!", "error");
                return RedirectToAction("Index");
            }
            var model = new EditProfileViewModel
            {
                Id = user.Id,
                PhoneNumber = user.PhoneNumber ?? ""
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetFlashMessage("Invalid input! Check again", "error");
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user is null)
            {
                SetFlashMessage("Sorry user does not exist", "error");
                return RedirectToAction("Index");
            }

            user.PhoneNumber = model.PhoneNumber;
            await _userManager.UpdateAsync(user);
            SetFlashMessage("Profile updated successfully!!", "success");
            return RedirectToAction("Index", "Home");
        }








        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                SetFlashMessage("Unable to reset password. The email address does not match your user email", "error");
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { token, email = model.Email }, Request.Scheme);

            // Send the email using your EmailSender service
            await _emailSender.SendEmailAsync(
                model.Email,
                "Reset Password",
                $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>.");

            return RedirectToAction("ForgotPasswordConfirmation");
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }





        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid password reset token.");
            }

            var model = new ResetPasswordViewModel
            {
                Token = token,
                Email = email
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }







        //[AllowAnonymous]
        //public async Task<IActionResult> ConfirmEmail(string userId, string token)
        //{
        //    if (userId == null || token == null) return RedirectToAction("Index", "Home");
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null) return NotFound();
        //    var result = await _userManager.ConfirmEmailAsync(user, token);
        //    return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //}







    }
}
