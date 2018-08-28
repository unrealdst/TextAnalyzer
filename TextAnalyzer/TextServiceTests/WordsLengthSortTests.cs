using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using TextService.Interfaces;
using TextService.SortStragety;

namespace TextServiceTests
{
    [TestClass]
    public class WordsLengthSortTests
    {
        private Mock<IRegExProvider> RegExProvider;

        private WordsLengthSort WordsLengthSort;

        [TestInitialize]
        public void TestInitialize()
        {
            RegExProvider = new Mock<IRegExProvider>();

            WordsLengthSort = new WordsLengthSort(RegExProvider.Object);
        }

        [TestMethod]
        public void Sort_ParameterIsNull_Expetion()
        {
            //When,Then
            Assert.ThrowsException<ArgumentNullException>(() => { WordsLengthSort.Sort(null, true); });
        }

        [TestMethod]
        public void Sort_TextMatch_Result()
        {
            //Given
            var text = "abc";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = WordsLengthSort.Sort(text, true);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.SortedText.Count());
            Assert.AreEqual("abc", result.SortedText.First());
        }

        [TestMethod]
        public void Sort_FirstWorldIsLonger_Result()
        {
            //Given
            var text = "abc ab";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = WordsLengthSort.Sort(text, true);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.SortedText.Count());
            Assert.AreEqual("ab", result.SortedText.First());
            Assert.AreEqual("abc", result.SortedText.Last());
        }

        [TestMethod]
        public void Sort_SecondtWorldIsLonger_Result()
        {
            //Given
            var text = "ab abc";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = WordsLengthSort.Sort(text, true);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.SortedText.Count());
            Assert.AreEqual("ab", result.SortedText.First());
            Assert.AreEqual("abc", result.SortedText.Last());
        }

        [TestMethod]
        public void Sort_FirstWorldIsLongerAndDescending_RevrtedResult()
        {
            //Given
            var text = "abc ab";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = WordsLengthSort.Sort(text, false);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.SortedText.Count());
            Assert.AreEqual("abc", result.SortedText.First());
            Assert.AreEqual("ab", result.SortedText.Last());
        }

        [TestMethod]
        public void Sort_SecondtWorldIsLongerAndDescending_RevrtedResult()
        {
            //Given
            var text = "ab abc";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = WordsLengthSort.Sort(text, false);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.SortedText.Count());
            Assert.AreEqual("abc", result.SortedText.First());
            Assert.AreEqual("ab", result.SortedText.Last());
        }


    }
}