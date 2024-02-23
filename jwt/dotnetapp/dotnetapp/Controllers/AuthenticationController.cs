using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ApplicationDbContext _context;

        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger, ApplicationDbContext context)
        {
            _authService = authService;
            _logger = logger;
            _context = context;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await _authService.Login(model);
                if (status == 0)
                    return BadRequest(message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("registeration")]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                if (model.Role == "Admin" || model.Role == "User")
                {
                    var (status, message) = await _authService.Registeration(model, model.Role);
                    if (status == 0)
                    {
                        return BadRequest(message);
                    }
                    var user = new User
                    {
                        UserName = model.Username,
                        Password = model.Password,
                        Email = model.Email,
                        Role = model.Role,
                    };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    //return CreatedAtAction(nameof(Register), model);
                    return Ok(message);
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
