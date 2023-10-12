using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using eLegal_web.Models;
using eLegal.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using eLegal.Entities;

namespace eLegal_web.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly ELegalContext _eLegalContext;

    public LoginController(ILogger<LoginController> logger, ELegalContext eLegalContext)
    {
        _logger = logger;
        _eLegalContext = eLegalContext;
    }


    public IActionResult Index()
    {

        // Comproba si el usuario ya se encuentra registrado
        ClaimsPrincipal claimUser = HttpContext.User;
        if(claimUser.Identity == null){
            return View();
        }
        if(claimUser.Identity.IsAuthenticated){
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel login){

        // Validar usuario
        var user = _eLegalContext.SysUsuarios.Where(item => item.Usuario == login.User && item.Password == login.Password).FirstOrDefault();
        if( user == null){
            ViewData["ValidateMessage"] = "Usuario y/o contraseña no validos";
            return View();
        }

            
        List<Claim> claims = new List<Claim>(){
            new Claim(ClaimTypes.NameIdentifier, user.Usuario),
            new Claim(ClaimTypes.Name, user.Nombre),
            new Claim(ClaimTypes.Email, user.Mail??""),
            new Claim("User_ID", user.Id.ToString())
        };

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        AuthenticationProperties properties = new AuthenticationProperties(){
            AllowRefresh = true,
            IsPersistent = login.KeepLoggedIn
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

        return RedirectToAction("Index", "Home");

        
    }
    
}
