using System.ComponentModel.DataAnnotations;

namespace DemoMvc.Models
{
    public class GradeModel
    {
        [Required]
        [Range(0, 10, ErrorMessage = "Điểm A phải từ 0 đến 10")]
        public double ScoreA { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Điểm B phải từ 0 đến 10")]
        public double ScoreB { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Điểm C phải từ 0 đến 10")]
        public double ScoreC { get; set; }

        public double CalculateFinalScore()
        {
            return (ScoreA * 0.6) + (ScoreB * 0.3) + (ScoreC * 0.1);
        }
    }
}