using PayCorona.Data;
using PayCorona.Interface;
using PayCorona.Models;

namespace PayCorona.Repository
{
    //Паттерн репозиторий для БД
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _dataContext;

        public ClientRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Client> GetClients() => _dataContext.Clients.OrderBy(c => c.Id).ToList();

        public void CreateClient(Client client) 
        {
            _dataContext.Clients.Add(client);
            _dataContext.SaveChanges();
        } 

        public void DeleteClient(Client client) 
        {
            _dataContext.Clients.Remove(client);
            _dataContext.SaveChanges();
        } 

        public Client? GetClientByLogin(string login) => _dataContext.Clients.FirstOrDefault(client => client.Login == login);

        public bool IsClientExist(string login) => _dataContext.Clients.Any(client => client.Login == login);
        public bool IsClientExist(string login, string password) => _dataContext.Clients.Any(client => client.Login == login && client.Password == password);
        public void UpdateClient(Client client) 
        {
            _dataContext.Update(client);
            _dataContext.SaveChanges();
        }

        public Client? GetClientById(int id) => _dataContext.Clients.FirstOrDefault(client => client.Id == id);
    }
}
