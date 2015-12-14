using System;
using NUnit.Framework;
using Translyte.Core;

namespace Translyte.Test
{
    [TestFixture]
    public class TranslatorTest
    {

        [SetUp]
        public void Setup() { }


        [TearDown]
        public void Tear() { }

        [Test]
        public async void TranslateEnToRuTest()
        {
            var tr = new Translator("en", "ru");
            string text = "Book";
            var res = await tr.Translate(text);
            Assert.AreEqual("Книга", res);
        }

        [Test]
        public async void TranslateRuToEnTest()
        {
            var tr = new Translator("ru", "en");
            string text = "Кошка";
            var res = await tr.Translate(text);
            Assert.AreEqual("Cat", res);
        }

        [Test]
        public async void TranslateEmptyTextTest()
        {
            var tr = new Translator("en", "ru");
            string text = "";
            var res = await tr.Translate(text);
            Assert.AreEqual(null, res);
            Assert.Pass(res);
        }

        [Test]
        public async void TranslateEmptyLanguage()
        {
            var tr = new Translator("", "");
            string text = "Bike";
            var res = await tr.Translate(text);
            Assert.Inconclusive(res);
        }
    }
}