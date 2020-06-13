using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTradingSystem
{
    class Market
    {
        private List<Account> _accounts = new List<Account>();
        public Market()
        {

        }
        public void AddAccount(Account account)
        {
            this._accounts.Add(account);
        }

        public void PrintAllAccount()
        {
            foreach (Account account in this._accounts)
            {
                account.PrintAccount();
            }
        }

        public Account Search(string name)
        {
            foreach (Account account in this._accounts)
            {
                if (account.Name == name)
                {
                    return account;
                }
            }
            //return null if cannot find any account
            return null;
        }
        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
        }
    }
}
