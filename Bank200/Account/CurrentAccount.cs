namespace Bank200.Account
{
    public class CurrentAccount : ICurrentAccount
    {

        public long Overdraft { get; set; }
        public long Id { get; set; }
        public string CustomerNumber { get; set; }
        public long Balance { get; set; }

        public CurrentAccount(long id, string customerNumber = "", long balance = 0, long overdraft = 0)
        {
            Id = id;
            CustomerNumber = customerNumber;
            Balance = balance;
            Overdraft = overdraft;
        }

    }
}
