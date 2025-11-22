using System;
using Bank2025;

class Program
{
    static void Main()
    {
        var bank = new Bank("IFOSUP Bank");

        // subscribe bank-level handler for negative balances
        bank.NegativeBalanceAction += bank.HandleNegativeBalance;

        var alice = new Person("Alice", "Dupont", new DateTime(1995,5,1));
        var bob = new Person("Bob", "Martin", new DateTime(1990,3,2));

        var ca = new CurrentAccount("CA-1001", alice, credit: 200);
        var sa = new SavingsAccount("SA-2001", alice, initialBalance: 1000);
        var cb = new CurrentAccount("CA-3001", bob, credit: 0);

        bank.AddAccount(ca);
        bank.AddAccount(sa);
        bank.AddAccount(cb);

        ca.Deposit(50);
        Console.WriteLine($"Balance CA-1001 before withdraw: {ca.Balance}");
        // This withdraw will push it negative (50 - 300 = -250) -> credit is 200, so allowed but negative
        ca.Withdraw(300);

        // Apply interest to all accounts
        foreach (var acc in bank.Accounts.Values)
        {
            acc.ApplyInterest();
            Console.WriteLine($"After interest: {acc.Number} => {acc.Balance:F2}");
        }

        Console.WriteLine($"Sum for Alice: {bank.SumOfPerson(alice)}");

        // Demonstrate exception on invalid deposit/withdraw
        try {
            sa.Deposit(0);
        } catch (ArgumentOutOfRangeException ex) {
            Console.WriteLine("Caught expected ArgumentOutOfRangeException on deposit: " + ex.Message);
        }

        try {
            cb.Withdraw(5000); // should throw insufficient
        } catch (InsufficientBalanceException ex) {
            Console.WriteLine("Caught expected InsufficientBalanceException: " + ex.Message);
        }

        Console.WriteLine("Done.");
    }
}
