using System;
using System.Collections.Generic;

namespace Bank2025
{
    public class Bank
    {
        public string Name { get; set; }
        // Exercise 3 & 8: Bank works with Account
        public Dictionary<string, Account> Accounts { get; } = new();

        // Exercise 22: expose an Action<Account> that will be called when any account goes negative
        public event Action<Account>? NegativeBalanceAction;

        public Bank(string name)
        {
            Name = name;
        }

        public void AddAccount(Account account)
        {
            Accounts[account.Number] = account;
            // subscribe to the account's NegativeBalanceEvent so Bank can handle it
            account.NegativeBalanceEvent += HandleNegativeBalance;
        }

        public void DeleteAccount(string number)
        {
            if (Accounts.TryGetValue(number, out var acc))
            {
                // unsubscribe to avoid memory leaks
                acc.NegativeBalanceEvent -= HandleNegativeBalance;
            }
            Accounts.Remove(number);
        }

        public double GetBalance(string number)
        {
            if (!Accounts.TryGetValue(number, out var acc))
                throw new KeyNotFoundException($"Account {number} not found");
            return acc.Balance;
        }

        public double SumOfPerson(Person p)
        {
            double sum = 0;
            foreach (var a in Accounts.Values)
                if (a.Owner == p)
                    sum += a.Balance;
            return sum;
        }

        // This is the method that will handle the event and print the message to console
        public void HandleNegativeBalance(Account account)
        {
            Console.WriteLine($"Le numéro de compte {account.Number} vient de passer en négatif");
            // also forward to external subscribers
            NegativeBalanceAction?.Invoke(account);
        }
    }
}
