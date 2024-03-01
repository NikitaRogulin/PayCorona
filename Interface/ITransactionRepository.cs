using PayCorona.Models;

namespace PayCorona.Interface
{
    public interface ITransactionRepository
    {
        void CreateTransaction(Transaction transaction);
        void DeleteTransaction(Transaction transaction);
        List<Transaction> GetTransactionsByClientID(int clientID);
        void UpdateTransaction(Transaction transaction);
    }
}
