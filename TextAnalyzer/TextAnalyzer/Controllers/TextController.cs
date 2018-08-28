using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TextAnalyzer.Models;
using TextService.Interfaces;
using TextService.Models;
using TextService.Services;

namespace TextAnalyzer.Controllers
{
    public class TextController : Controller
    {
        private readonly IStatisticService statisticService;
        private readonly ISortService sortService;

        public TextController()
        {
            IRegExProvider regExProvider = new RegExProvider();
            this.statisticService = new StatisticService(regExProvider);
            this.sortService = new SortService(regExProvider);
        }

        public TextController(IStatisticService statisticService, ISortService sortService)
        {
            this.statisticService = statisticService;
            this.sortService = sortService;
        }

        [HttpPost]
        public ActionResult Sort(TextInputModel text, SortOptionInputModel sortOption)
        {
            if (!ModelState.IsValid)
            {
                return GetErrorResponse();
            }

            SortParameters parameters = new SortParameters()
            {
                Text = GetText(text.File),
                SortOptions = sortOption.SortOptions,
                Asc = sortOption.Asc
            };

            var order = sortService.SortText(parameters);
            return Json(order);
        }

        [HttpPost]
        public ActionResult GenerateStatistics(TextInputModel text)
        {
            if (!ModelState.IsValid)
            {
                return GetErrorResponse();
            }

            StatisticParameters parameters = new StatisticParameters() { Text = GetText(text.File) };
            var statistic = this.statisticService.GetStatistic(parameters);

            var textStatistics = new TextStatisticsResultModel()
            {
                Hyphens = statistic.Hyphens,
                Spaces = statistic.Spaces,
                Words = statistic.Words
            };

            return Json(textStatistics);
        }


        [HttpGet]
        public ActionResult Ping()
        {
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        private string GetText(HttpPostedFileBase file)
        {
            BinaryReader binaryReader = new BinaryReader(file.InputStream);
            byte[] binData = binaryReader.ReadBytes(file.ContentLength);
            return System.Text.Encoding.UTF8.GetString(binData);
        }

        private ActionResult GetErrorResponse()
        {
            var sb = new StringBuilder();
            foreach (var error in ModelState)
            {
                if (error.Value.Errors.Any())
                {
                    sb.Append($"{error.Key}: {string.Join(", ", error.Value.Errors.Select(x => x.ErrorMessage))}");
                }
            }
            return Json(sb.ToString());
        }
    }
}