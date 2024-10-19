using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Reflection.Metadata;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Alunos;

namespace Ucode.Web.Pages.Alunos;

    public partial class EditAlunoPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public UpdateAlunoRequest InputModel { get; set; } = new();
        #endregion

        #region Parameter
        [Parameter]
        public string Id { get; set; } = string.Empty;
        #endregion

        #region Services
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public IAlunoHandler Handler { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            GetAlunoByIdRequest? request = null!;
            try
            {
                request = new GetAlunoByIdRequest
                {
                    Id = long.Parse(Id)
                };
            }
            catch
            {
                Snackbar.Add("Parâmetro Inválido", Severity.Error);
            }

            if (request is null) return;

            IsBusy = true;
            try
            {
            await Task.Delay(500);
                var response = await Handler.GetByIdAsync(request);

                //if (response.IsSucess && response.Data is not null)
                if (response is { IsSucess: true, Data: not null })
                    InputModel = new UpdateAlunoRequest
                    {
                        Id = response.Data.Id,
                        Nome = response.Data.Nome,
                        Contato = response.Data.Contato,
                        Email = response.Data.Email,
                        Instagram = response.Data.Instagram,
                        Cidade = response.Data.Cidade,
                        Estado = response.Data.Estado
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
    #region
    public async Task OnValidSubmitAsync() 
    {
        IsBusy = true;

        try
        {
            var result = await Handler.UpdateAsync(InputModel);

            if (result.IsSucess) 
            {
                Snackbar.Add("Aluno(a) ataulizado(a)", Severity.Error);
                NavigationManager.NavigateTo("/alunos");
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
