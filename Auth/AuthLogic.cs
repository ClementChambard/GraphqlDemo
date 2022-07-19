using System.Security.Cryptography;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Api.Auth;

/// <summary>
/// Interface for Authentication logic
/// </summary>
public interface IAuthLogic {

    /// <summary> User registration function </summary>
    /// <param name="input"> Input data (name, email, password) </param>
    /// <returns> A sucess or error message </returns>
    public RegisterPayload Register(RegisterInput input);

    /// <summary> User login fonction </summary>
    /// <param name="input"> Input data (email, password) </param>
    /// <returns> A jwt and a sucess or error message </returns>
    public LoginPayload Login(LoginInput input);
}

/// <summary>
/// Main class for Aithentication logic
/// </summary>
public class AuthLogic : IAuthLogic {

    private readonly Api.Data.ApiDbContext _context;
    private readonly TokenSettings _tokenSettings;

    /// <summary> Base constructor </summary>
    public AuthLogic([Service(ServiceKind.Synchronized)]Api.Data.ApiDbContext context, IOptions<TokenSettings> tokenSettings) { _context = context; _tokenSettings = tokenSettings.Value; }

    /// <inheritDoc/>
    public RegisterPayload Register(RegisterInput input)
    {
        string errorMessage = RegistrationValidation(input.Email, input.Password, input.ConfirmPassword);
        if (!string.IsNullOrEmpty(errorMessage))
            return new RegisterPayload{StatusString = errorMessage};

        User newUser = new User{
            EmailAddress = input.Email,
            FirstName = input.FirstName,
            LastName = input.LastName,
            Password = PasswordHash(input.Password)
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();

        UserRoles newUserRoles = new UserRoles{
            Name = "default",
            UserId = newUser.UserId
        };

        _context.UserRoles.Add(newUserRoles);
        _context.SaveChanges();

        return new RegisterPayload{StatusString = "Registration success"};
    }

    /// <inheritDoc/>
    public LoginPayload Login(LoginInput input)
    {
        if (string.IsNullOrEmpty(input.Email) || string.IsNullOrEmpty(input.Password))
            return new LoginPayload{Token = null, StatusString = "Invalid credentials"};

        var user = _context.Users.Where(_ => _.EmailAddress == input.Email).FirstOrDefault();
        if (user == null) 
            return new LoginPayload{Token = null, StatusString = "Invalid credentials"};

        if (!ValidatePasswordHash(input.Password, user.Password)) 
            return new LoginPayload{Token = null, StatusString = "Invalid credentials"};

        var roles = _context.UserRoles.Where(_ => _.UserId == user.UserId).ToList();
        return new LoginPayload{Token = GetJWTAuthKey(user, roles), StatusString = "Success"};
    }

    /// <summary>
    /// Create a hash from a string
    /// </summary>
    /// <param name="password"> The original password </param>
    /// <returns> The hashed password </returns>
    public static string PasswordHash(string password)
    {
        byte[] salt;

        RandomNumberGenerator.Create().GetBytes(salt = new byte[16]);
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);

        byte[] hash = pbkdf2.GetBytes(20);
        byte[] hashBytes = new byte[36];

        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        return Convert.ToBase64String(hashBytes);
    }

    private bool ValidatePasswordHash(string password, string dbPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(dbPassword);
        byte[] salt = new byte[16];

        Array.Copy(hashBytes, 0, salt, 0, 16);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);

        byte[] hash = pbkdf2.GetBytes(20);

        for (int i = 0; i < 20; i++)
        {
            if (hashBytes[i + 16] != hash[i])
            {
                return false;
            }
        }
        return true;
    }

    private string GetJWTAuthKey(User user, List<UserRoles> roles)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>();

        claims.Add(new Claim("Email", user.EmailAddress));
        claims.Add(new Claim("LastName", user.LastName));

        if ((roles?.Count ?? 0) > 0)
        {
            foreach( var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
        }

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _tokenSettings.Issuer,
            audience: _tokenSettings.Audience,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials,
            claims: claims
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    private string RegistrationValidation(string emailAddress, string password, string confirmPassword)
    {
        if (string.IsNullOrEmpty(emailAddress)) 
            return "Email can't be empty";
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword)) 
            return "Password or confirm password can't be empty";
        if (password != confirmPassword) 
            return "Confirm password is not the same as password";

        string emailRules = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
        if (!Regex.IsMatch(emailAddress, emailRules)) 
            return "Not a valid email";

        string passwordRules = @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$";
        if (!Regex.IsMatch(password, passwordRules)) 
            return "Not a valid password";

        return string.Empty;
    }
}