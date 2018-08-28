using Common;

namespace TextService.Models
{
    public class SortParameters
    {
        public string Text { get; set; }

        public SortOptions SortOptions { get; set; }
        public bool Asc { get; set; }
    }
}
