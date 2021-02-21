using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Questionnaires.Domain.Models;
using Questionnaires.Domain.Models.Query;
using Questionnaires.Domain.Repository;

namespace Questionnaires.Infrastructure.Repository
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private readonly QuestionnaireContext _context;

        public QuestionsRepository(QuestionnaireContext context)
        {
            _context = context;
        }

        public async Task<QueryResult<Question>> GetQuestions(GetQuestionsQuery query,
            CancellationToken cancellationToken)
        {
            var queryable = _context
                .Questions
                .AsNoTracking()
                .Where(x => x.QuestionnaireId == query.QuestionnaireId);

            var totalCount = await queryable.CountAsync(cancellationToken);
            var questions = await queryable
                .Include(x => x.Texts
                    .Where(t => t.Locale == query.Locale))
                .Include(x => x.AnswerOptions
                    .OrderBy(i => i.OrderNumber))
                .ThenInclude(x => x.Texts
                    .Where(t => t.Locale == query.Locale))
                .OrderBy(x => x.Subject.OrderNumber)
                .ThenBy(x => x.OrderNumber)
                .Take(query.Limit)
                .Skip(query.Offset)
                .ToArrayAsync(cancellationToken);

            return new QueryResult<Question>(totalCount, query.Limit, query.Offset, questions);
        }

        public Task<Question> GetById(int questionId, CancellationToken cancellationToken)
        {
            return _context.Questions.FirstOrDefaultAsync(x => x.QuestionId == questionId, cancellationToken);
        }
    }
}