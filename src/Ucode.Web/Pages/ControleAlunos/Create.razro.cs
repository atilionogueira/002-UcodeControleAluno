using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.ControleAluno;
using Ucode.Core.Requests.Curso;
using Ucode.Core.Requests.Modulo;



namespace Ucode.Web.Pages.ControleAlunos
{
    public partial class CreateControleAlunoPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public CreateControleAlunoRequest InputModel { get; set; } = new();
        public List<Curso> Cursos { get; set; } = [];
        public List<Modulo> Modulos { get; set; } = [];

        #endregion
        #region Services

        [Inject]
        public IControleAlunoHandler ControleAlunoHandler { get; set; } = null!;

        [Inject]
        public IModuloHandler ModuloHandler { get; set; } = null!;

        [Inject]
        public ICursoHandler CursoHandler { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        #endregion

        #region Overrides
            
        protected override async Task OnInitializedAsync()
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
                    InputModel.CursoId = Cursos.FirstOrDefault()?.Id ?? 0;
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
        #region Methods
                         
        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await ControleAlunoHandler.CreateAsync(InputModel);
                if (result.IsSucess)
                {
                    Snackbar.Add(result.Message, Severity.Success);
                    NavigationManager.NavigateTo("/controlealunos");
                }
                else
                    Snackbar.Add(result.Message, Severity.Error);
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
