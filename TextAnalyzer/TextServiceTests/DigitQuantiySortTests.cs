﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using TextService.Interfaces;
using TextService.SortStragety;

namespace TextServiceTests
{
    [TestClass]
    public class DigitQuantiySortTests
    {
        private Mock<IRegExProvider> RegExProvider;

        private DigitQuantiySort DigitQuantiySort;

        [TestInitialize]
        public void TestInitialize()
        {
            RegExProvider = new Mock<IRegExProvider>();

            DigitQuantiySort = new DigitQuantiySort(RegExProvider.Object);
        }

        [TestMethod]
        public void Sort_ParameterIsNull_Expetion()
        {
            //When,Then
            Assert.ThrowsException<ArgumentNullException>(() => { DigitQuantiySort.Sort(null, true); });
        }

        [TestMethod]
        public void Sort_TextMatch_Result()
        {
            //Given
            var text = "abc";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = DigitQuantiySort.Sort(text, true);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.SortedText.Count());
            Assert.AreEqual("abc", result.SortedText.First());
        }

        [TestMethod]
        public void Sort_TwoMatch_Result()
        {
            //Given
            var text = "abc abc";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = DigitQuantiySort.Sort(text, true);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.SortedText.Count());
            Assert.AreEqual("abc", result.SortedText.First());
            Assert.AreEqual("abc", result.SortedText.Last());
        }

        [TestMethod]
        public void Sort_TwoDifferentMatch_Result()
        {
            //Given
            var text = "abc efg";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = DigitQuantiySort.Sort(text, true);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.SortedText.Count());
            Assert.AreEqual("abc", result.SortedText.First());
            Assert.AreEqual("efg", result.SortedText.Last());
        }

        [TestMethod]
        public void Sort_NumberInWorld_Result()
        {
            //Given
            var text = "abc2";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = DigitQuantiySort.Sort(text, true);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.SortedText.Count());
            Assert.AreEqual("abc2", result.SortedText.First());
        }

        [TestMethod]
        public void Sort_NumberInBothWorlds_BothInResult()
        {
            //Given
            var text = "abc2 efg2";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = DigitQuantiySort.Sort(text, true);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.SortedText.Count());
            Assert.AreEqual("abc2", result.SortedText.First());
            Assert.AreEqual("efg2", result.SortedText.Last());
        }

        [TestMethod]
        public void Sort_FirstLetterSame_MatchBySecond()
        {
            //Given
            var text = "ab ac";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = DigitQuantiySort.Sort(text, true);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.SortedText.Count());
            Assert.AreEqual("ab", result.SortedText.First());
            Assert.AreEqual("ac", result.SortedText.Last());
        }

        [TestMethod]
        public void Sort_RevertedInputAndDesc_Result()
        {
            //Given
            var text = "ac ab";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = DigitQuantiySort.Sort(text, false);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.SortedText.Count());
            Assert.AreEqual("ac", result.SortedText.First());
            Assert.AreEqual("ab", result.SortedText.Last());
        }

        [TestMethod]
        public void Sort_OneHAveNumberSecondNot_Result()
        {
            //Given
            var text = "ac1 ab";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = DigitQuantiySort.Sort(text, false);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.SortedText.Count());
            Assert.AreEqual("ac1", result.SortedText.First());
            Assert.AreEqual("ab", result.SortedText.Last());
        }

        [TestMethod]
        public void Sort_FirstDontHaveNumberSecondHAve_Result()
        {
            //Given
            var text = "ac ab1";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = DigitQuantiySort.Sort(text, false);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.SortedText.Count());
            Assert.AreEqual("ab1", result.SortedText.First());
            Assert.AreEqual("ac", result.SortedText.Last());
        }

        [TestMethod]
        public void Sort_FirstHaveNumberSecondHaveTwo_Result()
        {
            //Given
            var text = "ac1 ab12";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = DigitQuantiySort.Sort(text, false);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.SortedText.Count());
            Assert.AreEqual("ab12", result.SortedText.First());
            Assert.AreEqual("ac1", result.SortedText.Last());
        }

        [TestMethod]
        public void Sort_FirstHaveTwoNumberSecondHaveone_Result()
        {
            //Given
            var text = "ab12 ab1";
            RegExProvider.Setup(x => x.GetMatches(It.IsAny<string>(), It.IsAny<string>())).Returns(text.Split(' '));

            //When
            var result = DigitQuantiySort.Sort(text, false);

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.SortedText.Count());
            Assert.AreEqual("ab12", result.SortedText.First());
            Assert.AreEqual("ab1", result.SortedText.Last());
        }
    }
}