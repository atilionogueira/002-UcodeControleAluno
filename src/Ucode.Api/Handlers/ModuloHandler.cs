using Microsoft.EntityFrameworkCore;
using Ucode.Api.Data;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Modulo;
using Ucode.Core.Responses;

namespace Ucode.Api.Handlers
{
    public class ModuloHandler(AppDbContext context) : IModuloHandler
    {
        public async Task<PagedResponse<List<Modulo>?>> GetAllAsync(GetAllModuloRequest request)
        {
            try
            {
                var query = context
                    .Modulos
                    .AsNoTracking()
                    //   .Include(x => x.Curso)
                    .Where(x => x.UserId == request.UserId)
                    .OrderBy(x => x.SubTopico);

                var modulos = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Modulo>?>(modulos.Count == 0 ? null : modulos, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Modulo>?>(null, 500, "Não foi possível recuperar o modulo");
            }
        }

        public async Task<Response<Modulo?>> GetByAsync(GetModuloByIdRequest request)
        {
            try
            {
                var modulo = await context
                    .Modulos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return modulo is null
                    ? new Response<Modulo?>(null, 404, "Modulo não encontrado")
                    : new Response<Modulo?>(modulo);
            }
            catch
            {
                return new Response<Modulo?>(null, 500, "Não foi possível recuperar o modulo");
            }
        }

        public async Task<Response<Modulo?>> CreateAsync(CreateModuloRequest request)
        {
            try
            {
                var modulo = new Modulo
                {
                    UserId = request.UserId,
                    SubTopico = request.SubTopico,
                    Secao = request.Secao,
                    CursoId = request.CursoId
                };

                await context.Modulos.AddAsync(modulo);
                await context.SaveChangesAsync();

                return new Response<Modulo?>(modulo, 201, "Modulo criado com sucesso");
            }
            catch
            {
                return new Response<Modulo?>(null, 500, "Não foi possível criar o modulo");
            }
        }

        public async Task<Response<Modulo?>> UpdateAsync(UpdateModuloRequest request)
        {
            try
            {
                var modulo = await context
                    .Modulos
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (modulo is null)
                    return new Response<Modulo?>(null, 404, "Modulo não encontrado");

                modulo.CursoId = request.CursoId;
                modulo.SubTopico = request.SubTopico;
                modulo.Secao = request.Secao;

                context.Modulos.Update(modulo);
                await context.SaveChangesAsync();

                return new Response<Modulo?>(modulo, message: "Modulo atualizado com sucesso");
            }
            catch
            {
                return new Response<Modulo?>(null, 500, "Não foi possível atualizar o modulo");
            }
        }

        public async Task<Response<Modulo?>> DeleteAsync(DeleteModuloRequest request)
        {
            try
            {
                var modulo = await context
                    .Modulos
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (modulo is null)
                    return new Response<Modulo?>(null, 404, "Modulo não encontrado");

                context.Modulos.Remove(modulo);
                await context.SaveChangesAsync();

                return new Response<Modulo?>(modulo, message: "Modulo excluído com sucesso");
            }
            catch
            {
                return new Response<Modulo?>(null, 500, "Não foi possível excluir o modulo");
            }
        }

        public Task<Response<List<Modulo>>> GetByCursoIdAsync(GetModulosByCursoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

