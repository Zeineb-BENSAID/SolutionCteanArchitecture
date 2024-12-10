using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SCA.ApplicationCore.Interfaces;
public interface IGenericRepositoryAsync<T> where T : class
{
    Task AddAsync(T entity); // Ajout asynchrone
    Task<T> GetByIdAsync(params object[] keyValues); // Recherche par clé primaire
    Task<IEnumerable<T>> GetAllAsync(); // Récupération de toutes les entités
    Task<T> GetAsync(Expression<Func<T, bool>> where); // Recherche d'une entité basée sur une condition(lambda)
    Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where); // Recherche de plusieurs entités selon une condition
    Task DeleteAsync(Expression<Func<T, bool>> where); // Suppression basée sur une condition (lambda expression)
    void Delete(T entity); // Suppression synchrone d'une entité T
    void Update(T entity); // Mise à jour synchrone d'une entité T
}
