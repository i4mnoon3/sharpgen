using System;
using NUnit.Framework;
using SharpGenerator.Models;

namespace SharpGenerator.Tests.Models
{
    [TestFixture]
    public class TableTests
    {
        [Test]
        public void TestToClass()
        {
            var t = new Table("User");
            t.Database = new Database("HealthWatch");
            t.AddColumn("UserID", "integer", true, true, true);
            t.AddColumn("Name");
            
            var c = t.ToClass();
            Assert.AreEqual(c.Properties[0].Type, "int");
            Assert.AreEqual(c.Properties[1].Type, "string");
        }
    }
}
