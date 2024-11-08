using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Curso;
using Ucode.Core.Requests.Modulo;

namespace Ucode.Web.Pages.Modulos
{
    public partial class EditModuloPage : ComponentBase
    {
        #region Properties
        [Parameter]
        public string Id { get; set; } = string.Empty;
        public bool IsBusy { get; set; } = false;
        public UpdateModuloRequest InputModel { get; set; } = new();
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
            await GetModuloByIdAsync();
            await GetCursosAsync();
            IsBusy = false;
        }
        #endregion

        #region Methods
        public async Task OnValidSubmitAsync() 
        {
            IsBusy = true;
            try
            {
                var result = await ModuloHandler.UpdateAsync(InputModel);
                if (result.IsSucess) 
                {
                    Snackbar.Add("Modulo Atualizado", Severity.Success);
                    NavigationManager.NavigateTo("/modulos");
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message,Severity.Error);
            }
            finally 
            {
                IsBusy = false;
            }
        }
        #endregion

        #region Private Methods

        private async Task GetModuloByIdAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetModuloByIdRequest { Id = long.Parse(Id) };
                var result = await ModuloHandler.GetByAsync(request);
                if (result is { IsSucess: true, Data: not null })
                {
                    InputModel = new UpdateModuloRequest
                    {
                        CursoId = result.Data.CursoId,
                        SubTopico = result.Data.SubTopico,
                        Secao = result.Data.Secao,
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
       private async Task GetCursosAsync() 
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
     
    }
}
