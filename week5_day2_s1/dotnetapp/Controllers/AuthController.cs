using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        // Implement authentication logic here
        if (login.Username == "admin" && login.Password == "password")
        {
            var token = "your-generated-token";
            return Ok(new { token });
        }
        else
        {
            return Unauthorized();
        }
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
