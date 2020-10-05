using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Shop
    {
        private int _gold;
        private Item[] _inventory;

        //shop constructor
        public Shop()
        {
            _inventory = new Item[5];
            _gold = 4;
        }

        //overload shop constructor
        public Shop(Item[] item)
        {
            item = _inventory;
            _gold = 4;
        }

        /*
        public bool Sell(int shopIndex, int playerIndex)
        {
            //find item to buy in inventory
            Item itemToBuy = _inventory[shopIndex];
            //checks if purchase sucsesfully
            if (Game.Buy(itemToBuy, playerIndex))
            {
                //increase shop gold buyt item cost to complete transaction 
                _gold += itemToBuy.cost;
                return true;
            }
            return false;
        }
        */
    }
}
