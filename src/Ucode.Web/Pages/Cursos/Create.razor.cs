using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Curso;


namespace Ucode.Web.Pages.Cursos
{
    public partial class CreateCursoPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public CreateCursoRequest InputModel { get; set; } = new();

        #endregion

        #region Services
        [Inject]
        public ICursoHandler Handler { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        #endregion

        #region Methods

        public async Task OnValidSubmitAsync() 
        {
            IsBusy = true;
            try
            {
                var result = await Handler.CreateAsync(InputModel);
                if (result.IsSucess) 
                {
                    Snackbar.Add(result.Message, Severity.Success);
                    NavigationManager.NavigateTo("/cursos");
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
