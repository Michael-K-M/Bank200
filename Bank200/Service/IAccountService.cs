namespace Bank200.Service
{
    public interface IAccountService
    {
        public void openSavingsAccount(long accountId, long amountToDeposit);
        public void openCurrentAccount(long accountId);
        public void withdraw(long accountId, int amountToWithdraw);

        // throw AccountNotFoundException, WithdrawalAmountTooLargeException; 

        public void deposit(long accountId, int amountToDeposit);

        // throw AccountNotFoundException;
    }
}
