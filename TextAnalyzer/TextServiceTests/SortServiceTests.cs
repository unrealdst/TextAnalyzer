using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TextService.Interfaces;
using TextService.Models;
using TextService.Services;

namespace TextServiceTests
{
    [TestClass]
    public class SortServiceTests
    {
        private Mock<IRegExProvider> RegExProvider;

        private SortService SortService;

        [TestInitialize]
        public void TestInitialize()
        {
            RegExProvider = new Mock<IRegExProvider>();

            SortService = new SortService(RegExProvider.Object);
        }

        [TestMethod]
        public void SortText_ParameterIsNull_Expetion()
        {
            //When,Then
            Assert.ThrowsException<ArgumentNullException>(() => { SortService.SortText(null); });
        }

        [TestMethod]
        public void SortText_ParameterPropertyIsNull_Expetion()
        {
            //Given
            var parameters = new SortParameters();

            //When,Then
            Assert.ThrowsException<ArgumentNullException>(() => { SortService.SortText(parameters); });
        }

        [TestMethod]
        public void SortText_Asc_SortAsc()
        {
            //Given
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(new List<string>() { "1", "2", "3" });

            var parameters = new SortParameters()
            {
                Asc = true,
                SortOptions = SortOptions.AlphabeticOrder,
                Text = "Text"
            };

            //When
            SortTextModel result = SortService.SortText(parameters);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.SortedText.Count());
            Assert.AreEqual("1", result.SortedText.First());
            Assert.AreEqual("2", result.SortedText.ElementAt(1));
            Assert.AreEqual("3", result.SortedText.Last());
        }

        [TestMethod]
        public void SortText_Desc_SortDesc()
        {
            //Given
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(new List<string>() { "1", "2", "3" });

            var parameters = new SortParameters()
            {
                Asc = false,
                SortOptions = SortOptions.AlphabeticOrder,
                Text = "Text"
            };

            //When
            SortTextModel result = SortService.SortText(parameters);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.SortedText.Count());
            Assert.AreEqual("3", result.SortedText.First());
            Assert.AreEqual("2", result.SortedText.ElementAt(1));
            Assert.AreEqual("1", result.SortedText.Last());
        }
    }
}

