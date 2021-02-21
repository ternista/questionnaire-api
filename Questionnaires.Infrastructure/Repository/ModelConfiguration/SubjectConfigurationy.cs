using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questionnaires.Domain.Models;

namespace Questionnaires.Infrastructure.Repository.ModelConfiguration
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {   
            builder
                .HasMany(x => x.Texts)
                .WithOne();
        }
    }
}