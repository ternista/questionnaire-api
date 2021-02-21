using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questionnaires.Domain.Models;

namespace Questionnaires.Infrastructure.Repository.ModelConfiguration
{
    public class QuestionnaireConfiguration : IEntityTypeConfiguration<Questionnaire>
    {
        public void Configure(EntityTypeBuilder<Questionnaire> builder)
        {
            builder
                .HasMany(x => x.Questions)
                .WithOne();
        }
    }
}