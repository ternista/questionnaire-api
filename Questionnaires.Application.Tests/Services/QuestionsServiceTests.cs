using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using NUnit.Framework;
using Questionnaires.Application.Requests;
using Questionnaires.Application.Services;
using Questionnaires.Domain.Models;
using Questionnaires.Domain.Models.Query;
using Questionnaires.Domain.Repository;
using Question = Questionnaires.Domain.Models.Question;

namespace Questionnaires.Application.Tests.Services
{
    public class QuestionsServiceTests
    {
        private Mock<IQuestionsRepository> _questionsRepositoryMock;
        
        private QuestionsService _service;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization
            {
                ConfigureMembers = true
            });

            _questionsRepositoryMock = _fixture.Freeze<Mock<IQuestionsRepository>>();

            _service = _fixture.Create<QuestionsService>();
        }

        [Test]
        public async Task GetQuestions_WithNoRelevantResults_ReturnsEmptyResponse()
        {
            // Arrange
            _questionsRepositoryMock
                .Setup(x => x.GetQuestions(It.IsAny<GetQuestionsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<Question>(0, 0, 5, new Question[0]));
            
            // Act
            var results = await _service.GetQuestions(new GetQuestionsRequest(), CancellationToken.None);
            
            // Assert
            Assert.IsEmpty(results.Items);
            Assert.AreEqual(0, results.TotalCount);
        }
        
        [Test]
        public async Task GetQuestions_WithResults_ReturnsCorrectResponse()
        {
            // Arrange
            var questions = _fixture.CreateMany<Question>(10)
                .ToArray();

            foreach (var question in questions)
            {
                question.AnswerOptions = _fixture.CreateMany<Answer>(4).ToList();
            }
            
            _questionsRepositoryMock
                .Setup(x => x.GetQuestions(It.IsAny<GetQuestionsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new QueryResult<Question>(123, 10, 5, questions));
            
            // Act
            var results = await _service.GetQuestions(new GetQuestionsRequest(), CancellationToken.None);
            
            // Assert
            Assert.AreEqual(10, results.Items.Length);
            foreach (var question in results.Items)
            {
                Assert.AreEqual(4, question.AnswerOptions.Length);
            }
            Assert.AreEqual(123, results.TotalCount);
            Assert.AreEqual(10, results.Limit);
            Assert.AreEqual(5, results.Offset);
            Assert.AreEqual(5, results.Offset);
        }
    }
}