using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using dotnetapp.Models;
using dotnetapp.Repository;

namespace dotnetapp.Service
{
    public class UserServiceImpl : UserService
    {
        private readonly UserRepo _userRepository;
        private readonly IConfiguration _configuration;

        public UserServiceImpl(UserRepo userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        // public async Task<User> RegisterUserAsync(User user)
        // {
        //     return await _userRepository.AddUserAsync(user);
        // }

        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole),
                new Claim(ClaimTypes.Name, user.Username),

            };

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                expires: DateTime.Now.AddDays(365), 
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }
        
        public async Task<User> GetUserByIdAsync(long userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

      public async Task<(int, string)> RegisterUserAsync(User user)
{
    try
    {
        // Perform user registration logic, such as checking if the user already exists
        var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser != null)
        {
            // User with the provided email already exists
            return (0, "User with this email already exists.");
        }

        // Add the new user to the repository
        var addedUser = await _userRepository.AddUserAsync(user);
        if (addedUser != null)
        {
            // Registration successful
            return (1, "Registration successful");
        }
        else
        {
            // Failed to add user to the repository
            return (0, "Failed to register user.");
        }
    }
    catch (Exception ex)
    {
        // Handle any exceptions that occur during registration
        return (0, ex.Message); // Return the error message
    }
}
public async Task<(int, string)> Login(Login model)
{
    try
    {
        // Retrieve the user from the database using the provided email
        var user = await _userRepository.GetUserByEmailAsync(model.Email);

        // Check if the user exists
        if (user == null)
            return (0, "Invalid Email");

        // Check if the provided password matches the user's password
        if (user.Password != model.Password)
            return (0, "Invalid password");

        // If both email and password are valid, return success
        return (1, "Login successful");
    }
    catch (Exception ex)
    {
        // Handle any exceptions that occur during the login process
        return (0, ex.Message); // Return the error message
    }
}


    }
}
