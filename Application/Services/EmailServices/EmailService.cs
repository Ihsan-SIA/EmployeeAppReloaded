using Microsoft.Extensions.Options;
using System.IO;
//using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using Application.Services.Users;
using Application.Email;

namespace Presentation.EmailServices;
public class EmailSender : IEmailSender
{
    private readonly EmailSettings _settings;

    public EmailSender(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_settings.FromName, _settings.FromAddress));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        var builder = new BodyBuilder { HtmlBody = htmlMessage };
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}

//You run this in your terminal in the Presentation directory dotnet add package MailKit


//using Application.Dtos;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Data.Model;
//using Application.Abstractions;
//using Microsoft.Extensions.Logging;
//using Microsoft.EntityFrameworkCore;

//namespace Application.Services.Users
//{
//    public class AuthenticationService(UserManager<IdentityUser> _userManager, ILogger<AuthenticationService> _logger) : IAuthenticationService
//    {
//        public async Task<Result> ChangePassword(ChangePasswordDto changePasswordDto)
//        {
//            var user = await _userManager.FindByIdAsync(changePasswordDto.Id);
//            if (user is null)
//            {
//                return new Result()
//                {
//                    IsSuccess = false,
//                    ErrorMessage = "No user found"
//                };
//            }
//            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.Id);
//            if (result.Succeeded)
//            {
//                return new Result()
//                {
//                    IsSuccess = true,
//                };
//            }


//        }

//        public async Task<Result<UserDto>> SignIn(string credentials, string password)
//        {
//            var user = await _userManager.Users.FirstOrDefaultAsync(
//                c=> c.UserName == credentials || c.Email == credentials
//                );
//            if (user is null)
//            {
//                return new Result<UserDto>()
//                {
//                    IsSuccess = false,
//                    ErrorMessage = "Incorrect username or email or password"
//                };
//            }
//            var result = await _userManager.CheckPasswordAsync(user, password);
//            if (result)
//            {
//                return new Result<UserDto>()
//                {
//                    IsSuccess = true,
//                    //Value = 
//                };
//            }


//        }

//        public async Task<Result<UserDto>> SignUp(CreateUserDto createUserDto)
//        {
//            var user = new IdentityUser()
//            {
//                Id = Guid.NewGuid().ToString(),
//                Email = createUserDto.Email,
//                UserName = createUserDto.UserName
//            };
//            var result = await _userManager.CreateAsync(user, createUserDto.Password);
//            if (result.Succeeded)
//            {
//                return new Result<UserDto>()
//                {
//                    Value = new UserDto(user.Id, user.Email, user.UserName),
//                    IsSuccess = true
//                };
//            }
//            _logger.LogError(result.Errors.First().Description);
//            return new Result<UserDto>()
//            {
//                IsSuccess = false,
//                ErrorMessage = result.Errors.First().Description)
//                };
//        }
//    }
//}
//}
