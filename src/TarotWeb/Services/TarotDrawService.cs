using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using TarotWeb.Data;
using TarotWeb.Models;
using TarotWeb.ViewModels;
namespace TarotWeb.Services;
public sealed class TarotDrawService
{
    private readonly TarotDbContext _db;
    public TarotDrawService(TarotDbContext db) => _db = db;
    public async Task<TarotReading> DrawAsync(DrawForm form)
    {
        var cards = await _db.TarotCards.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
        if (cards.Count < form.DrawCount) throw new InvalidOperationException("牌組數量不足，無法完成抽牌。");
        // Fisher–Yates: shuffle once, draw distinct cards without replacement.
        for (var i = cards.Count - 1; i > 0; i--)
        {
            var j = RandomNumberGenerator.GetInt32(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
        var reading = new TarotReading { Question = form.Question.Trim(), DrawCount = form.DrawCount, CreatedAtUtc = DateTime.UtcNow };
        for (var i = 0; i < form.DrawCount; i++)
        {
            reading.Cards.Add(new ReadingCard { TarotCardId = cards[i].Id, Position = i + 1,
                IsReversed = RandomNumberGenerator.GetInt32(2) == 1 });
        }
        _db.TarotReadings.Add(reading);
        await _db.SaveChangesAsync();
        return reading;
    }
}
