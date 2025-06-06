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

    public async Task<UserResponseDto?> RegisterUserAsync(UserRequestDto request)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            return null;

        var emailRegex = _config["Regex:Email"];
        var passwordRegex = _config["Regex:Password"];

        if (!Regex.IsMatch(request.Email, emailRegex) ||
            !Regex.IsMatch(request.Password, passwordRegex))
            return null;

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Phones = request.Phones,
            Token = TokenGenerator.GenerateToken()
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new UserResponseDto
        {
            Id = user.Id,
            Created = user.Created,
            Modified = user.Modified,
            LastLogin = user.LastLogin,
            Token = user.Token,
            IsActive = user.IsActive
        };
    }
}
