

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

public class UnitOfWork : IUnitOfWork
{
    private MyDbContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(MyDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
    {
        _transaction = await _context.Database.BeginTransactionAsync(isolationLevel);
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
        await _transaction.CommitAsync();
    }

    public async Task RollBackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}