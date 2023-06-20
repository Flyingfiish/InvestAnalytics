using System.Linq.Expressions;
using InvestAnalytics.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestAnalytics.API.Db;

public static class Extentions
{
    /// <summary>
    /// Creates or updates entity by given predicate
    /// </summary>
    /// <param name="dbSet"></param>
    /// <param name="entity"></param>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    public static async Task Update<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> expression)
        where T : class, IHaveId
    {
        var existingEntity = await dbSet.FirstOrDefaultAsync(expression);
        if (existingEntity is not null)
        {
            entity.Id = existingEntity.Id;
        }

        dbSet.Update(entity);
    }
}