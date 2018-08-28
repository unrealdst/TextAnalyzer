using System.Collections.Generic;

namespace TextService.Interfaces
{
    public interface IRegExProvider
    {
        int GetMatchCount(string text, string regEx);
        IEnumerable<string> GetMatches(string text, string regEx);
    }
}
