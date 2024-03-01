using PayCorona.Models;

namespace PayCorona.Interface
{
    public interface IWalletService 
    {
        public void Deposit(decimal sum,int walletID);
        public void Send(decimal sum, int reseverID, int senderID); 
        public decimal GetBalance(int clientID);

        public Wallet Create(int clientID);
    }
}
