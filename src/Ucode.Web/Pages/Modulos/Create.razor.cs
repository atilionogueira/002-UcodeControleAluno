using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Curso;
using Ucode.Core.Requests.Modulo;

namespace Ucode.Web.Pages.Modulos
{
    public partial class CreateModuloPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public CreateModuloRequest InputModel { get; set; } = new();
        public List<Curso> Cursos { get; set; } = [];

        #endregion

        #region Services

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
                var request = new GetAllCursoRequest();
                var result = await CursoHandler.GetAllAsync(request);
                if (result.IsSucess)
                {
                    Cursos = result.Data ?? [];
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
                var result = await ModuloHandler.CreateAsync(InputModel);
                if (result.IsSucess)
                {
                    Snackbar.Add(result.Message, Severity.Success);
                    NavigationManager.NavigateTo("/modulos");
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
