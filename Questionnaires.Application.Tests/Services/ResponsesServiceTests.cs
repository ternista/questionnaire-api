using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Questionnaires.Application.Requests;
using Questionnaires.Application.Services;
using Questionnaires.Domain.DomainEvents;
using Questionnaires.Domain.Models;
using Questionnaires.Domain.Repository;

namespace Questionnaires.Application.Tests.Services
{
    public class ResponsesServiceTests
    {
        private Mock<IResponsesRepository> _responsesRepositoryMock;
        private Mock<IEventDispatcher> _eventDispatcherMock;
        
        private ResponsesService _service;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization
            {
                ConfigureMembers = true
            });

            _responsesRepositoryMock = _fixture.Freeze<Mock<IResponsesRepository>>();
            _eventDispatcherMock = _fixture.Freeze<Mock<IEventDispatcher>>();

            _service = _fixture.Create<ResponsesService>();
        }

        [Test]
        public void SubmitResponses_WithNoResponses_ThrowsException()
        {
            // Arrange
            var command = new SubmitUsereResponsesCommand
            {
                Responses = new UserResponse[0]
            };
            
            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _service.SubmitResponses(command));
        }
        
        [Test]
        public async Task SubmitResponses_WithNewAndUpdatedResponses_PublishesEvents()
        {
            // Arrange
            var command = new SubmitUsereResponsesCommand
            {
                Responses = _fixture.CreateMany<UserResponse>(5).ToArray()
            };

            _responsesRepositoryMock
                .Setup(x => x.GetUserResponsesForQuestions(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(_fixture.CreateMany<Response>(3).ToArray());
            
            // Act
            await _service.SubmitResponses(command, CancellationToken.None);
            
            // Arrange
            _eventDispatcherMock.Verify(x => x.PublishEvents(It.Is<IEnumerable<ResponseSubmittedEvent>>(events => events.Count() == 5)), Times.Once);
        }
        
        [Test]
        public async Task SubmitResponses_WithNewResponses_SavesResponses()
        {
            // Arrange
            var command = new SubmitUsereResponsesCommand
            {
                Responses = _fixture.CreateMany<UserResponse>(5).ToArray()
            };

            _responsesRepositoryMock
                .Setup(x => x.GetUserResponsesForQuestions(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(new Response[0]);
            
            // Act
            await _service.SubmitResponses(command, CancellationToken.None);
            
            // Arrange
            _responsesRepositoryMock.Verify(x => x.AddResponses(It.Is<Response[]>(r => r.Length == 5)));
            _responsesRepositoryMock.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()));
        }
        
        [Test]
        public async Task SubmitResponses_WithUpdatedResponses_SavesResponses()
        {
            // Arrange
            var command = new SubmitUsereResponsesCommand
            {
                Responses = _fixture.CreateMany<UserResponse>(5).ToArray()
            };

            _responsesRepositoryMock
                .Setup(x => x.GetUserResponsesForQuestions(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(_fixture.CreateMany<Response>(5).ToArray());
            
            // Act
            await _service.SubmitResponses(command, CancellationToken.None);
            
            // Arrange
            _responsesRepositoryMock.Verify(x => x.DeleteResponses(It.Is<Response[]>(r => r.Length == 5)));
            _responsesRepositoryMock.Verify(x => x.AddResponses(It.Is<Response[]>(r => r.Length == 5)));
            _responsesRepositoryMock.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()));
        }
        
        [Test]
        public async Task SubmitResponses_WithNewAndUpdatedResponses_SavesResponses()
        {
            // Arrange
            var command = new SubmitUsereResponsesCommand
            {
                Responses = _fixture.CreateMany<UserResponse>(5).ToArray()
            };

            _responsesRepositoryMock
                .Setup(x => x.GetUserResponsesForQuestions(It.IsAny<int>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(_fixture.CreateMany<Response>(3).ToArray());
            
            // Act
            await _service.SubmitResponses(command, CancellationToken.None);
            
            // Arrange
            _responsesRepositoryMock.Verify(x => x.DeleteResponses(It.Is<Response[]>(r => r.Length == 3)));
            _responsesRepositoryMock.Verify(x => x.AddResponses(It.Is<Response[]>(r => r.Length == 5)));
            _responsesRepositoryMock.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()));
        }
    }
}