using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTradingSystem
{
    class Transaction
    {
        Account _fromAccount;
        Account _toAccount;
        Item _fromItem;
        Item _toItem;
        public Transaction (Account fromAccount, Account toAccount, Item fromItem, Item toItem)
        {
            this._fromAccount = fromAccount;
            this._toAccount = toAccount;
            this._fromItem = fromItem;
            this._toItem = toItem;
        }
        
        public void Execute()
        {
            this._fromAccount.RemoveItem(_fromItem);
            this._toAccount.RemoveItem(_toItem);
            this._fromAccount.AddItem(_toItem);
            this._toAccount.AddItem(_fromItem);
        }
    }
}
