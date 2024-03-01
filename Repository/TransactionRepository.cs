using PayCorona.Data;
using PayCorona.Interface;
using PayCorona.Models;

namespace PayCorona.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _dataContext;

        public TransactionRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void CreateTransaction(Transaction transaction)
        {
            _dataContext.Transactions.Add(transaction);
            _dataContext.SaveChanges();
        }

        public void DeleteTransaction(Transaction transaction)
        {
            _dataContext.Transactions.Remove(transaction);
            _dataContext.SaveChanges();
        }
        public List<Transaction> GetTransactionsByClientID(int clientID) => _dataContext.Transactions.Where(transaction => transaction.ClientID == clientID).ToList();

        public void UpdateTransaction(Transaction transaction)
        {
            _dataContext.Update(transaction);
            _dataContext.SaveChanges();
        }
    }
}
