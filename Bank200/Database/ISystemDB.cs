using Bank200.Account;

namespace Bank200.Database
{
    public interface ISystemDB
    {

        public IAccount? GetAccount(long id);
        public void UpdateAccount(IAccount account);
        public void OpenCurrentAccount(long id);

        public void openSavingsAccount(long id, long balance);

    }
}
