
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Curso;

namespace Ucode.Web.Pages.Cursos
{
    public partial class EditCursoPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public UpdateCursoRequest InputModel { get; set; } = new();
        #endregion

        #region Parameters
        [Parameter]
        public string Id { get; set; } = string.Empty;
        #endregion

        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ICursoHandler Handler { get; set; } = null!;

        #endregion

        #region override 
        protected override async Task OnInitializedAsync()
        {
            GetCursoByIdRequest? request = null!;
            try
            {
                request = new GetCursoByIdRequest
                {
                    Id = long.Parse(Id)
                };
            }
            catch (Exception)
            {
                Snackbar.Add("Parâmetro inválido", Severity.Error);
            }

            if (request is null) return;

            IsBusy = true;
            try
            {
                await Task.Delay(500);
                var response = await Handler.GetByIdAsync(request);
                //if (response.IsSucess && response.Data is not null)
                if (response is { IsSucess: true, Data: not null })  // usando merge into pattern
                    InputModel = new UpdateCursoRequest
                    {
                        Id = response.Data.Id,
                        Nome = response.Data.Nome,
                        Resumo = response.Data.Resumo,
                        Categoria = response.Data.Categoria
                    };
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

        #region Nethods
        public async Task OnValidSubmitAsync() 
        {
            IsBusy = true;
            try
            {
                var result = await Handler.UpdateAsync(InputModel);
                if (result.IsSucess) 
                {
                    Snackbar.Add("Curso atualizado",Severity.Success);
                    NavigationManager.NavigateTo("/cursos");
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
