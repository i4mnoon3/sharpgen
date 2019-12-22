using System;
using NUnit.Framework;

namespace SharpGenerator.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        Program p;
        
        [SetUp]
        public void Setup()
        {
            p = new Program();
        }
        
        [Test]
        public void TestShowHelp()
        {
            Program.Main(new string[] {});
        }
        
        [Test]
        public void TestRun()
        {
            p.Run(new string[] { "g", "Server=.\\sqlexpress;Database=healthwatch;Trusted_Connection=True;" });
        }
    }
}
