namespace Bank200.Exceptions
{
    public class AccountNotFoundException : Exception
    {

        public AccountNotFoundException(long CustomerNumber)
            : base(String.Format("Invalid Account Number: {0}", CustomerNumber))
        {

        }

    }
}
