using PayCorona.Models;

namespace PayCorona.Interface
{
    public interface IClientRepository
    {
        List<Client> GetClients();
        void CreateClient(Client client);
        void DeleteClient(Client client);

        Client? GetClientByLogin(string login);
        Client? GetClientById(int id);
        bool IsClientExist(string login);
        bool IsClientExist(string login, string password);
        void UpdateClient(Client client);
    }
}
