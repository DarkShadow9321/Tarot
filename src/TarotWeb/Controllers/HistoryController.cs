using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarotWeb.Data;
namespace TarotWeb.Controllers;
public sealed class HistoryController : Controller
{
    private readonly TarotDbContext _db;
    public HistoryController(TarotDbContext db) => _db = db;
    public async Task<IActionResult> Index() => View(await _db.TarotReadings.AsNoTracking()
        .OrderByDescending(r => r.CreatedAtUtc).Take(100).ToListAsync());
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.TarotReadings.FindAsync(id);
        if (item == null) return NotFound();
        _db.TarotReadings.Remove(item);
        await _db.SaveChangesAsync();
        TempData["Notice"] = "占卜紀錄已刪除。";
        return RedirectToAction(nameof(Index));
    }
}
