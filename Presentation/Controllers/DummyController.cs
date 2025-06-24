

        // 2. Change Password
        // public IActionResult ChangePassword() => View();

        // [HttpPost]
        // public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        // {
        //     if (!ModelState.IsValid) return View(model);
        //     var user = await _userManager.GetUserAsync(User);
        //     var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        //     if (result.Succeeded)
        //     {
        //         await _signInManager.RefreshSignInAsync(user);
        //         return RedirectToAction("Index", "Home");
        //     }
        //     foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
        //     return View(model);
        // }

        // // 3. Forgot Password
        // [AllowAnonymous]
        // public IActionResult ForgotPassword() => View();

        // [HttpPost, AllowAnonymous]
        // public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        // {
        //     if (!ModelState.IsValid) return View(model);
        //     var user = await _userManager.FindByEmailAsync(model.Email);
        //     if (user == null || !(await _userManager.IsEmailConfirmedAsync(user))) return RedirectToAction("ForgotPasswordConfirmation");
        //     var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //     var callbackUrl = Url.Action("ResetPassword", "Account", new { token, email = model.Email }, Request.Scheme);
        //     await _emailSender.SendEmailAsync(model.Email, "Reset Password", $"Reset your password here: <a href='{callbackUrl}'>link</a>");
        //     return RedirectToAction("ForgotPasswordConfirmation");
        // }

        // [AllowAnonymous]
        // public IActionResult ResetPassword(string token, string email) => View(new ResetPasswordViewModel { Token = token, Email = email });

        // [HttpPost, AllowAnonymous]
        // public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        // {
        //     if (!ModelState.IsValid) return View(model);
        //     var user = await _userManager.FindByEmailAsync(model.Email);
        //     if (user == null) return RedirectToAction("ResetPasswordConfirmation");
        //     var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        //     if (result.Succeeded) return RedirectToAction("ResetPasswordConfirmation");
        //     foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
        //     return View(model);
        // }
        // 4. Confirm Email