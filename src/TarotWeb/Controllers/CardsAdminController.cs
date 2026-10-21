using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TarotWeb.Data;
using TarotWeb.Models;
namespace TarotWeb.Controllers;
// Local-only teaching demo: no user accounts. Add Identity + authorization BEFORE deployment.
public sealed class CardsAdminController : Controller
{
    private readonly TarotDbContext _db;
    public CardsAdminController(TarotDbContext db) => _db = db;
    public async Task<IActionResult> Index() => View(await _db.TarotCards.AsNoTracking().OrderBy(c => c.SortOrder).ToListAsync());
    [HttpGet] public IActionResult Create() => View("Edit", new TarotCard { Arcana = "Major", Suit = "大阿爾克那", Symbol = "✦" });
    [HttpGet] public async Task<IActionResult> Edit(int id)
    {
        var card = await _db.TarotCards.FindAsync(id);
        return card is null ? NotFound() : View(card);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(TarotCard card)
    {
        ModelState.Remove(nameof(TarotCard.ReadingCards));
        if (!ModelState.IsValid) return View("Edit", card);
        var duplicate = await _db.TarotCards.AnyAsync(x => x.SortOrder == card.SortOrder && x.Id != card.Id);
        if (duplicate) { ModelState.AddModelError(nameof(card.SortOrder), "此排序編號已使用。"); return View("Edit", card); }
        if (card.Id == 0) _db.TarotCards.Add(card);
        else
        {
            var original = await _db.TarotCards.FindAsync(card.Id);
            if (original == null) return NotFound();
            original.Name = card.Name; original.EnglishName = card.EnglishName;
            original.Arcana = card.Arcana; original.Suit = card.Suit; original.SortOrder = card.SortOrder;
            original.UprightMeaning = card.UprightMeaning; original.ReversedMeaning = card.ReversedMeaning; original.Symbol = card.Symbol;
        }
        await _db.SaveChangesAsync();
        TempData["Notice"] = "牌卡資料已儲存。";
        return RedirectToAction(nameof(Index));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var card = await _db.TarotCards.FindAsync(id);
        if (card == null) return NotFound();
        if (await _db.ReadingCards.AnyAsync(x => x.TarotCardId == id))
        {
            TempData["Notice"] = "此牌已有占卜紀錄，為保留歷史資料，不能直接刪除。";
            return RedirectToAction(nameof(Index));
        }
        _db.TarotCards.Remove(card);
        await _db.SaveChangesAsync();
        TempData["Notice"] = "牌卡已刪除。";
        return RedirectToAction(nameof(Index));
    }
}
