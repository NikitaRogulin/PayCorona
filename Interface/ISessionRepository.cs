using PayCorona.Models;

namespace PayCorona.Interface;

public interface ISessionRepository
{
    void CreateSession(Session session);
    Session? GetSession(Guid token);

    void DeleteSession(Session session);
    void UpdateSession(Session session);

    void DeleteExpiredSessions();

    List<Session> GetExpiredSessions();
}