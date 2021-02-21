using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Questionnaires.Application.Events;
using Questionnaires.Common;
using Questionnaires.Domain.DomainEvents;
using Questionnaires.Domain.Models;
using Questionnaires.Domain.Repository;
using Question = Questionnaires.Domain.Models.Question;

namespace Questionnaires.Application.Tests.Events
{
    public class ResponseSubmittedEventHandlerTests
    {
        private Mock<IQuestionsRepository> _questionsRepositoryMock;
        private Mock<IResponseSummaryRepository> _responsesSummaryRepositoryMock;
        
        private ResponseSubmittedEventHandler _handler;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization
            {
                ConfigureMembers = true
            });

            _questionsRepositoryMock = _fixture.Freeze<Mock<IQuestionsRepository>>();
            _responsesSummaryRepositoryMock = _fixture.Freeze<Mock<IResponseSummaryRepository>>();

            _handler = _fixture.Create<ResponseSubmittedEventHandler>();
        }

        [Test]
        public async Task Handle_WithFreeTextQuestion_DoesntCreateSummary()
        {
            // Arrange
            var @event = _fixture.Create<ResponseSubmittedEvent>();

            _questionsRepositoryMock.Setup(x => x.GetById(@event.QuestionId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Question(1, 2, 0, AnswerCategoryType.FreeText));
            
            // Act
            await _handler.Handle(@event, CancellationToken.None);
            
            // Assert
            _responsesSummaryRepositoryMock.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()), Times.Never);
        }
        
        [Test]
        public async Task Handle_WithSingleChoiceQuestion_WhenSummaryDoesntExist_CreatesSummary()
        {
            // Arrange
            var @event = _fixture.Create<ResponseSubmittedEvent>();

            _questionsRepositoryMock.Setup(x => x.GetById(@event.QuestionId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Question(1, 2, 0, AnswerCategoryType.SingleChoiceAnswer));

            _responsesSummaryRepositoryMock.Setup(x =>
                    x.Exists(@event.QuestionId, @event.DepartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            
            // Act
            await _handler.Handle(@event, CancellationToken.None);
            
            // Assert
            _responsesSummaryRepositoryMock.Verify(x => x.Add(It.IsAny<DepartmentQuestionResponseSummary>()), Times.Once);
            _responsesSummaryRepositoryMock.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()), Times.Once);
        }
        
        [Test]
        public async Task Handle_WithSingleChoiceQuestion_WhenSummaryDoesntExist_UpdatesSummary()
        {
            // Arrange
            var @event = _fixture.Create<ResponseSubmittedEvent>();

            _questionsRepositoryMock.Setup(x => x.GetById(@event.QuestionId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Question(1, 2, 0, AnswerCategoryType.SingleChoiceAnswer));

            _responsesSummaryRepositoryMock.Setup(x =>
                    x.Exists(@event.QuestionId, @event.DepartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            
            // Act
            await _handler.Handle(@event, CancellationToken.None);
            
            // Assert
            _responsesSummaryRepositoryMock.Verify(x => x.Update(It.IsAny<DepartmentQuestionResponseSummary>()), Times.Once);
            _responsesSummaryRepositoryMock.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}