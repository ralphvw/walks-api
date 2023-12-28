using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        var identityUser = new IdentityUser
        {
            UserName = request.Username,
            Email = request.Username
        };

        var identityResult = await _userManager.CreateAsync(identityUser, request.Password);

        if (identityResult.Succeeded)
        {
            if (request.Roles != null && request.Roles.Any())
            {
               identityResult = await _userManager.AddToRolesAsync(identityUser, request.Roles);
               if (identityResult.Succeeded)
               {
                   return Ok("User was registered. Please login");
               }
            }
        }

        return BadRequest("Something went wrong");
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Username);

        if (user != null)
        {
            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (checkPassword)
            {
                //Create token
                return Ok();
            }
        }

        return BadRequest("Invalid login");
    }
}