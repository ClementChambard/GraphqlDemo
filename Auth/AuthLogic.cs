using System.Security.Cryptography;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Api.Auth;

/// <summary>
/// </summary>
public interface IAuthLogic {
    /// <summary> User registration function </summary>
    public string Register(string firstName, string lastName, string emailAddress, string password, string confirmPassword);

    /// <summary> User login fonction </summary>
    public string Login(string email, string password);
}

/// <summary>
/// </summary>
public class AuthLogic : IAuthLogic {

    private readonly Api.Data.ApiDbContext _context;
    private readonly TokenSettings _tokenSettings;

    /// <summary></summary>
    public AuthLogic([Service(ServiceKind.Synchronized)]Api.Data.ApiDbContext context, IOptions<TokenSettings> tokenSettings) { _context = context; _tokenSettings = tokenSettings.Value; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="emailAddress"></param>
    /// <param name="password"></param>
    /// <param name="confirmPassword"></param>
    /// <returns></returns>
    public string Register(string firstName, string lastName, string emailAddress, string password, string confirmPassword)
    {
        string errorMessage = RegistrationValidation(emailAddress, password, confirmPassword);
        if (!string.IsNullOrEmpty(errorMessage)) return errorMessage;
        User newUser = new User{
            EmailAddress = emailAddress,
            FirstName = firstName,
            LastName = lastName,
            Password = PasswordHash(password)
        };
        _context.Users.Add(newUser);
        _context.SaveChanges();
        UserRoles newUserRoles = new UserRoles{
            Name = "default",
            UserId = newUser.UserId
        };
        _context.UserRoles.Add(newUserRoles);
        _context.SaveChanges();
        return "Registration success";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public string Login(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return "Invelid credentials";
        var user = _context.Users.Where(_ => _.EmailAddress == email).FirstOrDefault();
        if (user == null) return "Invalid credentials";
        if (!ValidatePasswordHash(password, user.Password)) return "Invalid credentials";
        var roles = _context.UserRoles.Where(_ => _.UserId == user.UserId).ToList();
        return GetJWTAuthKey(user, roles);
    }

    private string PasswordHash(string password)
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
        if (string.IsNullOrEmpty(emailAddress)) return "Email can't be empty";
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword)) return "Password or confirm password can't be empty";
        if (password != confirmPassword) return "Confirm password is not the same as password";
        string emailRules = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
        if (!Regex.IsMatch(emailAddress, emailRules)) return "Not a valid email";
        string passwordRules = @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$";
        if (!Regex.IsMatch(password, passwordRules)) return "Not a valid password";
        return string.Empty;
    }
}