using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasierWorship.Bible.Test
{
    [TestClass]
    public class BibleTests
    {
        [TestMethod]
        public void GetVerse_HasResult()
        {
            var bible = new Data.Bible("niv2011.sqlite3");

            var res = bible.GetVerse(Data.Bible.Book.Psalms, 119, 10);

            Assert.IsTrue(res != null);
        }

        [TestMethod]
        public void GetVerse_HasNoResult()
        {
            var bible = new Data.Bible("niv2011.sqlite3");

            var res = bible.GetVerse(Data.Bible.Book.Habakkuk, 1, 400);

            Assert.IsTrue(res == null);
        }

        [TestMethod]
        public void GetVerses_HasManyResults()
        {
            var bible = new Data.Bible("niv2011.sqlite3");

            var res = bible.GetVerses(Data.Bible.Book.Psalms, 119, 100, 176);

            Assert.IsTrue(res.Count() > 1);
        }
    }
}
