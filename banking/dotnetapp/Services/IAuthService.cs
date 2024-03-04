using dotnetapp.Models;

namespace dotnetapp.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);
        // Task<(int, string)> Login(LoginModel model);
     Task<(int, string, string, long, string)> Login(LoginModel model);

        Task<User> GetUserByIdAsync(long userId);

    }
}
