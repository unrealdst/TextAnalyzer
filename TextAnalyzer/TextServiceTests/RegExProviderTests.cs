using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TextService.Services;

namespace TextServiceTests
{
    [TestClass]
    public class RegExProviderTests
    {

        private RegExProvider RegExProvider;

        [TestInitialize]
        public void TestInitialize()
        {
            RegExProvider = new RegExProvider();
        }

        [TestMethod]
        public void GetMatches_TextIsNull_Expetion()
        {
            //When,Then
            Assert.ThrowsException<ArgumentNullException>(() => { RegExProvider.GetMatches(null, "a"); });
        }

        [TestMethod]
        public void GetMatches_RegExIsNull_Expetion()
        {
            //When,Then
            Assert.ThrowsException<ArgumentNullException>(() => { RegExProvider.GetMatches("a", null); });
        }

        [TestMethod]
        public void GetMatches_BothParmeterAreNull_Expetion()
        {
            //When,Then
            Assert.ThrowsException<ArgumentNullException>(() => { RegExProvider.GetMatches(null, null); });
        }

        [TestMethod]
        public void GetMatches_TextNotContainMatches_EmptyCollection()
        {
            //When
            var result = RegExProvider.GetMatches("123", "a");

            //Then
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetMatches_TextContainMatches_Collection()
        {
            //When
            var result = RegExProvider.GetMatches("123", "1");

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("1", result.First());
        }


        [TestMethod]
        public void GetMatches_TextContainMultiMatches_Collection()
        {
            //When
            var result = RegExProvider.GetMatches("123 1", "1");

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("1", result.First());
            Assert.AreEqual("1", result.Last());
        }

        [TestMethod]
        public void GetMatchCount_TextIsNull_Expetion()
        {
            //When,Then
            Assert.ThrowsException<ArgumentNullException>(() => { RegExProvider.GetMatchCount(null, "a"); });
        }

        [TestMethod]
        public void GetMatchCount_RegExIsNull_Expetion()
        {
            //When,Then
            Assert.ThrowsException<ArgumentNullException>(() => { RegExProvider.GetMatchCount("a", null); });
        }

        [TestMethod]
        public void GetMatchCount_BothParmeterAreNull_Expetion()
        {
            //When,Then
            Assert.ThrowsException<ArgumentNullException>(() => { RegExProvider.GetMatchCount(null, null); });
        }

        [TestMethod]
        public void GetMatchCount_TextNotContainMatches_Zero()
        {
            //When
            var result = RegExProvider.GetMatchCount("123", "a");

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetMatchCount_TextContainMatches_One()
        {
            //When
            var result = RegExProvider.GetMatchCount("123", "1");

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }


        [TestMethod]
        public void GetMatchCount_TextContainMultiMatches_Two()
        {
            //When
            var result = RegExProvider.GetMatchCount("123 1", "1");

            //Then
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }

    }
}