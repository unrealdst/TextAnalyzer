using System.Text.RegularExpressions;
using TextService.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace TextService.Services
{
    public class RegExProvider : IRegExProvider
    {
        public int GetMatchCount(string text, string regEx)
        {
            var result = GetMatches(text, regEx).Count();
            return result;
        }

        public IEnumerable<string> GetMatches(string text, string regEx)
        {
            Regex regex = new Regex(regEx);
            MatchCollection match = regex.Matches(text);

            var result = match.Cast<Match>().Select(x => x.ToString());
            return result;
        }
    }
}
