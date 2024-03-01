using PayCorona.Data;
using PayCorona.Models;
using System.Diagnostics.Metrics;

namespace PayCorona
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Clients.Any())
            {
                var clients = new List<Client>()
                {
                    new Client()
                    {
                            Name = "Nikita",
                            Login = "nikita@yandex.ru",
                            Password = "sddsaa"
                    },
                    new Client() 
                    {
                            Name = "Vasya",
                            Login = "vasya@yandex.ru",
                            Password = "ssss"
                    }
                };
                dataContext.Clients.AddRange(clients);
                dataContext.SaveChanges();
            }
        }
    }
}
