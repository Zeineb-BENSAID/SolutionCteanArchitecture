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

/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AM.ApplicationCore.Interfaces;
using AM.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class GenericRepositoryAsyncTests
    {
        private Mock<AMContext> _mockContext;
        private Mock<DbSet<TestEntity>> _mockDbSet;
        private GenericRepositoryAsync<TestEntity> _repository;

        [SetUp]
        public void SetUp()
        {
            // Création de mocks pour AMContext et DbSet.
            _mockContext = new Mock<AMContext>();
            _mockDbSet = new Mock<DbSet<TestEntity>>();

            // Configuration pour que DbSet soit accessible depuis le contexte.
            _mockContext.Setup(c => c.Set<TestEntity>()).Returns(_mockDbSet.Object);

            // Initialisation du repository avec le mock de contexte.
            _repository = new GenericRepositoryAsync<TestEntity>(_mockContext.Object);
        }

        [Test]
        public async Task AddAsync_Should_Add_Entity_To_DbSet()
        {
            // Arrange : Création d'une entité de test.
            var entity = new TestEntity { Id = 1, Name = "Test" };

            // Act : Appel de la méthode AddAsync.
            await _repository.AddAsync(entity);

            // Assert : Vérification que AddAsync a été appelé sur DbSet.
            _mockDbSet.Verify(db => db.AddAsync(entity, default), Times.Once);
        }

        [Test]
        public void Delete_Should_Remove_Entity_From_DbSet()
        {
            // Arrange : Création d'une entité de test.
            var entity = new TestEntity { Id = 1, Name = "Test" };

            // Act : Appel de la méthode Delete.
            _repository.Delete(entity);

            // Assert : Vérification que Remove a été appelé sur DbSet.
            _mockDbSet.Verify(db => db.Remove(entity), Times.Once);
        }

        [Test]
        public async Task GetAsync_Should_Return_Entity_Based_On_Condition()
        {
            // Arrange : Configuration de DbSet avec des données simulées.
            var data = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Test1" },
                new TestEntity { Id = 2, Name = "Test2" }
            }.AsQueryable();

            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // Act : Appel de la méthode GetAsync.
            var result = await _repository.GetAsync(e => e.Id == 1);

            // Assert : Vérification que l'entité correcte est retournée.
            Assert.NotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public async Task GetAllAsync_Should_Return_All_Entities()
        {
            // Arrange : Configuration de DbSet avec des données simulées.
            var data = new List<TestEntity>
            {
                new TestEntity { Id = 1, Name = "Test1" },
                new TestEntity { Id = 2, Name = "Test2" }
            }.AsQueryable();

            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // Act : Appel de la méthode GetAllAsync.
            var result = await _repository.GetAllAsync();

            // Assert : Vérification que toutes les entités sont retournées.
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count());
        }
    }

    // Classe d'entité de test.
    public class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
*/


/*
 Moq est une bibliothèque de simulation (mocking) populaire pour .NET, utilisée dans les tests unitaires. Elle permet de créer des objets simulés (mocks) de dépendances ou d'interfaces, afin de tester une classe ou une méthode isolément, sans nécessiter l'implémentation réelle des dépendances. Moq simplifie la vérification des interactions entre votre code et ses dépendances.

Pourquoi utiliser Moq ?
Isolation : Tester un composant indépendamment des autres.
Contrôle : Simuler des comportements spécifiques de dépendances (comme les exceptions ou les retours de données spécifiques).
Validation : Vérifier que certaines méthodes ou actions ont été appelées avec les bons arguments.
 */