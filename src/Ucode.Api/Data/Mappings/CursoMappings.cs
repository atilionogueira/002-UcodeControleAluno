using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ucode.Core.Models;
using Ucode.Core.Enums;

namespace Ucode.Api.Data.Mappings
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            // Configuração da tabela
            builder.ToTable("Curso");

            // Configuração da chave primária
            builder.HasKey(c => c.Id);

            // Configuração das propriedades
            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Resumo)
                .HasMaxLength(1000)
                .HasColumnType("varchar(1000)");

            builder.Property(c => c.Categoria)
                .IsRequired(true)
                .HasColumnType("smallint");

            builder.Property(c => c.UserId)
                .IsRequired()
                .HasMaxLength(160)
                .HasColumnType("varchar(160)");

            
            // Configuração do enum Categoria
            builder.Property(c => c.Categoria)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (ECursoCategoria)Enum.Parse(typeof(ECursoCategoria), v)
                )
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

            // Configuração dos relacionamentos
            builder.HasMany(c => c.Modulos)
                .WithOne(m => m.Curso)
                .HasForeignKey(m => m.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.ControleAlunos)
                .WithOne(ca => ca.Curso)
                .HasForeignKey(ca => ca.CursoId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }

}

