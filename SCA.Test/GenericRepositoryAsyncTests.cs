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
            // Création de mocks pour SCAContext et DbSet.
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



        // Classe d'entité de test.
        public class TestEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}

/*
 Moq est une bibliothèque de simulation (mocking) populaire pour .NET, utilisée dans les tests unitaires.
Elle permet de créer des objets simulés (mocks) de dépendances ou d'interfaces, afin de tester une classe ou une méthode isolément, 
sans nécessiter l'implémentation réelle des dépendances. 

Moq simplifie la vérification des interactions entre votre code et ses dépendances.

Pourquoi utiliser Moq ?
Isolation : Tester un composant indépendamment des autres.
Contrôle : Simuler des comportements spécifiques de dépendances (comme les exceptions ou les retours de données spécifiques).
Validation : Vérifier que certaines méthodes ou actions ont été appelées avec les bons arguments.
 */