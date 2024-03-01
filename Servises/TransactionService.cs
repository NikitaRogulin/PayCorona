using PayCorona.Dto;
using PayCorona.Interface;
using PayCorona.Models;
using PayCorona.Repository;

namespace PayCorona.Servises
{
    public class TransactionService : ITransactionService
    {
        public ITransactionRepository _transactionRepository;
        public IClientRepository _clientRepository;

        public TransactionService(ITransactionRepository transactionRepository, IClientRepository clientRepository)
        {
            _transactionRepository = transactionRepository;
            _clientRepository = clientRepository;
        }

        public List<Transaction> GetTransactions(int clientID)
        {
            var client = _clientRepository.GetClientById(clientID);
            if(client == null)
            {
                throw new Exception();
            }
            return _transactionRepository.GetTransactionsByClientID(client.Id);
        }
    }
}
