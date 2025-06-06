using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public UserService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<(UserResponseDto? user, string? error)> RegisterUserAsync(UserRequestDto request)
    {
        // Verifica si el correo existe en la bd
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            return (null, "Ya existe un usuario registrado con el correo proporcionado.");

        // Obtiene expresiones regulares del appsettings.json
        var emailRegex = _config["Regex:Email"];
        var passwordRegex = _config["Regex:Password"];

        // Valida el formato del correo
        if (!Regex.IsMatch(request.Email, emailRegex))
            return (null, "El correo ingresado no tiene un formato válido. Ejemplo: usuario@dominio.com");

        // Valida el formato de la contraseña
        if (!Regex.IsMatch(request.Password, passwordRegex))
            return (null, "La contraseña debe tener al menos 8 caracteres, incluir una letra mayúscula y un número.");

        // Neva instancia de usuario
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Phones = request.Phones,
            Token = TokenGenerator.GenerateToken() // Genera UUID como token
        };

        // Persiste en la bd
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        // Retorna el DTO de respuesta
        return (new UserResponseDto
        {
            Id = user.Id,
            Created = user.Created,
            Modified = user.Modified,
            LastLogin = user.LastLogin,
            Token = user.Token,
            IsActive = user.IsActive
        }, null);
    }
}
