using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarotWeb.Data;
using TarotWeb.Services;
using TarotWeb.ViewModels;
namespace TarotWeb.Controllers;
public sealed class TarotController : Controller
{
    private readonly TarotDrawService _draw;
    private readonly TarotDbContext _db;
    public TarotController(TarotDrawService draw, TarotDbContext db) { _draw = draw; _db = db; }
    [HttpGet] public IActionResult Draw() => View(new DrawForm());
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Draw(DrawForm form)
    {
        if (!ModelState.IsValid) return View(form);
        var reading = await _draw.DrawAsync(form);
        return RedirectToAction(nameof(Result), new { id = reading.Id });
    }
    [HttpGet]
    public async Task<IActionResult> Result(int id)
    {
        var reading = await _db.TarotReadings.AsNoTracking().Include(r => r.Cards).ThenInclude(c => c.TarotCard)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (reading == null) return NotFound();
        return View(reading);
    }
}
