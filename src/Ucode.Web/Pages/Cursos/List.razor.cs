using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Curso;
using Ucode.Core.Requests.Modulo;
using Ucode.Core.Responses;



namespace Ucode.Web.Pages.Cursos
{
    public partial class ListCursosPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public List<Curso> Cursos { get; set; } = [];
        public List<Modulo> Modulos { get; set; } = new List<Modulo>();
        public string SearchTerm { get; set; } = string.Empty;

        #endregion

        #region Services
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public ICursoHandler Handler { get; set; } = null!;
        [Inject]
        public IModuloHandler ModuloHandler { get; set; } = null!;           
        [Inject]
        public IDialogService DialogService { get; set; } = null!;  // Modal tem que registrar no MainLayout.razor

        #endregion

        #region Overrides       

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllCursoRequest();
                var result = await Handler.GetAllAsync(request);
                if (result.IsSucess)
                    Cursos = result.Data ?? [];
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
        public async Task<Response<Curso?>> OnDeleteButtonClickedAsync(long id, string nome)
        {
            if (await CursoPossuiModulosAsync(id))
            {
                ExibirMensagemErroCursoPossuiModulos(nome);
                return new Response<Curso?>(null, 400, $"Curso {nome} possui módulos associados e não pode ser excluído.");
            }

            if (await ConfirmarExclusaoAsync(nome))
            {
                await ExcluirCursoAsync(id, nome);
                return new Response<Curso?>(null, 200, $"Curso {nome} excluído com sucesso.");
            }

            return new Response<Curso?>(null, 400, "A exclusão foi cancelada pelo usuário.");
        }

        private async Task<bool> CursoPossuiModulosAsync(long cursoId)
        {
            var modulos = await ObterModulosAsync();
            return modulos.Any(m => m.CursoId == cursoId);
        }

        private async Task<List<Modulo>> ObterModulosAsync()
        {
            var moduloRequest = new GetAllModuloRequest();
            var modulosResult = await ModuloHandler.GetAllAsync(moduloRequest);

            if (modulosResult.IsSucess)
            {
                return modulosResult.Data ?? new List<Modulo>();
            }

            // Em caso de falha, retorne uma lista vazia ou lance uma exceção, conforme sua necessidade
            return new List<Modulo>();
        }

        private void ExibirMensagemErroCursoPossuiModulos(string nome)
        {
            Snackbar.Add($"Curso {nome} possui módulos associados e não pode ser excluído.", Severity.Error);
        }

        private async Task<bool> ConfirmarExclusaoAsync(string nome)
        {
            var result = await DialogService.ShowMessageBox(
                "ATENÇÃO",
                $"Ao prosseguir, o curso {nome} será excluído. Esta é uma ação irreversível! Deseja continuar?",
                yesText: "EXCLUIR",
                cancelText: "Cancelar");

            return result == true;
        }

        private async Task ExcluirCursoAsync(long id, string nome)
        {
            // Chame o método de exclusão do curso aqui
            await OnDeleteAsync(id, nome);
        }


        public async Task OnDeleteAsync(long id, string nome)
        {
            try
            {
                var request = new DeleteCursoRequest { Id = id };
                var response = await Handler.DeleteAsync(request);

                // Verifica a resposta do backend
                if (!response.IsSucess)
                {
                    Snackbar.Add(response.Message, Severity.Warning);
                    return;
                }

                // Se o curso foi excluído, remove-o da lista e exibe mensagem de sucesso
                Cursos.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Curso {nome} excluído com sucesso.", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }



        // Função para filtrar cursos com base no termo de pesquisa.
        public Func<Curso, bool> Filter => cursos =>
        {
            // Simplificação, verifica `SearchTerm` apenas uma vez.
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;

            // Verifica se o ID ou o nome do curso contém o termo de pesquisa.
            return cursos.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
                || cursos.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase);
        };

        #endregion

    }
}




