using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questionnaires.Domain.Models;

namespace Questionnaires.Infrastructure.Repository.ModelConfiguration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder
                .HasMany(x => x.AnswerOptions)
                .WithOne();

            builder.HasOne(x => x.Subject)
                .WithMany();
            
            builder
                .HasMany(x => x.Texts)
                .WithOne();
        }
    }
}