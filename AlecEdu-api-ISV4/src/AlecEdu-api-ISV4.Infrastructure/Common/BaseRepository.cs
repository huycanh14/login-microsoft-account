using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using AlecEdu_api.Application.Common;
using AlecEdu_api.Domain.Common;
using AlecEdu_api.Domain.Enums;
using AlecEdu_api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AlecEdu_api.Infrastructure.Common;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly AlecEduContext _context;
    private readonly DbSet<T> _dbSet; // Table trong db;
    private static IEnumerable<PropertyInfo> Props => typeof(T).GetProperties();

    public BaseRepository(AlecEduContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    private T TrimData(T entity)
    {
        var stringProperties = entity.GetType().GetProperties()
            .Where(p => p.PropertyType == typeof(string) && p.CanWrite);

        foreach (var stringProperty in stringProperties)
        {
            string currentValue = (string)stringProperty.GetValue(entity, null);
            if (currentValue != null)
                stringProperty.SetValue(entity, currentValue.Trim(), null);
        }

        return entity;
    }

    public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
    {
        var items = !trackChanges ? _dbSet.AsNoTracking() : _dbSet;
        items = includeProperties.Aggregate(items, (current, includeProperty)
            => current.Include(includeProperty));
        return items;
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
    {
        return trackChanges == false
            ? _dbSet.AsNoTracking().Where(expression)
            : _dbSet.Where(expression);
    }

    public IQueryable<T> FindByCondition(
        Expression<Func<T, bool>> expression,
        bool trackChanges = false,
        params Expression<Func<T, object>>[] includeProperties)
    {
        var items = FindByCondition(expression, trackChanges);
        items = includeProperties.Aggregate(items, (current, includeProperty)
            => current.Include(includeProperty));
        return items;
    }

    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
    {
        // var items = _dbSet.AsNoTracking();
        // items = includeProperties.Aggregate(items, (current, includeProperty)
        //     => current.Include(includeProperty));
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            await _context.Entry(entity).Reference(i => includeProperties).LoadAsync();
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges = false)
    {
        return !trackChanges ? await _dbSet.AsNoTracking().ToListAsync() : await _dbSet.ToListAsync();
    }

    public async Task<ListResponse<T>> GetAllFilterAsync(List<Expression<Func<T, bool>>> fields,
        List<string>? memberNames,
        string? keySort,
        List<string> includes,
        ESort sortDir = ESort.ASC,
        int pageSize = 10,
        int page = 1,
        bool all = false,
        bool trackChanges = false)
    {
        IQueryable<T> searchResults = _dbSet;
        page = page < 1 ? 1 : page;

        // Add Query
        var parameters = new ConcurrentDictionary<string, ParameterExpression>();
        var memberAssignments = new ConcurrentDictionary<string, MemberAssignment>();
        var selectors = new ConcurrentDictionary<string, Expression<Func<T, T>>>();

        // include
        foreach (var include in includes)
        {
            var check = Props.FirstOrDefault(x => x.ToString()?.ToUpper() == include.ToUpper());
            if (check != null)
            {
                searchResults = searchResults.Include(include);
            }
        }


        //Add filters
        searchResults = fields.Aggregate(searchResults, (current, expression) => current.Where(expression));
        if (memberNames == null || !memberNames.Any())
        {
            searchResults = searchResults.Select(x => x);
        }
        else
        {
            var parameterName = typeof(T).FullName;

            var requestName = $"{parameterName}:{string.Join(",", memberNames.OrderBy(x => x))}";
            if (!selectors.TryGetValue(requestName, out var selector))
            {
                if (!parameters.TryGetValue(parameterName!, out var parameter))
                {
                    parameter = Expression.Parameter(typeof(T), typeof(T).Name.ToLowerInvariant());

                    if (parameterName != null) _ = parameters.TryAdd(parameterName, parameter);
                }

                var bindings = memberNames
                    .Select(name =>
                    {
                        var memberName = $"{parameterName}:{name}";
                        if (!memberAssignments.TryGetValue(memberName, out var binding))
                        {
                            var member = Expression.PropertyOrField(parameter, name);
                            binding = Expression.Bind(member.Member, member);

                            _ = memberAssignments.TryAdd(memberName, binding);
                        }

                        return binding;
                    });

                var body = Expression.MemberInit(Expression.New(typeof(T)), bindings);
                selector = Expression.Lambda<Func<T, T>>(body, parameter);

                selectors.TryAdd(requestName, selector);
            }

            //get sort field
            searchResults = searchResults.Select(selector);
        }

        if (keySort != null)
        {
            searchResults = sortDir == ESort.ASC
                ? searchResults.OrderBy(x => EF.Property<object>(x, keySort))
                : searchResults.OrderByDescending(x => EF.Property<object>(x, keySort));
        }


        //// Get the search results 
        var count = await searchResults.CountAsync();
        searchResults = all == false
            ?  searchResults.Skip(page * pageSize - pageSize).Take(pageSize)
            :  searchResults;
        if (!trackChanges)
        {
            searchResults = searchResults.AsNoTracking();
        }
        var data = await searchResults.ToListAsync();

        return new ListResponse<T>(data: data, listHeader: null, count: count);
        
    }

    public async Task<T?> GetByIdAsync(int id, bool trackChanges = false)
    {
        var data = await _dbSet.FindAsync(id);
        if (data != null && !trackChanges) _context.Entry(data).State = EntityState.Detached;
        return data;
    }

    public async Task<bool> ExitsByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity != null;
    }

    public void Add(T entity)
    {
        entity = TrimData(entity);
        _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        var listEntity = entities.ToList();
        for (var i = 0; i < listEntity.Count(); i++)
        {
            var entity = listEntity[i];
            listEntity[i] = TrimData(entity);
        }

        _dbSet.AddRange(listEntity);
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        entity = TrimData(entity);
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }
}
