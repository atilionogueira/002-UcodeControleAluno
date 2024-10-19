using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ucode.Core.Models;

namespace Ucode.Api.Data.Mappings
{
    public class ModuloMappings : IEntityTypeConfiguration<Modulo>
    {
        public void Configure(EntityTypeBuilder<Modulo> builder)
        {
            // Configuração da tabela
            builder.ToTable("Modulo");

            // Configuração da chave primária
            builder.HasKey(m => m.Id);

            // Configuração das propriedades
            builder.Property(m => m.SubTopico)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("varchar(200)");           

            builder.Property(m => m.Secao)
                .HasMaxLength(1000)
                .HasColumnType("varchar(1000)");

            builder.Property(m => m.UserId)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)");         
                 

        }
    }
}
