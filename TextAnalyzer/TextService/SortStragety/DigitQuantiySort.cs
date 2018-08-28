﻿using System;
using System.Collections.Generic;
using System.Linq;
using TextService.Interfaces;
using TextService.Models;

namespace TextService.SortStragety
{
    public class DigitQuantiySort : ISortStrategy
    {
        private readonly IRegExProvider regExProvider;

        private const string Match = @"\d?\w+\d?";
        private readonly Func<string, int> orderExpression = x => x.ToCharArray().Where(y => (y > '0' && y < '9')).Count();

        public DigitQuantiySort(IRegExProvider regExProvider)
        {
            this.regExProvider = regExProvider;
        }

        public SortTextModel Sort(string text, bool asc)
        {
            if (text == null)
            {
                throw new ArgumentNullException();
            }

            IEnumerable<string> matches = regExProvider.GetMatches(text, Match);
            var result = new SortTextModel()
            {
                SortedText = Sort(matches, asc)
            };

            return result;
        }

        private IEnumerable<string> Sort(IEnumerable<string> matches, bool asc)
        {
            if (asc)
            {
                return matches.OrderBy(orderExpression);
            }
            return matches.OrderByDescending(orderExpression);
        }
    }
}
