using System;
using TextService.Interfaces;
using TextService.Models;

namespace TextService.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IRegExProvider regExProvider;

        public StatisticService(IRegExProvider regExProvider)
        {
            this.regExProvider = regExProvider;
        }

        public StatisticModel GetStatistic(StatisticParameters parameters)
        {
            if(parameters == null || parameters.Text == null)
            {
                throw new ArgumentNullException();
            }

            var result = new StatisticModel
            {
                Hyphens = regExProvider.GetMatchCount(parameters.Text, @"\-"),
                Spaces = regExProvider.GetMatchCount(parameters.Text, @"\s"),
                Words = regExProvider.GetMatchCount(parameters.Text, @"(\w|\d)*")
            };

            return result;
        }
    }
}
