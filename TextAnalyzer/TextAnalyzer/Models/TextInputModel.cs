using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TextAnalyzer.Models
{
    public class TextInputModel
    {
        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}