// UserController.cs
using dotnetapp.Models;
using dotnetapp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly ApplicationDbContext _context;

        public UserController(UserService userService, ILogger<UserController> logger, ApplicationDbContext context)
        {
            _userService = userService;
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");

                var loginResult = await _userService.Login(model);
                if (loginResult.Item1 == 0)
                    return BadRequest(loginResult.Item2);
                return Ok(loginResult.Item2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> Register(User model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                if (model.UserRole == "Admin" || model.UserRole == "Customer")
                {
                    var registrationResult = await _userService.RegisterUserAsync(model);
                    if (registrationResult.Item1 == 0)
                    {
                        return BadRequest(registrationResult.Item2);
                    }
                    _context.Users.Add(model);
                    await _context.SaveChangesAsync();
                    return Ok(registrationResult.Item2);
                }
                else
                {
                    return BadRequest("Invalid Role");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
