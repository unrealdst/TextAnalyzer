using TextService.Models;

namespace TextService.Interfaces
{
    public interface IStatisticService
    {
        StatisticModel GetStatistic(StatisticParameters parameters);
    }
}