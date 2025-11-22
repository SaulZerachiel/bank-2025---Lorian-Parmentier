namespace Bank2025
{
    public interface IAccount
    {
        double Balance { get; }
        void Deposit(double amount);
        void Withdraw(double amount);
    }

    public interface IBankAccount : IAccount
    {
        Person Owner { get; }
        string Number { get; }
        void ApplyInterest();
    }
}
