using System;
using NUnit.Framework;
using Translyte.Core;

namespace Translyte.Test
{
    [TestFixture]
    public class TestsSample
    {

        [SetUp]
        public void Setup() { }


        [TearDown]
        public void Tear() { }

        [Test]
        public async void TranslateTest()
        {
            var tr = new Translator("en", "ru");
            var res = await tr.Translate("book");
            Assert.Pass(res);
        }
    }
}