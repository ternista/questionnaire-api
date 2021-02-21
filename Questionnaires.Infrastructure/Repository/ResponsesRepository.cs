using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Questionnaires.Domain.Models;
using Questionnaires.Domain.Models.Query;
using Questionnaires.Domain.Repository;

namespace Questionnaires.Infrastructure.Repository
{
    public class ResponsesRepository : IResponsesRepository
    {
        private readonly QuestionnaireContext _context;

        public ResponsesRepository(QuestionnaireContext context)
        {
            _context = context;
        }

        public void AddResponses(IEnumerable<Response> responses)
        {
            _context.Responses.AddRange(responses);
        }

        public void DeleteResponses(IEnumerable<Response> responses)
        {
             _context.Responses.RemoveRange(responses);
        }

        public Task<Response[]> GetUserResponsesForQuestions(int userId, IEnumerable<int> questionIds)
        {
            return _context.Responses
                .Where(x => x.UserId == userId && questionIds.Contains(x.QuestionId))
                .ToArrayAsync();
        }

        public Task<AggregatedCounts> GetDepartmentAggregatedResults(int questionId, int departmentId,
            CancellationToken cancellationToken = default)
        {
            return _context.Responses
                .Where(x => x.QuestionId == questionId && x.DepartmentId == departmentId)
                .Select(x => new { Score = x.Answer.OrderNumber, DepartmentId = x.DepartmentId  })
                .GroupBy(x => new {x.DepartmentId })
                .Select(grouping => new AggregatedCounts
                {
                    Average = grouping.Average(r => r.Score),
                    Max = grouping.Max(r => r.Score),
                    Min = grouping.Min(r => r.Score),
                    TotalResponses = grouping.Count()
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
        
        public Task SaveChanges(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}