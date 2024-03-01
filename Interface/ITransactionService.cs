using PayCorona.Dto;
using PayCorona.Models;

namespace PayCorona.Interface
{
    public interface ITransactionService
    {
        public List<Transaction> GetTransactions(int clientID);
    }
}
