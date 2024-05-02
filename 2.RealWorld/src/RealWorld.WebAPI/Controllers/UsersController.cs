using Microsoft.AspNetCore.Mvc;
using RealWorld.WebAPI.DTOs;
using RealWorld.WebAPI.Services;

namespace RealWorld.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await userService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto request, CancellationToken cancellationToken)
    {
        var result = await userService.CreateAsync(request, cancellationToken);
        if (result)
        {
            return Ok(new {Message = "User Registration is successful"});
        }
        return BadRequest(new { Message = "We encountered an error during user registration!" } );
    }

    [HttpGet]
    public async Task<IActionResult> DeleteById(int id, CancellationToken cancellationToken)
    {
        var result = await userService.DeleteByIdAsync(id, cancellationToken);
        if (result)
        {
            return Ok(new { Message = "User deleted successfully" });
        }
        return BadRequest(new { Message = "We encountered an error deleting the user!" });
    }
}
