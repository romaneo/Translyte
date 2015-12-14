using System;
using NUnit.Framework;
using Translyte.Core.Models;
using Translyte.Core.Parsers;
using Translyte.Core;

namespace Translyte.Test
{
    [TestFixture]
    public class FB2ParserTest
    {

        [SetUp]
        public void Setup() { }


        [TearDown]
        public void Tear() { }

        [Test]
        public void ParseExistingBookTest()
        {
            Book book = new BookFullModel("/sdcard/translyte/Starik.fb2");
            Book.Load(ref book);
            Assert.AreNotEqual(null, book.Title);
        }
        [Test]
        public void ParseNotExistingBookTest()
        {
            Book book = new BookFullModel("/sdcard/translyte/notexists.fb2");
            Book.Load(ref book);
            Assert.AreNotEqual(null, book.Title);
            Assert.Pass(book.Title);
        }
        [Test]
        public void CheckBookAuthorTest()
        {
            Book book = new BookFullModel("/sdcard/translyte/Starik.fb2");
            Book.Load(ref book);
            Assert.AreNotEqual(null, book.Author);
            Assert.Pass(book.Author);
        }
        [Test]
        public void CheckBookAnnotationTest()
        {
            Book book = new BookFullModel("/sdcard/translyte/Starik.fb2");
            Book.Load(ref book);
            Assert.AreNotEqual(null, ((BookFullModel)book).Annotation);
            Assert.Pass(((BookFullModel)book).Annotation);
        }
    }
}