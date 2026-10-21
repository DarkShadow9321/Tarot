namespace TarotWeb.Models;
public sealed class ReadingCard
{
    public int Id { get; set; }
    public int TarotReadingId { get; set; }
    public TarotReading TarotReading { get; set; } = null!;
    public int TarotCardId { get; set; }
    public TarotCard TarotCard { get; set; } = null!;
    public int Position { get; set; }
    public bool IsReversed { get; set; }
}
