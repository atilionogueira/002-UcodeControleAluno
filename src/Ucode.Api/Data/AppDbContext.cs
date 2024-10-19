using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Ucode.Api.Models;
using Ucode.Core.Models;

namespace Ucode.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
         : IdentityDbContext<User,
        IdentityRole<long>,
        long,
        IdentityUserClaim<long>,
        IdentityUserRole<long>,
        IdentityUserLogin<long>,
        IdentityRoleClaim<long>,
        IdentityUserToken<long>>(options)
    {
        public DbSet<Aluno> Alunos { get; set; } = null!;
        public DbSet<ControleAluno> ControleAlunos { get; set; } = null!;
        public DbSet<Curso> Cursos { get; set; } = null!;
        public DbSet<Modulo> Modulos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Relacionamento 1:N entre Curso e Modulo
            modelBuilder.Entity<Modulo>()
            .HasOne(m => m.Curso)
            .WithMany(c => c.Modulos)
            .HasForeignKey(m => m.CursoId)
            .OnDelete(DeleteBehavior.Restrict); // Evita a exclusão em cascata.

        }
    }
}
