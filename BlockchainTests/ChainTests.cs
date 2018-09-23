using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Tests
{
    [TestClass()]
    public class ChainTests
    {
        [TestMethod()]
        public void ChainTest()
        {
            var chain = new Chain();
            chain.Add("Code blog", "Admin");

            Assert.AreEqual("Code blog", chain.Last.Data);
        }

        [TestMethod()]
        public void CheckTest()
        {
            var chain = new Chain();
            chain.Add("hello, world", "Admin");
            chain.Add("code blog", "shwan");

            Assert.IsTrue(chain.Check());
        }
    }
}