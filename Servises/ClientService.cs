using PayCorona.Interface;
using PayCorona.Models;

namespace PayCorona.Servises
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletService _walletService;

        public ClientService(IClientRepository clientRepository, ISessionRepository sessionRepository, IWalletRepository walletRepository, IWalletService walletService)
        {
            _clientRepository = clientRepository;
            _sessionRepository = sessionRepository;
            _walletRepository  = walletRepository;
            _walletService = walletService;
        }

        public bool ClientRegister(string login, string password, string name)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(login))
            {
                return false;
            }
            
            //var hashpassword = HashPassword(password);
            Client client = new Client { Login = login, Password = password, Name = name };
            
            if (_clientRepository.IsClientExist(login))
            {
                return false;
            }
            _clientRepository.CreateClient(client);
            _walletService.Create(client.Id);
            return true;
        }

        public Session? ClientAuth(string login, string password)
        {
            if (!_clientRepository.IsClientExist(login, password)) 
            {
                return null;
            }

            var client = _clientRepository.GetClientByLogin(login);
            var session = new Session(client!.Id, Guid.NewGuid(), DateTime.UtcNow.AddMinutes(5));
            _sessionRepository.CreateSession(session);

            return session;
        }
    }
}
