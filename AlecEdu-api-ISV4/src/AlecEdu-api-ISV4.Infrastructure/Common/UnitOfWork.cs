using AlecEdu_api.Application.Common;
using AlecEdu_api.Application.Contracts.Persistence;
using AlecEdu_api.Domain.Entities;
using AlecEdu_api.Infrastructure.Persistence;
using AlecEdu_api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AlecEdu_api.Infrastructure.Common;

public class UnitOfWork: IUnitOfWork
{
    private readonly AlecEduContext _context;
    
    private readonly Dictionary<Type, object> _repositories;
    public UnitOfWork(AlecEduContext context)
    {
        _context = context;
        _repositories ??= new Dictionary<Type, object>();
        _repositories[typeof(User)] = new UserRepository(_context);
        _repositories[typeof(Token)] = new TokenRepository(_context);
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }

    public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new BaseRepository<TEntity>(_context);
        }

        return (BaseRepository<TEntity>)_repositories[type];
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void LazyLoadingEnabledFalse()
    {
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        _context.ChangeTracker.LazyLoadingEnabled = false;
    }

    public async Task RollbackAsync()
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        await transaction.RollbackAsync();
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }
}
