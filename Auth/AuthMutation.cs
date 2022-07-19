using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;

namespace Api.Auth;

/// <summary>
/// Class containing all possible mutation actions for authentification
/// </summary>
[ExtendObjectType(typeof(Api.Resolvers.Mutations.Mutation))]
public class AuthMutation {

    /// <summary>
    /// Registration mutation 
    /// </summary>
    /// <param name="input"> Input for the mutation </param>
    /// <returns> Status string </returns>
    public RegisterPayload Register([Service] IAuthLogic authLogic, RegisterInput input)
    {
        return authLogic.Register(input);
    }

    /// <summary>
    /// Login mutation 
    /// </summary>
    /// <param name="input"> Input for the mutation </param>
    /// <returns> Token and status string </returns>
    public LoginPayload Login([Service] IAuthLogic authLogic, LoginInput input)
    {
        return authLogic.Login(input);
    }

    /// <summary> Test action for default roles </summary>
    [Authorize(Roles=new[]{"default", "admin"})] 
    public string ActionDefault() => "You have access to default actions";

    /// <summary> Test action for admin roles </summary>
    [Authorize(Roles=new[]{"admin"})] 
    public string ActionAdmin() => "You have access to admin actions";

    /// <summary> Returns the connected user </summary>
    [Authorize]
    public User GetMe([Service] Api.Data.ApiDbContext context, ClaimsPrincipal claimsPrincipal)
    {
        return context.Users.Where(u => u.EmailAddress == claimsPrincipal.FindFirstValue("Email")).FirstOrDefault();
    }

}