using Application.Dtos;
using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Users
{
    public interface IAuthenticationService
    {
        Task<Result<UserDto>> SignUp(CreateUserDto createUserDto);
        Task<Result<UserDto>> SignIn(string credentials, string password);
        Task<Result> ChangePassword(ChangePasswordDto changePasswordDto);
    }
}
