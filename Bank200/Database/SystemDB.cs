using Bank200.Account;

namespace Bank200.Database
{
    public class SystemDB : ISystemDB
    {
        // create list
        private List<IAccount> _accountList = new List<IAccount>();
        public SystemDB()
        {
            // create data for the database
            var saving1 = new SavingsAccount(1, 2000, "1");
            var saving2 = new SavingsAccount(2, 5000, "2");

            var current1 = new CurrentAccount(3, "3", 1000, 10000);
            var current2 = new CurrentAccount(4, "4", 5000, 20000);

            // add the data to the list
            _accountList.Add(saving1);
            _accountList.Add(saving2);

            _accountList.Add(current1);
            _accountList.Add(current2);

        }

        public IAccount GetAccount(long id)
        {
            return _accountList.First(x => x.Id == id);
        }

        public void OpenCurrentAccount(long id)
        {
            var newCurrentAccount = new CurrentAccount(id);
            _accountList.Add(newCurrentAccount);
        }

        public void openSavingsAccount(long id, long balance)
        {
            var newSavingsAccount = new SavingsAccount(id, balance);
            _accountList.Add(newSavingsAccount);
        }

    }
}
