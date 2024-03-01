using PayCorona.Data;
using PayCorona.Interface;
using PayCorona.Models;

namespace PayCorona.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly DataContext _dataContext;
        public WalletRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void CreateWallet(Wallet wallet)
        {
            _dataContext.Wallets.Add(wallet);
            _dataContext.SaveChanges();
        }

        public void DeleteWallet(Wallet wallet)
        {
            _dataContext.Wallets.Remove(wallet);
            _dataContext.SaveChanges();
        }
        public void UpdateWallet(Wallet wallet)
        {
            _dataContext.Update(wallet);
            _dataContext.SaveChanges();
        }
        public Wallet GetWalletByClientID(int clientId) => _dataContext.Wallets.FirstOrDefault(wallet => wallet.ClientID == clientId);
    }
}
