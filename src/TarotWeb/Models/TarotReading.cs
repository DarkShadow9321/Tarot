using System.ComponentModel.DataAnnotations;
namespace TarotWeb.Models;
public sealed class TarotReading
{
    public int Id { get; set; }
    [Required, StringLength(200)] public string Question { get; set; } = "";
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public int DrawCount { get; set; }
    public List<ReadingCard> Cards { get; set; } = new();
}
