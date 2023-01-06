using System.Linq.Expressions;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Enums;

namespace AlecEdu_api.Application.Common;

public interface IBaseRepository<T> where T : class
{
    /***
        * Lớp cơ bản của Repository gồm các phương thức hay dùng đến.
        * Sử dụng GENERIC REPOSITORY: cho phép chúng ta định nghĩa một kiểu dữ liệu hoặc lớp mà không cần quan tâm đến kiểu dữ liệu chính xác của nó là gì => cho phép chúng ta định nghĩa một data structure dùng chung
    */
    
    IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
        params Expression<Func<T, object>>[] includeProperties);
    
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);
    
    Task<IEnumerable<T>> GetAllAsync(bool trackChanges = false);
    Task<ListResponse<T>> GetAllFilterAsync(
        List<Expression<Func<T, bool>>> fields,
        List<string>? memberNames,
        string? keySort,
        List<string> includes,
        ESort sortDir = ESort.ASC,
        int pageSize = 10,
        int page = 1,
        bool all = false,
        bool trackChanges = false
    );

    Task<T?> GetByIdAsync(int id, bool trackChanges = false);
    Task<bool> ExitsByIdAsync(int id);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
    void RemoveRange(IEnumerable<T> entities);
}
