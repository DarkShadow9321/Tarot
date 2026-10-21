using System.ComponentModel.DataAnnotations;
namespace TarotWeb.Models;
public sealed class TarotCard
{
    public int Id { get; set; }
    [Required, StringLength(80)] public string Name { get; set; } = "";
    [Required, StringLength(80)] public string EnglishName { get; set; } = "";
    [Required, StringLength(20)] public string Arcana { get; set; } = "";
    [Required, StringLength(30)] public string Suit { get; set; } = "";
    public int SortOrder { get; set; }
    [Required, StringLength(500)] public string UprightMeaning { get; set; } = "";
    [Required, StringLength(500)] public string ReversedMeaning { get; set; } = "";
    [StringLength(12)] public string Symbol { get; set; } = "✦";
    public List<ReadingCard> ReadingCards { get; set; } = new();
}
