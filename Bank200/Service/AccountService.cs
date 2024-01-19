using Bank200.Account;
using Bank200.Database;

namespace Bank200.Service
{
    public class AccountService : IAccountService
    {
        private readonly ISystemDB _db;
        public AccountService(ISystemDB db)
        {
            _db = db;
        }
        public void deposit(long accountId, int amountToDeposit)
        {

            var account = _db.GetAccount(accountId);
            account.Balance += amountToDeposit;
        }

        public void openCurrentAccount(long accountId)
        {

            _db.OpenCurrentAccount(accountId);
        }

        public void openSavingsAccount(long accountId, long amountToDeposit)
        {
            if (amountToDeposit < 1000)
            {
                throw new NullReferenceException("Deposit amount may not be less than R1000");
            }

                _db.openSavingsAccount(accountId, amountToDeposit);
            
        }

        public void withdraw(long accountId, int amountToWithdraw)
        {

            var account = _db.GetAccount(accountId);
            account.Balance -= amountToWithdraw;

            if (account is ICurrentAccount currentAccount)
            {
                var test = currentAccount.Overdraft;
            }
        }

        // Testing purpose only
        public void Run()
        {
            Console.WriteLine("Hello, from the AccountService class!");
        }
    }
}
