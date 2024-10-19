using Microsoft.EntityFrameworkCore;
using Ucode.Api.Data;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Alunos;
using Ucode.Core.Responses;

namespace Ucode.Api.Handlers
{
    public class AlunoHandler(AppDbContext context) : IAlunoHandler
    {
        public async Task<PagedResponse<List<Aluno>>> GetAllAsync(GetAllAlunoRequest request)
        {
            try
            {
                var query = context
               .Alunos
               .AsNoTracking()
               .Where(x => x.UserId == request.UserId)
               .OrderBy(x => x.Nome);


                var alunos = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Aluno>>(alunos, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Aluno>>(null, 500, "Não foi possível recuperar o aluno");
            }
        }

        public async Task<Response<Aluno?>> GetByIdAsync(GetAlunoByIdRequest request)
        {
            try
            {
                var aluno = await context
               .Alunos
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return aluno is null
                    ? new Response<Aluno?>(null, 404, message: "Aluno não encontrado")
                    : new Response<Aluno?>(aluno, message:"Aluno encontrado");
            }
            catch
            {
                return new Response<Aluno?>(null, 500, "Não foi possível recuperar o aluno");
            }
                

        }
        public async Task<Response<Aluno?>> CreateAsync(CreateAlunoRequest request)
        {
            try
            {
                var aluno = new Aluno
                {
                    UserId = request.UserId,
                    Nome = request.Nome,
                    Contato = request.Contato,
                    Email = request.Email,
                    Instagram = request.Instagram,
                    Cidade = request.Cidade,
                    Estado = request.Estado
                };

                await context.Alunos.AddAsync(aluno);
                await context.SaveChangesAsync();

                return new Response<Aluno?>(aluno, 201, "Aluno Criado com sucesso");
            }
            catch 
            {
                return new Response<Aluno?>(null, 500, "Não foi possível criar um aluno");
               
            }
        }

        public async Task<Response<Aluno?>> UpdateAsync(UpdateAlunoRequest request)
        {
            try
            {
                var aluno = await context
               .Alunos
               .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (aluno is null)
                    return new Response<Aluno?>(null, 404, "Aluno não encontrado");

                aluno.Nome = request.Nome;
                aluno.Contato = request.Contato;
                aluno.Email = request.Email;
                aluno.Instagram = request.Instagram;
                aluno.Cidade = request.Cidade;
                aluno.Estado = request.Estado;

                context.Alunos.Update(aluno);
                await context.SaveChangesAsync();

                return new Response<Aluno?>(aluno, message:"Aluno atualizado com sucesso");

            }
            catch 
            {
                return new Response<Aluno?>(null, 500, "Não foi possível altualizar o aluno"); 
            }
        }

        public async Task<Response<Aluno?>> DeleteAsync(DeleteAlunoRequest request)
        {
            try
            {
                var aluno = await context
             .Alunos
             .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (aluno is null)
                    return new Response<Aluno?>(null, 404, "Aluno não encontrado");

                context.Alunos.Remove(aluno);
                await context.SaveChangesAsync();

                return new Response<Aluno?>(aluno, message: "Aluno excluído com sucesso");

            }
            catch
            {
                return new Response<Aluno?>(null, 500, "Não foi possível excluir o aluno");
            }
        }     
               
    }
}
