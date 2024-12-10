using SCA.ApplicationCore.Interfaces;
using AM.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SCA.Infrastructure;

public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
{
    private readonly SCAContext _context;
    private readonly DbSet<T> _dbset;

    public GenericRepositoryAsync(SCAContext context)
    {
        _context = context;
        _dbset = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        // Utilisation de AddAsync pour une insertion asynchrone.
        await _dbset.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        // Suppression reste synchrone car DbSet.Remove ne dispose pas de méthode asynchrone.
        _dbset.Remove(entity);
    }

    public async Task DeleteAsync(Expression<Func<T, bool>> where)
    {
        // Suppression de plusieurs entités basée sur une condition avec Where exécuté de manière asynchrone.
        var entities = await _dbset.Where(where).ToListAsync();
        _dbset.RemoveRange(entities);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> where)
    {
        // Utilisation de FirstOrDefaultAsync pour une recherche asynchrone.
        return await _dbset.Where(where).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        // Utilisation de ToListAsync pour charger toutes les entités de manière asynchrone.
        return await _dbset.ToListAsync();
    }

    public async Task<T> GetByIdAsync(params object[] keyValues)
    {
        // Utilisation de FindAsync pour une recherche par clé primaire asynchrone.
        return await _dbset.FindAsync(keyValues);
    }

    public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where)
    {
        // Utilisation de ToListAsync pour exécuter la requête asynchrone.
        return await _dbset.Where(where).ToListAsync();
    }

    public void Update(T entity)
    {
        // Mise à jour reste synchrone car DbSet.Update ne dispose pas de méthode asynchrone.
        _dbset.Update(entity);
    }
}
