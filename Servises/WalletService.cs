using PayCorona.Interface;
using PayCorona.Models;
using PayCorona.Repository;

namespace PayCorona.Servises
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;

        public WalletService(IWalletRepository walletRepository, ITransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }
        public Wallet Create(int clientID) 
        {
            var wallet = new Wallet { Balance = 0, ClientID = clientID};
            _walletRepository.CreateWallet(wallet);
            return wallet;
        }
        
        public void Deposit(decimal sum, int clientID)
        {
            var wallet = _walletRepository.GetWalletByClientID(clientID);
            if(wallet != null) 
            {
                wallet.Balance += sum;
                _walletRepository.UpdateWallet(wallet);
            }
            else 
            {
                throw new Exception();
            }
        }
        public decimal GetBalance(int clientID) 
        {
            var wallet = _walletRepository.GetWalletByClientID(clientID);
            if(wallet == null) 
            {
                throw new Exception();
            }
            return wallet.Balance;
        }

        // методы репозитория Транзакции
        public void Send(decimal sum, int clientID, int recipientId)
        {
            var wallet = _walletRepository.GetWalletByClientID(clientID);
            var reseverWallet = _walletRepository.GetWalletByClientID(recipientId);
            if (wallet != null && reseverWallet != null)
            {
                if (wallet.Balance >= sum)
                {
                    var transaction = new Transaction()
                    {
                        ClientID = clientID,
                        RecipientId = recipientId,
                        Sum = sum,
                        DateTime = DateTime.UtcNow
                    };
                    wallet.Balance -= sum;
                    reseverWallet.Balance += sum;

                    _transactionRepository.CreateTransaction(transaction);
                    _walletRepository.UpdateWallet(wallet);
                    _walletRepository.UpdateWallet(reseverWallet);
                }
            }
        }
    }
}
