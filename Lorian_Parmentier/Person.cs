using System;

namespace Bank2025
{
    public class Person
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }

        public Person(string first, string last, DateTime birth)
        {
            FirstName = first ?? throw new ArgumentNullException(nameof(first));
            LastName = last ?? throw new ArgumentNullException(nameof(last));
            BirthDate = birth;
        }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
