using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questionnaires.Domain.Models;

namespace Questionnaires.Infrastructure.Repository.ModelConfiguration
{
    public class DepartmentResponseSummarySummaryConfiguration : IEntityTypeConfiguration<DepartmentQuestionResponseSummary>
    {
        public void Configure(EntityTypeBuilder<DepartmentQuestionResponseSummary> builder)
        {
            builder.HasKey(x => new { x.QuestionId, x.DepartmentId });
        }
    }
}