using Microsoft.EntityFrameworkCore;
using Questionnaires.Domain.Models;
using Questionnaires.Infrastructure.Repository.ModelConfiguration;

namespace Questionnaires.Infrastructure.Repository
{
    public class QuestionnaireContext : DbContext
    {
        public QuestionnaireContext(DbContextOptions<QuestionnaireContext> contextOptions) : base(contextOptions)
        {
        }

        public DbSet<Questionnaire> Questionnairs { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Response> Responses { get; set; }

        public DbSet<DepartmentQuestionResponseSummary> ResponseSummaries { get; set; }
        
        public DbSet<Subject> Subjects { get; set; }
        
        public DbSet<Answer> Answers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuestionnaireConfiguration).Assembly);
        }
    }
}