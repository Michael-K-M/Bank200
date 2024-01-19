namespace Bank200.Exceptions
{
    public class WithdrawalAmountTooLargeException : Exception
    {
        public WithdrawalAmountTooLargeException() { }

        public WithdrawalAmountTooLargeException(string CustomerNumber)
            : base(String.Format("Invalid withdraw amount: {0}", CustomerNumber))
        {

        }
    }
}
