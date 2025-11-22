using System;

namespace Bank2025
{
    public abstract class Account : IBankAccount
    {
        public string Number { get; private set; }
        public Person Owner { get; private set; }
        public double Balance { get; private set; }

        // Exercise 20 & 23: event using Action<Account>
        public event Action<Account>? NegativeBalanceEvent;

        // Bank can subscribe via this Action as well; expose for convenience
        internal void OnNegativeBalance() => NegativeBalanceEvent?.Invoke(this);

        protected Account(string number, Person owner)
        {
            Number = number ?? throw new ArgumentNullException(nameof(number));
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Balance = 0.0;
        }

        protected Account(string number, Person owner, double balance)
        {
            Number = number ?? throw new ArgumentNullException(nameof(number));
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Balance = balance;
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be greater than zero.");
            Balance += amount;
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Withdraw amount must be greater than zero.");
            if (!CanWithdraw(amount))
                throw new InsufficientBalanceException($"Cannot withdraw {amount} from account {Number}.");
            double previous = Balance;
            Balance -= amount;
            // If derived class needs to trigger events on state change, it can call OnNegativeBalance()
            PostWithdrawHook(previous, Balance);
        }

        // Hook for derived classes to act after withdraw (e.g., trigger event when crossing to negative)
        protected virtual void PostWithdrawHook(double previousBalance, double newBalance) { }

        protected abstract bool CanWithdraw(double amount);

        // Exercise 10: interest calculation abstract method
        protected abstract double CalculInterets();

        public void ApplyInterest()
        {
            Balance += CalculInterets();
        }
    }
}
