namespace Api.Auth;


/// <summary>
/// Input class for the register mutation 
/// </summary>
public class RegisterInput {

    /// <summary> The firstname of the new user </summary>
    public string FirstName { get; set; }

    /// <summary> The lastname of the new user </summary>
    public string LastName { get; set; }

    /// <summary> The email address of the new user </summary>
    public string Email { get; set; }

    /// <summary> The password of the new user </summary>
    public string Password { get; set; }

    /// <summary> The confirmation for the password of the new user </summary>
    public string ConfirmPassword { get; set; }

}

/// <summary>
/// Input class for the login mutation 
/// </summary>
public class LoginInput {

    /// <summary> Email of the account </summary>
    public string Email { get; set; }

    /// <summary> Password of the account </summary>
    public string Password { get; set; }

}