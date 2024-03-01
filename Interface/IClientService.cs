using PayCorona.Models;

namespace PayCorona.Interface
{
    public interface IClientService
    {
        public Session? ClientAuth(string login, string password);
        public bool ClientRegister(string login, string password, string name);
    }
}
