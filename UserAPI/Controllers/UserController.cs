using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _service;

    public UsersController(UserService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRequestDto request)
    {
        var result = await _service.RegisterUserAsync(request);
        if (result == null)
            return BadRequest(new { mensaje = "El correo ya registrado" });

        return Created("", result);
    }
}