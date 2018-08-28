using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TextService.Interfaces;
using TextService.Models;
using TextService.Services;

namespace TextServiceTests
{
    [TestClass]
    public class StatisticServiceTests
    {
        private Mock<IRegExProvider> RegExProvider;

        private StatisticService StatisticService;

        [TestInitialize]
        public void TestInitialize()
        {
            RegExProvider = new Mock<IRegExProvider>();

            StatisticService = new StatisticService(RegExProvider.Object);
        }

        [TestMethod]
        public void GetStatistic_ParameterIsNull_Expetion()
        {
            //When,Then
            Assert.ThrowsException<ArgumentNullException>(() => { StatisticService.GetStatistic(null); });
        }

        [TestMethod]
        public void GetStatistic_ParameterPropertyIsNull_Expetion()
        {
            //Given
            var parameters = new StatisticParameters();

            //When,Then
            Assert.ThrowsException<ArgumentNullException>(() => { StatisticService.GetStatistic(parameters); });
        }

        [TestMethod]
        public void GetStatistic_ParameterPropertyIsNotNull_CallRegexService()
        {
            //Given
            RegExProvider.Setup(x => x.GetMatchCount(It.IsAny<string>(), It.IsAny<string>())).Returns(1);
            var parameters = new StatisticParameters() { Text = "Text" };

            //When
            var result = StatisticService.GetStatistic(parameters);

            //Then
            Assert.AreEqual(1, result.Hyphens);
            Assert.AreEqual(1, result.Spaces);
            Assert.AreEqual(1, result.Words);
            RegExProvider.Verify(x => x.GetMatchCount(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
        }


    }
}