using Microsoft.AspNetCore.Mvc;
namespace TarotWeb.Controllers;
public sealed class HomeController : Controller
{
    public IActionResult Index() => View();
    public IActionResult Error() => View();
}
