using System;
using NUnit.Framework;
using SharpGenerator.Helpers;

namespace SharpGenerator.Tests.Helpers
{
    [TestFixture]
    public class StrHelperTests
    {
        [Test]
        public void TestMethod()
        {
            string h = "hello_world";
            Assert.AreEqual("hello world", h.ToWords());
            Assert.AreEqual("HelloWorld", h.ToPascalCase());
            Assert.AreEqual("HW", "h w".ToPascalCase());
            Assert.AreEqual("HW", "h_w".ToPascalCase());
            Assert.AreEqual("Hello", "Hello".ToPascalCase());
            Assert.AreEqual("helloWorld", h.ToCamelCase());
            Assert.AreEqual("hELLOWORLD", "HELLO WORLD".ToCamelCase());
            Assert.AreEqual("HELLOWORLD", "HELLO WORLD".ToPascalCase());
            Assert.AreEqual("h", "H".ToCamelCase());
        }
    }
}
