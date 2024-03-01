using System.ComponentModel.DataAnnotations;

namespace PayCorona.Models
{
    public class Session 
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Guid Token { get; set; }
        public DateTime ExpireTime { get; set; }

        public Session(int clientId, Guid token, DateTime expireTime)
        {
            ClientId = clientId;
            Token = token;
            ExpireTime = expireTime;
        }
    }
}
