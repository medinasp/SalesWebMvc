using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Data;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    //Incluí o método Seed pra rodar no home porque não sabia como rodar a aplicação direto no StartUp
    //direto pela injeção de dependência no Program.cs, depois que eu aprendi como fazer, incluindo a linha
    //app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();
    //eu removi o serviço daqui e só deixei comentado para referência futura

    //public HomeController(ILogger<HomeController> logger, SeedingService seedingService)
    //{
    //    _logger = logger;
    //    seedingService.Seed();
    //}

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
