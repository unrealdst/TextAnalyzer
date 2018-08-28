using Common;
using System.ComponentModel.DataAnnotations;

namespace TextAnalyzer.Models
{
    public class SortOptionInputModel
    {
        [Required]
        public SortOptions SortOptions { get; set; }
        [Required]
        public bool Asc { get; set; }
    }
}