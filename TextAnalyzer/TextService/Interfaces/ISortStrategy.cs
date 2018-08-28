using TextService.Models;

namespace TextService.Interfaces
{
    internal interface ISortStrategy
    {
        SortTextModel Sort(string text, bool asc);
    }
}
