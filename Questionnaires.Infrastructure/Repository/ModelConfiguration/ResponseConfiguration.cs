using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questionnaires.Domain.Models;

namespace Questionnaires.Infrastructure.Repository.ModelConfiguration
{
    public class ResponseConfiguration : IEntityTypeConfiguration<Response>
    {
        public void Configure(EntityTypeBuilder<Response> builder)
        {
            builder.HasKey(x => new { x.QuestionId, x.UserId });
            
            builder
                .HasOne(x => x.Answer)
                .WithMany();
        }
    }
}