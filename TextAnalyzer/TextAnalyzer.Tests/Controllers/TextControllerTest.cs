using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TextAnalyzer;
using TextAnalyzer.Controllers;
using TextAnalyzer.Models;
using TextService.Interfaces;

namespace TextAnalyzer.Tests.Controllers
{
    [TestClass]
    public class TextControllerTest
    {
        private Mock<IStatisticService> statisticService;
        private Mock<ISortService> sortService;
        private TextController textController;

        [TestInitialize]
        public void TestInitialize()
        {
            statisticService = new Mock<IStatisticService>();
            sortService = new Mock<ISortService>();

            textController = new TextController(statisticService.Object, sortService.Object);
        }

        [TestMethod]
        public void Ping_ReturnValue()
        {
            // When
            var result = textController.Ping();

            // Then
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void GenerateStatistics_ReturnValue()
        {
            //Given
            var input = new TextInputModel()
            {
                File = new HttpPostedFileBase()
                {
                    file
                }
            };

            // When
            var result = textController.GenerateStatistics(input);

            // Then
            Assert.IsNotNull(result);
        }
    }
}
