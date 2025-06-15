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
