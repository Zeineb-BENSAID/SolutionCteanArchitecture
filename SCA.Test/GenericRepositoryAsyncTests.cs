using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SCA.ApplicationCore.Interfaces;
using AM.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SCA.Infrastructure;

namespace Tests
{
    [TestFixture]
    public class GenericRepositoryAsyncTests
    {
        private Mock<SCAContext> _mockContext;
        private Mock<DbSet<TestEntity>> _mockDbSet;
        private GenericRepositoryAsync<TestEntity> _repository;

        [SetUp]
        public void SetUp()
        {
            // Cr�ation de mocks pour SCAContext et DbSet.
            _mockContext = new Mock<SCAContext>();
            _mockDbSet = new Mock<DbSet<TestEntity>>();

            // Configuration pour que DbSet soit accessible depuis le contexte.
            _mockContext.Setup(c => c.Set<TestEntity>()).Returns(_mockDbSet.Object);

            // Initialisation du repository avec le mock de contexte.
            _repository = new GenericRepositoryAsync<TestEntity>(_mockContext.Object);
        }

        [Test]
        public async Task AddAsync_Should_Add_Entity_To_DbSet()
        {
            // Arrange : Cr�ation d'une entit� de test.
            var entity = new TestEntity { Id = 1, Name = "Test" };

            // Act : Appel de la m�thode AddAsync.
            await _repository.AddAsync(entity);

            // Assert : V�rification que AddAsync a �t� appel� sur DbSet.
            _mockDbSet.Verify(db => db.AddAsync(entity, default), Times.Once);
        }

        [Test]
        public void Delete_Should_Remove_Entity_From_DbSet()
        {
            // Arrange : Cr�ation d'une entit� de test.
            var entity = new TestEntity { Id = 1, Name = "Test" };

            // Act : Appel de la m�thode Delete.
            _repository.Delete(entity);

            // Assert : V�rification que Remove a �t� appel� sur DbSet.
            _mockDbSet.Verify(db => db.Remove(entity), Times.Once);
        }



        // Classe d'entit� de test.
        public class TestEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}

/*
 Moq est une biblioth�que de simulation (mocking) populaire pour .NET, utilis�e dans les tests unitaires.
Elle permet de cr�er des objets simul�s (mocks) de d�pendances ou d'interfaces, afin de tester une classe ou une m�thode isol�ment, 
sans n�cessiter l'impl�mentation r�elle des d�pendances. 

Moq simplifie la v�rification des interactions entre votre code et ses d�pendances.

Pourquoi utiliser Moq ?
Isolation : Tester un composant ind�pendamment des autres.
Contr�le : Simuler des comportements sp�cifiques de d�pendances (comme les exceptions ou les retours de donn�es sp�cifiques).
Validation : V�rifier que certaines m�thodes ou actions ont �t� appel�es avec les bons arguments.
 */