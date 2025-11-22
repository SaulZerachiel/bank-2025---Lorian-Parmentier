using System;

namespace Bank2025
{
    public class SavingsAccount : Account
    {
        public DateTime DateLastWithdraw { get; private set; }

        public SavingsAccount(string number, Person owner) : base(number, owner)
        {
            DateLastWithdraw = DateTime.Now;
        }

        public SavingsAccount(string number, Person owner, double initialBalance) : base(number, owner, initialBalance)
        {
            DateLastWithdraw = DateTime.Now;
        }

        protected override bool CanWithdraw(double amount)
        {
            return Balance >= amount;
        }

        protected override double CalculInterets()
        {
            return Balance * 0.045;
        }

        protected override void PostWithdrawHook(double previousBalance, double newBalance)
        {
            DateLastWithdraw = DateTime.Now;
            base.PostWithdrawHook(previousBalance, newBalance);
        }
    }
}
