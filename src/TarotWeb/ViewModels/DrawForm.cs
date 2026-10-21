using System.ComponentModel.DataAnnotations;
namespace TarotWeb.ViewModels;
public sealed class DrawForm
{
    [Required(ErrorMessage = "請輸入想詢問的問題。")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "問題長度需介於 2 到 200 字。")]
    [Display(Name = "占卜問題")]
    public string Question { get; set; } = "";
    [Range(1, 3, ErrorMessage = "請選擇一張或三張牌。")]
    public int DrawCount { get; set; } = 3;
}
