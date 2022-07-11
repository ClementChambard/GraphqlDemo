namespace Api.Auth;

/// <summary>
/// Class containing all possible mutation actions for authentification
/// </summary>
[ExtendObjectType(typeof(Api.Resolvers.Mutations.Mutation))]
public class AuthMutation {

    /// <summary>
    /// Registration mutation 
    /// </summary>
    /// <param name="firstName"> Your firstname </param>
    /// <param name="lastName"> Your lastname </param>
    /// <param name="email"> Your email address </param>
    /// <param name="password"> Your password </param>
    /// <param name="confirmPassword"> Confirm your password </param>
    /// <returns></returns>
    public string Register([Service] IAuthLogic authLogic, string firstName, string lastName, string email, string password, string confirmPassword)
    {
        return authLogic.Register(firstName, lastName, email, password, confirmPassword);
    }

    /// <summary>
    /// Login mutation 
    /// </summary>
    /// <param name="email"> Your email </param>
    /// <param name="password"> Your password </param>
    /// <returns></returns>
    public string Login([Service] IAuthLogic authLogic, string email, string password)
    {
        return authLogic.Login(email, password);
    }

}