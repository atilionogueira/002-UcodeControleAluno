using Microsoft.EntityFrameworkCore;
using Ucode.Api.Data;
using Ucode.Core.Common;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.ControleAluno;
using Ucode.Core.Requests.Modulo;
using Ucode.Core.Responses;

namespace Ucode.Api.Handlers
{
    public class ControleAlunoHandler(AppDbContext context) : IControleAlunoHandler
    {

        public async Task<PagedResponse<List<ControleAluno>?>> GetAllAsync(GetAllControleAlunoRequest request)
        {
            try
            {
                var query = context
                       .ControleAlunos
                       .AsNoTracking()
                       .Where(x => x.UserId == request.UserId)
                       .OrderBy(x => x.CursoId);

                var controleAlunos = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<ControleAluno>?>(controleAlunos.Count == 0 ? null : controleAlunos, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<ControleAluno>?>(null, 500, "Não foi possível recuperar o modulo");
            }
        }
        public async Task<Response<ControleAluno?>> GetByIdAsync(GetControleAlunoByIdRequest request)
        {
            try
            {
                var controlealuno = await context
                    .ControleAlunos
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return controlealuno is null
                    ? new Response<ControleAluno?>(null, 404, "Controle de Aluno não encontrado")
                    : new Response<ControleAluno?>(controlealuno);

            }
            catch 
            {
                return new Response<ControleAluno?>(null, 500, "Não foi possível encontrar o controlde de aluno");
            }
        }
        public async Task<Response<ControleAluno?>> CreateAsync(CreateControleAlunoRequest request)
        {
            try
            {
                var controlealuno = new ControleAluno
                {
                    UserId = request.UserId,
                    DataInicio = DateTime.Now,
                    DataFim = request.DataFim,
                    Resumo = request.Resumo,                 
                    CursoId = request.CursoId,
                    ModuloId = request.ModuloId,
                    Status = request.Status              
                };

                await context.ControleAlunos.AddAsync(controlealuno);
                await context.SaveChangesAsync();

                return new Response<ControleAluno?>(controlealuno, 201, "Controlde de Aluno criado com sucesso");

            }
            catch 
            {
                return new Response<ControleAluno?>(null, 500, "Não foi possível criar o controlde de aluno");
            }
        }  
        public async Task<Response<ControleAluno?>> UpdateAsync(UpdateControleAlunoRequest request)
        {
            try
            {
                var controlealuno = await context
                .ControleAlunos
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (controlealuno is null)
                    return new Response<ControleAluno?>(null, 404, "Controle de Aluno não encontrado");
               
                controlealuno.DataFim = request.DataFim;
                controlealuno.Resumo = request.Resumo;
                controlealuno.Status = request.Status;
                controlealuno.CursoId = request.CursoId;
                controlealuno.ModuloId = request.ModuloId;

                context.ControleAlunos.Update(controlealuno);
                await context.SaveChangesAsync();

                return new Response<ControleAluno?>(controlealuno, 201, "Controle de aluno Atualizado");
            }
            catch 
            {
                return new Response<ControleAluno?>(null, 500, "Não foi possível atualizar o controlde de aluno");
            } 


        }
        public async Task<Response<ControleAluno?>> DeleteAsync(DeleteControleAlunoRequest request)
        {
            try
            {
                var controlealuno = await context
                 .ControleAlunos
                 .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (controlealuno is null)
                    return new Response<ControleAluno?>(null, 404, "Controle de Aluno não encontrado");

                context.ControleAlunos.Remove(controlealuno);
                await context.SaveChangesAsync();

                return new Response<ControleAluno?>(controlealuno,message: "Controle de aluno excluido");
            }
            catch
            {
                return new Response<ControleAluno?>(null, 500, "Não foi possível recuperar seu controle de aluno");
            }

        }

       
    }
}
