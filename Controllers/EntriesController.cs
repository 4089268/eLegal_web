using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using eLegal_web.Models;
using eLegal.Data;
using eLegal.Entities;

namespace eLegal_web.Controllers;

[Authorize]
public class EntriesController : Controller
{
    private readonly ILogger<EntriesController> _logger;
    private readonly ELegalContext _elegalContext;

    public EntriesController(ILogger<EntriesController> logger, ELegalContext elegalContext)
    {
        _logger = logger;
        _elegalContext = elegalContext;
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

}
