
using PayCorona.Data;
using PayCorona.Interface;
using PayCorona.Models;

namespace PayCorona.Repository;

public class SessionRepository : ISessionRepository
{
    private readonly DataContext _dataContext;

    public SessionRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void CreateSession(Session session)
    {
        _dataContext.Sessions.Add(session);
        _dataContext.SaveChanges();
    }

    public Session? GetSession(Guid token)
    {
        return _dataContext.Sessions.FirstOrDefault(x => x.Token == token);
    }

    public void UpdateSession(Session session)
    {
        _dataContext.Sessions.Update(session);
        _dataContext.SaveChanges();
    }
    public void DeleteSession(Session session)
    {
        _dataContext.Sessions.Remove(session);
        _dataContext.SaveChanges();
    }
    public void DeleteExpiredSessions() 
    {
        var session = GetExpiredSessions();
        _dataContext.Sessions.RemoveRange(session);
        _dataContext.SaveChanges();
    }

    public List<Session>? GetExpiredSessions()
    {
        var session = _dataContext.Sessions.Where(t => t.ExpireTime < DateTime.UtcNow).ToList();
        return session;
    }
}