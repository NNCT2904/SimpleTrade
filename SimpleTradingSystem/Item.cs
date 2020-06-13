using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTradingSystem
{
    class Item
    {
        private string _name;
        private string _description;

        public Item(string name, string description)
        {
            this._name = name; 
            this._description = description;
        }

        public string Name { get => this._name; }
        public string Description { get => this._description; }
        public void ChangeDescription(string newDes)
        {
            this._description = newDes;
        }
        public void Print()
        {
            Console.WriteLine($"Item name: {this._name}");
            Console.WriteLine($"Item description: \n{this._description}");
        }
    }
}
