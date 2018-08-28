using Common;
using System;
using TextService.Interfaces;
using TextService.Models;
using TextService.SortStragety;

namespace TextService.Services
{
    public class SortService : ISortService
    {
        private readonly IRegExProvider RegExProvider;

        public SortService(IRegExProvider regExProvider)
        {
            RegExProvider = regExProvider;
        }

        public SortTextModel SortText(SortParameters parameters)
        {
            if (parameters == null || parameters.Text == null)
            {
                throw new ArgumentNullException();
            }
            var sortStrategy = GetSortStrategy(parameters.SortOptions);
            SortTextModel result = sortStrategy.Sort(parameters.Text, parameters.Asc);
            return result;
        }

        private ISortStrategy GetSortStrategy(SortOptions option)
        {
            switch (option)
            {
                case SortOptions.WordsLength:
                    return new WordsLengthSort(RegExProvider);
                case SortOptions.DigitQuantiy:
                    return new DigitQuantiySort(RegExProvider);
                case SortOptions.AlphabeticOrder:
                    return new AlphabeticOrderSort(RegExProvider);
            }
            return null;
        }
    }
}
