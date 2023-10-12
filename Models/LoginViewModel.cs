namespace eLegal_web.Models;

public class LoginViewModel
{
    public string User { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool KeepLoggedIn { get; set; }
}
