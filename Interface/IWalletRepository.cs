using PayCorona.Models;

namespace PayCorona.Interface
{
    public interface IWalletRepository 
    {
        void CreateWallet(Wallet wallet);
        void DeleteWallet(Wallet wallet);
        Wallet GetWalletByClientID(int clientID);
        void UpdateWallet(Wallet wallet);
    }
}
