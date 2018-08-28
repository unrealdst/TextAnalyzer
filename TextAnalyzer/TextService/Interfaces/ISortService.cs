using TextService.Models;

namespace TextService.Interfaces
{
    public interface ISortService
    {
        SortTextModel SortText(SortParameters parameters);
    }
}
