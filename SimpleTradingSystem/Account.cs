using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTradingSystem
{
    class Account
    {
        private string _name;
        private string _address;
        private List<Item> _items;
        public Account(string name)
        {
            this._name = name;
            this._items = new List<Item>();
        }
        public string Name { get => this._name; }
        public List<Item> Items { get => this._items; }

        public void AddItem(Item item)
        {
            this._items.Add(item);
        }
        public void RemoveItem(Item item)
        {
            this._items.Remove(item);
        }

        public void PrintAccount()
        {
            Console.WriteLine($"{this._name}");
        }
        public void PrintItems()
        {
            if (this._items != null)
            {
                for (int i = 0; i < this._items.Count; i++)
                {
                    Console.WriteLine("{0,15}", $"Item #{i + 1} { new string('-', 10)}");
                    this._items[i].Print();
                    Console.WriteLine();
                }
            }
            else Console.WriteLine("There is no item to display!");

        }
    }
}
