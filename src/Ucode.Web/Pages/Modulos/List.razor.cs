using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.Modulo;
using Ucode.Core.Requests.Curso;


namespace Ucode.Web.Pages.Modulos
{
    public partial class ListModulosPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public List<Modulo> InputModel { get; set; } = new(); // Lista de Modulos
        public List<Curso> Cursos { get; set; } = new(); // Lista de Cursos
        public string SearchTerm { get; set; } = string.Empty;

        #endregion

        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public IModuloHandler ModuloHandler { get; set; } = null!;

        [Inject]
        public ICursoHandler CursoHandler { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                // Busca os Cursos
                var cursoRequest = new GetAllCursoRequest();
                var cursosResult = await CursoHandler.GetAllAsync(cursoRequest);

                if (cursosResult.IsSucess)
                {
                    Cursos = cursosResult.Data ?? new List<Curso>();
                }

                // Busca os Módulos
                var moduloRequest = new GetAllModuloRequest();
                var modulosResult = await ModuloHandler.GetAllAsync(moduloRequest);

                if (modulosResult.IsSucess)
                {
                    InputModel = modulosResult.Data ?? new List<Modulo>();

                    // Vincula os Módulos com seus Cursos correspondentes
                    foreach (var modulo in InputModel)
                    {
                        modulo.Curso = Cursos.FirstOrDefault(curso => curso.Id == modulo.CursoId) ?? new Curso();
                    }
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

        public Func<Modulo, bool> Filter => modulo =>
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;

            if (modulo.SubTopico.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if (modulo.Secao?.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ?? false)
                return true;

            return false;
        };

        public async void OnDeleteButtonClickedAsync(long id, string nome)
        {
            var result = await DialogService.ShowMessageBox(
                "ATENÇÃO",
                $"Ao prosseguir o módulo {nome} será excluído. Esta é uma ação irreversível! Deseja continuar?",
                yesText: "EXCLUIR",
                cancelText: "Cancelar");

            if (result is true)
                await OnDeleteAsync(id, nome);

            StateHasChanged();
        }

        public async Task OnDeleteAsync(long id, string nome)
        {
            try
            {
                var request = new DeleteModuloRequest { Id = id };
                await ModuloHandler.DeleteAsync(request);
                InputModel.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Módulo {nome} excluído", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        #endregion
    }
}
