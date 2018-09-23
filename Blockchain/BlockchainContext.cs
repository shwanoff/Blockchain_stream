using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Blockchain
{
    class BlockchainContext : DbContext
    {

        public BlockchainContext()
            : base("BlockchainConnection2")
        { }

        public DbSet<Block> Blocks { get; set; }
    }
}