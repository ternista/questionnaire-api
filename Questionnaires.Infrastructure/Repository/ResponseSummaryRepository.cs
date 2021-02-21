using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Questionnaires.Domain.Models;
using Questionnaires.Domain.Repository;

namespace Questionnaires.Infrastructure.Repository
{
    public class ResponseSummaryRepository : IResponseSummaryRepository
    {
        private readonly QuestionnaireContext _context;

        public ResponseSummaryRepository(QuestionnaireContext context)
        {
            _context = context;
        }

        public Task<DepartmentQuestionResponseSummary[]> GetAllForQuestionnaire(int questionnaireId,
            CancellationToken cancellationToken = default)
        {
            return _context
                .ResponseSummaries
                .Where(x => x.Question.QuestionnaireId == questionnaireId)
                .ToArrayAsync(cancellationToken);
        }

        public Task<bool> Exists(int questionId, int departmentId, CancellationToken cancellationToken = default)
        {
            return _context.ResponseSummaries
                .Where(x => x.QuestionId == questionId && x.DepartmentId == departmentId)
                .AnyAsync(cancellationToken);
        }
        
        public void Add(DepartmentQuestionResponseSummary summary)
        {
            _context.ResponseSummaries.Add(summary);
        }

        public void Update(DepartmentQuestionResponseSummary summary)
        {
            _context.ResponseSummaries.Update(summary);
        }

        public Task SaveChanges(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}