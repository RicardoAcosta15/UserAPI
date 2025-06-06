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
        var (result, error) = await _service.RegisterUserAsync(request);

        if (error != null)
            return BadRequest(new { mensaje = error }); // Devuelve mensaje de error personalizado

        return Created("", result); // Devuelve 201 con el usuario creado
    }
}