using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTradingSystem
{
    class Transaction
    {
        Account _fromAccount;
        Account _toAccount;
        Item _item;
        public Transaction (Account fromAccount, Account toAccount, Item item)
        {
            this._fromAccount = fromAccount;
            this._toAccount = toAccount;
            this._item = item;
        }
        
        public void Execute()
        {
            this._fromAccount.RemoveItem(this._item);
            this._toAccount.AddItem(this._item);
        }
    }
}
