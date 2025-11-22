using System;

namespace Bank2025
{
    public class CurrentAccount : Account
    {
        private double creditLine;
        public double CreditLine
        {
            get => creditLine;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "CreditLine must be >= 0");
                creditLine = value;
            }
        }

        public CurrentAccount(string number, Person owner, double credit = 0) : base(number, owner)
        {
            CreditLine = credit;
        }

        public CurrentAccount(string number, Person owner, double credit, double initialBalance) : base(number, owner, initialBalance)
        {
            CreditLine = credit;
        }

        protected override bool CanWithdraw(double amount)
        {
            return Balance + CreditLine >= amount;
        }

        protected override double CalculInterets()
        {
            // If positive balance -> 3%, otherwise -9.75% (charge)
            return Balance >= 0 ? Balance * 0.03 : Balance * 0.0975;
        }

        protected override void PostWithdrawHook(double previousBalance, double newBalance)
        {
            // Exercise 21: trigger NegativeBalanceEvent only when account passes from non-negative to negative
            if (previousBalance >= 0 && newBalance < 0)
            {
                // trigger event defined in Account
                OnNegativeBalance();
            }
        }
    }
}
