using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.ControleAluno;
using Ucode.Core.Requests.Curso;
using Ucode.Core.Requests.Modulo;

namespace Ucode.Web.Pages.ControleAlunos
{
    public partial class EditControleAlunosPage : ComponentBase
    {
        #region Properties
        [Parameter]
        public string Id { get; set; } = string.Empty;
        public bool IsBusy { get; set; } = false;
        public UpdateControleAlunoRequest InputModel { get; set; } = new();
        public List<Curso> Cursos { get; set; } = new List<Curso>();
        public List<Modulo> Modulos { get; set; } = new List<Modulo>();

        #endregion
        #region Services
        [Inject]
        public IControleAlunoHandler ControleAlunoHandlerHandler { get; set; } = null!;

        [Inject]
        public ICursoHandler CursoHandler { get; set; } = null!;

        [Inject]
        public IModuloHandler ModuloHandler { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Overrides
        protected override async Task OnInitializedAsync() 
        {
            IsBusy = true;
            await GetControleAlunoByIdAsync();
            await GetCursosModulosAsync();
            IsBusy = false;
        }

        #endregion
        #region Methods

        public async Task OnValidSubmitAsync() 
        {
            IsBusy = true;
            try
            {
                var result = await ControleAlunoHandlerHandler.UpdateAsync(InputModel);
                if (result.IsSucess) 
                {
                    Snackbar.Add("Controle de Aluno Atualizado ", Severity.Success);
                    NavigationManager.NavigateTo("/controlealunos");
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally 
            {
                IsBusy = false;
            }
        }

        #endregion
        #region Private Methods
        private async Task GetControleAlunoByIdAsync() 
        {
            IsBusy = true;
            try
            {
                var request = new GetControleAlunoByIdRequest { Id = long.Parse(Id) };
                var result = await ControleAlunoHandlerHandler.GetByIdAsync(request);
                if( result is { IsSucess: true, Data : not null }) 
                {
                    InputModel = new UpdateControleAlunoRequest
                    {
                        CursoId = result.Data.CursoId,
                        ModuloId = result.Data.ModuloId,
                        DataFim = result.Data.DataFim,
                        Resumo = result.Data.Resumo,
                        Status = result.Data.Status,
                        Id = result.Data.Id,
                    };

                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GetCursosModulosAsync() 
        {
            IsBusy = true;
            try
            {
                var cursoRequest = new GetAllCursoRequest();
                var cursoResult = await CursoHandler.GetAllAsync(cursoRequest);
                if (cursoResult.IsSucess)
                {
                    Cursos = cursoResult.Data ?? [];
                    InputModel.CursoId = Cursos.FirstOrDefault()?.Id ?? 0;
                }

                var moduloRequest = new GetAllModuloRequest();
                var moduloResult = await ModuloHandler.GetAllAsync(moduloRequest);
                if (moduloResult.IsSucess)
                {
                    Modulos = moduloResult.Data ?? [];
                    InputModel.CursoId = Modulos.FirstOrDefault()?.Id ?? 0;
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
