using Microsoft.EntityFrameworkCore.Storage;

namespace AlecEdu_api.Application.Common;

public interface IUnitOfWork: IDisposable
{
    /***
     * Đảm bảo những hành động liên quan đến database => transaction: Insert, Update, Delete
     * ===> Viết những hành động ảnh hưởng đến DB => insert, Update, Delete
     */
    IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    void SaveChanges();
    Task SaveChangesAsync();
    void LazyLoadingEnabledFalse();
    Task RollbackAsync();
    IDbContextTransaction BeginTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync();
}