using System.Threading;
using System.Threading.Tasks;
using Questionnaires.Domain.Models;

namespace Questionnaires.Domain.Repository
{
    public interface IResponseSummaryRepository
    {
        Task<DepartmentQuestionResponseSummary[]> GetAllForQuestionnaire(int questionnaireId,
            CancellationToken cancellationToken = default);

        void Add(DepartmentQuestionResponseSummary summary);
        
        void Update(DepartmentQuestionResponseSummary summary);

        Task SaveChanges(CancellationToken cancellationToken = default);
        Task<bool> Exists(int questionId, int departmentId, CancellationToken cancellationToken = default);
    }
}