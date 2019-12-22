using System;
using NUnit.Framework;
using SharpGenerator.Models;

namespace SharpGenerator.Tests.Models
{
    [TestFixture]
    public class ClassTests
    {
        [Test]
        public void TestToString()
        {
            var c = new Class();
            c.Namespace = "SomeProject.Models";
            c.Name = "User";
            c.AddProperty("UserID", "int");
            c.AddProperty("Name");
            
            Console.WriteLine(c.ToString());
        }
    }
}
