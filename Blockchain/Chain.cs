using System;
using System.Collections.Generic;
using System.Linq;

namespace Blockchain
{
    /// <summary>
    /// Цепочка блоков.
    /// </summary>
    public class Chain
    {
        /// <summary>
        /// Все блоки.
        /// </summary>
        public List<Block> Blocks { get; private set; }

        /// <summary>
        /// Последний добавленный блок.
        /// </summary>
        public Block Last { get; private set; }

        /// <summary>
        /// Создание новой цепочки.
        /// </summary>
        public Chain()
        {
            Blocks = LoadChainFromDB();

            if (Blocks.Count == 0)
            {
                var genesisBlock = new Block();

                Blocks.Add(genesisBlock);
                Last = genesisBlock;
                Save(Last);
            }
            else
            {
                if(Check())
                {
                    Last = Blocks.Last();
                }
                else
                {
                    throw new Exception("Ошибка получения блоков из базы данных. Цепочка не прошла проверку на целостность.");
                }

            }
        }

        /// <summary>
        /// Добавить блок.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="user"></param>
        public void Add(string data, string user)
        {
            var block = new Block(data, user, Last);
            Blocks.Add(block);
            Last = block;
            Save(Last);
        }

        /// <summary>
        /// Метод проверки корректности цепочки.
        /// </summary>
        /// <returns> true - цепочка корректна, false - цепочка не корректна. </returns>
        public bool Check()
        {
            var genesisBlock = new Block();
            var previosuHash = genesisBlock.Hash;

            foreach(var block in Blocks.Skip(1))
            {
                var hash = block.PreviousHash;

                if(previosuHash != hash)
                {
                    return false;
                }

                previosuHash = block.Hash;
            }

            return true;
        }

        /// <summary>
        /// Метод записи блока в базу данных.
        /// </summary>
        /// <param name="block"> Сохраняемый блок. </param>
        private void Save(Block block)
        {
            using (var db = new BlockchainContext())
            {
                db.Blocks.Add(block);
                db.SaveChanges(); //TODO: раскомментировать.
            }
        }

        /// <summary>
        /// Получение данных из базы данных в цепочку.
        /// </summary>
        /// <returns> Список блоков данных. </returns>
        private List<Block> LoadChainFromDB()
        {
            List<Block> result;

            using (var db = new BlockchainContext())
            {
                var count = db.Blocks.OrderByDescending(b => b.Id).Count();

                result = new List<Block>(count * 2);

                result.AddRange(db.Blocks);
            }

            return result;
        }

        private void Sync()
        {

        }
    }
}
