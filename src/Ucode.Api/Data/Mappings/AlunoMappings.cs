using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ucode.Core.Models;

namespace Ucode.Api.Data.Mappings
{
    public class AlunoMappings : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Aluno");
            builder.HasKey(x => x.Id);

            // Configurações das propriedades
            builder.Property(a => a.Nome)
                .IsRequired(true)
                .HasMaxLength(200);

            builder.Property(a => a.Contato)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(a => a.Email)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder.Property(a => a.Instagram)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder.Property(a => a.Cidade)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder.Property(a => a.Estado)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            builder.Property(a => a.UserId)
                .IsRequired(true)
                .HasMaxLength(160)
                .HasColumnType("varchar(160)");         
        }
    }
}
