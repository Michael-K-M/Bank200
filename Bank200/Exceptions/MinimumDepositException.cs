namespace Bank200.Exceptions
{
    public class MinimumDepositException : Exception
    {

        public MinimumDepositException() { }

        public MinimumDepositException(long deposit)
            : base(String.Format("Amount deposited is to small: {0}", deposit))
        {

        }

    }
}
