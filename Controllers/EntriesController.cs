using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using eLegal_web.Models;
using eLegal.Data;
using eLegal.Entities;
using eLegal_web.Data;
using System.Security.Claims;

namespace eLegal_web.Controllers;

[Authorize]
public class EntriesController : Controller
{
    private readonly ILogger<EntriesController> _logger;
    private readonly ELegalContext _elegalContext;
    private readonly ELegalSevice _elegalService;

    public EntriesController(ILogger<EntriesController> logger, ELegalContext elegalContext,  ELegalSevice elegalService )
    {
        _logger = logger;
        _elegalContext = elegalContext;
        _elegalService = elegalService;
    }

    public IActionResult Index([FromQuery] int? page)
    {

        // Datos paginator
        int items_per_page = 25;
        int currentPage = page??1;
        
        // Obtener entradas
        IEnumerable<VwEntradasResuman> entries = _elegalContext.VwEntradasResumen.OrderBy(item => item.UltimaActualizacion)
            .Skip(( currentPage - 1 ) * items_per_page )
            .Take(items_per_page)
            .ToList();

        
        // Pasar datos a vista
        ViewBag.Items = entries;
        ViewBag.CurrentPage = currentPage;
        ViewBag.Items_Per_Page = items_per_page;

        return View();
    }

    public IActionResult New(){

        // Verifica si hay mensajes de error en TempData o ViewBag
        if (TempData["Errores"] != null) {
            ViewBag.Errores = TempData["Errores"];
        }

        // Cargar catalogos
        var departments = _elegalContext.CatDepartamentos.ToList();
        ViewData["departments"] = departments;
        var origins = _elegalContext.CatOrigens.ToList();
        ViewData["origins"] = origins;
        var entries_type = _elegalContext.CatTipoEntrada.ToList();
        ViewData["entries_type"] = entries_type;

        // Pasar datos a vista
        ViewData["items"] = new List<VwEntradasResuman>();
        ViewData["currentPage"] = 0;
        ViewData["items_Per_Page"] = 100;
        
        // Generar model de registro
        var newEntry = new NewEntryViewModel();
        newEntry.Folio = "000000000000";
        newEntry.IdUsuarioRegistro = Convert.ToInt32(User.FindFirst("User_ID")!.Value);
        newEntry.UsuarioRegistro = User.Identity!.Name??"";
        newEntry.FechaRegistro = DateTime.Now;
        
        // Retornar vista
        return View("NewEntry", newEntry );
    }


    [HttpPost]
    public async Task<IActionResult> Store(NewEntryViewModel newEntry){

        if(!ModelState.IsValid){
            // Almacena los errores en TempData o ViewBag
            TempData["Errores"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return RedirectToAction("New", "Entries");
        }

        // Asignar id usuario actual
        newEntry.IdUsuarioRegistro = Convert.ToInt32(User.FindFirst("User_ID")!.Value);
        
        // Generar registro
        if( !await _elegalService.RegistroEntradaNueva(newEntry)){
            TempData["Errores"] = new string[]{"Error al registrar la entrada, inténtelo más tarde."};
            return RedirectToAction("New", "Entries");
        }
        
        return RedirectToAction("Index", "Entries");
    }
}
