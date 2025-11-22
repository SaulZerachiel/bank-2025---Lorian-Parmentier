using System;

namespace Bank2025
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException() : base("Insufficient balance for this operation.") {}
        public InsufficientBalanceException(string message) : base(message) {}
    }
}
