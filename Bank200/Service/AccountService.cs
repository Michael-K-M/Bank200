using Bank200.Account;
using Bank200.Database;
using Bank200.Exceptions;

namespace Bank200.Service
{
    public class AccountService : IAccountService
    {
        private readonly ISystemDB _db;
        public AccountService(ISystemDB db)
        {
            _db = db;
        }
        public void deposit(long accountId, long amountToDeposit)
        {

            var account = _db.GetAccount(accountId);
            if(account == null)
            {
                throw new AccountNotFoundException(accountId);
            }
            account.Balance += amountToDeposit;
            _db.UpdateAccount(account);
        }

        public void openCurrentAccount(long accountId)
        {

            _db.OpenCurrentAccount(accountId);
        }

        public void openSavingsAccount(long accountId, long amountToDeposit)
        {
            if (amountToDeposit < 1000)
            {
                throw new MinimumDepositException(amountToDeposit);
            }

            _db.openSavingsAccount(accountId, amountToDeposit);

        }

        public void withdraw(long accountId, long amountToWithdraw)
        {

            var account = _db.GetAccount(accountId);

            // Savings account requires atleast R1000 left inside
            long adjustment = -1000;

            if (account is ICurrentAccount currentAccount) 
            {
                adjustment = currentAccount.Overdraft;
            }

            if (account.Balance + adjustment >= amountToWithdraw)
            {
                //withdraw
                account.Balance -= amountToWithdraw;
                _db.UpdateAccount(account);
            }
            else 
            {
                // throw exception
                throw new WithdrawalAmountTooLargeException(account.CustomerNumber);
            } 
        }
    }
}
