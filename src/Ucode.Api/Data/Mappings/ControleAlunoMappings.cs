using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ucode.Core.Models;

namespace Ucode.Api.Data.Mappings
{
    public class ControleAlunoMappings : IEntityTypeConfiguration<ControleAluno>
    {
        public void Configure(EntityTypeBuilder<ControleAluno> builder)
        {
            builder.ToTable("ControleAluno");

            builder.HasKey(ca=> ca.Id);

            builder.Property(ca => ca.DataInicio)
                .IsRequired();

            builder.Property(ca => ca.DataFim)
                 .IsRequired();

            builder.Property(ca => ca.Resumo)
                .IsRequired(true)
                .HasMaxLength(500)
                .HasColumnType("nvarchar(500)");

            builder.Property(ca => ca.UserId)
                .IsRequired(true)
                .HasMaxLength(160)
                .HasColumnType("varchar(160)");

            builder.Property(ca => ca.Status)
                .IsRequired(true)
                .HasColumnType("SMALLINT");
        }
    }
}
