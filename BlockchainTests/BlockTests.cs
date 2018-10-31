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
    public class BlockTests
    {
        [TestMethod()]
        public void SerializeTest()
        {
            var block = new Block();
            var json = "{\"Created\":\"\\/Date(1535749200000+0300)\\/\",\"Data\":\"Hello, World\",\"Hash\":\"7640ad8f54c6ba1e228a869137ec50a502e190b3cb9fafea68bca1dc453b73dd\",\"PreviousHash\":\"111111\",\"User\":\"Admin\"}";

            var resultSring = block.Serialize();

            Assert.AreEqual(json, resultSring);

            var resultBlock = Block.Deserialize(resultSring);

            Assert.AreEqual(block.Hash, resultBlock.Hash);
            Assert.AreEqual(block.Created, resultBlock.Created);
            Assert.AreEqual(block.Data, resultBlock.Data);
            Assert.AreEqual(block.PreviousHash, resultBlock.PreviousHash);
            Assert.AreEqual(block.User, resultBlock.User);
        }
    }
}