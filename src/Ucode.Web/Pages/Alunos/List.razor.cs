using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Alunos;

namespace Ucode.Web.Pages.Alunos
{
    public partial class ListAlunosPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;

        public List<Aluno> Alunos { get; set; } = [];

        public string SearchTerm { get; set; } = string.Empty;

        #endregion

        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IAlunoHandler Handler { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllAlunoRequest();
                var result = await Handler.GetAllAsync(request);
                if (result.IsSucess)
                    Alunos = result.Data ?? []; // new list<Category>() que é um lista
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

        public Func<Aluno, bool> Filter => alunos =>
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;

            if (alunos.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if (alunos.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        // Exibe um DialogService(Model)
        public async void OnDeleteButtonClickedAsync(long id , string nome) 
        {
            var result = await DialogService.ShowMessageBox(
                "Atenção",
                $"Ao prosseguir o(a) aluno(a) {nome} será excluido(a). Esta é uma ação irreversível! Desja continuar?",
                yesText: "Excluir",
                cancelText: "Cancelar");

            if (result is true)
                await OnDeleteAsync(id,nome);

            StateHasChanged(); // remove um linha do MudDataGrid não precisa fazer um sql no bd 
        }

        public async Task OnDeleteAsync(long id, string nome) 
        {
            try
            {
                var request = new DeleteAlunoRequest { Id = id };
                await Handler.DeleteAsync(request);
                // await Handler.DeleteAsync(new DeleteAlunoRequest { Id = id }); // pode ser assim.
                Alunos.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Aluno(a) {nome} excluido(a)",Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
            
        #endregion
    }
}
