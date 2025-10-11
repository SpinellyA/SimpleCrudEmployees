
using System.Data;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync(IsolationLevel isolationLevel);
    Task CommitAsync();
    Task RollBackAsync();
}