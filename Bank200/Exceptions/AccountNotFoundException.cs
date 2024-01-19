namespace Bank200.Exceptions
{
    public class AccountNotFoundException : Exception
    {

        public AccountNotFoundException() { }

        public AccountNotFoundException(string CustomerNumber)
            : base(String.Format("Invalid Customer Number: {0}", CustomerNumber))
        {

        }

    }
}
