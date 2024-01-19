namespace Bank200.Account
{
    public interface IAccount
    {
        public long Id { get; set; }
        public string CustomerNumber { get; set; }
        public long Balance { get; set; }

    }
    public interface ICurrentAccount : IAccount // this will inherat from savings account as they share the same data
    {
        public long Overdraft { get; set; }
    }

}
