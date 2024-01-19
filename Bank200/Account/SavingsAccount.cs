namespace Bank200.Account
{
    public class SavingsAccount : IAccount
    {
        public long Id { get; set; }
        public string CustomerNumber { get; set; }
        public long Balance { get; set; }

        public SavingsAccount(long id, long balance, string customerNumber = "")
        {
            Id = id;
            CustomerNumber = customerNumber;
            Balance = balance;

        }
    }
}
