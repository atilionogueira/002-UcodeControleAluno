using Microsoft.EntityFrameworkCore;
using Ucode.Api.Data;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Curso;
using Ucode.Core.Responses;

namespace Ucode.Api.Handlers
{
    public class CursoHandler(AppDbContext context, ILogger<CursoHandler> logger) : ICursoHandler
    {
        private readonly ILogger<CursoHandler> _logger = logger;

        public async Task<PagedResponse<List<Curso>>> GetAllAsync(GetAllCursoRequest request)
        {
            try
            {
                var query = context
                    .Cursos
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId)
                    .OrderBy(x => x.Nome);

                var cursos = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Curso>>(
                    cursos,
                    count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Curso>>(null, 500, "Não foi possível consultar as categorias");
            }

        }

        public async Task<Response<Curso?>> GetByIdAsync(GetCursoByIdRequest request)
        {
            var curso = await context
               .Cursos
               .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            return curso is null
                ? new Response<Curso?>(null, 404, message: "Curso nao econtrado")
                : new Response<Curso?>(curso, message: "Curso encontrado");

        }
        public async Task<Response<Curso?>> CreateAsync(CreateCursoRequest request)
        {

            try
            {
                var curso = new Curso
                {
                    Nome = request.Nome,
                    Resumo = request.Resumo,
                    Categoria = request.Categoria,
                    UserId = request.UserId
                };

                await context.AddAsync(curso);
                await context.SaveChangesAsync();

                return new Response<Curso?>(curso, 201, "Curso criado com sucesso");
            }
            catch
            {
                return new Response<Curso?>(null, 500, "Não foi possível criar um curso");
            }

        }
        public async Task<Response<Curso?>> UpdateAsync(UpdateCursoRequest request)
        {
            try
            {
                var curso = await context
                .Cursos
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (curso is null)
                    return new Response<Curso?>(null, 404, "Curso nao encontrado");

                curso.Nome = request.Nome;
                curso.Resumo = request.Resumo;
                curso.Categoria = request.Categoria;

                context.Cursos.Update(curso);
                await context.SaveChangesAsync();

                return new Response<Curso?>(curso, message: "Curso atualizado com sucesso");

            }
            catch
            {
                return new Response<Curso?>(null, 500, "Nao foi possível atualizar o curso");
            }

        }
        public async Task<Response<Curso?>> DeleteAsync(DeleteCursoRequest request)
        {
            try
            {
                // Verifica se o curso existe
                var curso = await context
                .Cursos
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (curso is null)
                    return new Response<Curso?>(null, 404, "Curso não encontrado");

                // Verifica se há módulos associados ao curso
                var hasModulos = await context.Modulos.AnyAsync(m => m.CursoId == curso.Id);

                if (hasModulos)
                {
                    // Retorna erro informando que o curso tem módulos e não pode ser excluído
                    return new Response<Curso?>(null, 400, "Este curso possui módulos associados e não pode ser excluído.");
                }

                // Remove o objeto curso

                context.Cursos.Remove(curso);
                await context.SaveChangesAsync();

                return new Response<Curso?>(curso, message: "Curso excluído com sucesso");

            }
            catch
            {
                return new Response<Curso?>(null, 500, "Não foi possível excluir o curso");
            }
        }
    }          
        
}
