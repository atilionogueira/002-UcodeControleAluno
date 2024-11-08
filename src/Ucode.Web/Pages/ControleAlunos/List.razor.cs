using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ucode.Core.Enums;
using Ucode.Core.Handlers;
using Ucode.Core.Models;
using Ucode.Core.Requests.ControleAluno;
using Ucode.Core.Requests.Curso;
using Ucode.Core.Requests.Modulo;

namespace Ucode.Web.Pages.ControleAlunos
{
    public partial class ListControleAlunosPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public List<ControleAluno> ControleAlunos { get; set; } = new List<ControleAluno>();
        public List<Curso> Cursos { get; set; } = new List<Curso>();
        public List<Modulo> Modulos { get; set; } = new List<Modulo>();
        public string SearchTerm { get; set; } = string.Empty;

        #endregion

        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public IDialogService DialogService { get; set; } = null!;
        [Inject]
        public IControleAlunoHandler ControleAlunoHandler { get; set; } = null!;
        [Inject]
        public IModuloHandler ModuloHandler { get; set; } = null!;
        [Inject]
        public ICursoHandler CursoHandler { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                /*
                // Carrega os Cursos
                var cursoResult = await CursoHandler.GetAllAsync(new GetAllCursoRequest());
                Cursos = cursoResult.IsSucess ? cursoResult.Data ?? new() : new();

                // Carrega os Módulos
                var moduloResult = await ModuloHandler.GetAllAsync(new GetAllModuloRequest());
                Modulos = moduloResult.IsSucess ? moduloResult.Data ?? new() : new();
                */

                var cursoRequest = new GetAllCursoRequest();
                var cursoResult = await CursoHandler.GetAllAsync(cursoRequest);
                if (cursoResult.IsSucess)
                    Cursos = cursoResult.Data ?? [];

                var moduloRequest = new GetAllModuloRequest();
                var moduloResult = await ModuloHandler.GetAllAsync(moduloRequest);
                if (cursoResult.IsSucess)
                    Modulos = moduloResult.Data ?? [];


                // Carrega ControleAlunos e associa com Curso e Modulo

                var controleAlunoRequest = new GetAllControleAlunoRequest();
                var controleAlunoResult = await ControleAlunoHandler.GetAllAsync(controleAlunoRequest);
                if (controleAlunoResult.IsSucess && controleAlunoResult.Data is not null)
                {
                    ControleAlunos = controleAlunoResult.Data;

                    // Associa cada ControleAluno ao Curso e Modulo
                    foreach (var alunoControle in ControleAlunos)
                    {
                        alunoControle.Curso = Cursos.FirstOrDefault(c => c.Id == alunoControle.CursoId) ?? new Curso();
                        alunoControle.Modulo = Modulos.FirstOrDefault(m => m.Id == alunoControle.ModuloId) ?? new Modulo();
                    
                    }
                }

                // Atualiza a interface após carregar e relacionar dados
                StateHasChanged();
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

        public async void OnDeleteButtonClickedAsync(long id, string curso)
        {
            var result = await DialogService.ShowMessageBox(
                "ATENÇÃO",
                $"Ao prosseguir, o registro '{curso}' será excluído. Esta é uma ação irreversível! Deseja continuar?",
                yesText: "EXCLUIR",
                cancelText: "Cancelar");

            if (result is true)
                await OnDeleteAsync(id, curso);

            StateHasChanged();
        }

        public async Task OnDeleteAsync(long id, string curso)
        {
            try
            {
                var request = new DeleteControleAlunoRequest { Id = id };
                await ControleAlunoHandler.DeleteAsync(request);
                ControleAlunos.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Controle de Aluno '{curso}' excluído", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        public Func<ControleAluno, bool> Filter => controleAluno =>
        {
            // Verifica se o status do ControleAluno é "A Concluir"
            bool isStatusAConcluir = controleAluno.Status == EStatus.AConcluir;

            // Retorna false se o status não for "A Concluir"
            if (!isStatusAConcluir)
                return false;

            if (string.IsNullOrWhiteSpace(SearchTerm))
                return true;

            if (controleAluno.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                 return true;

            if (controleAluno.Modulo?.SubTopico.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ?? false)
                return true;
            
            if (controleAluno.Curso?.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase) ?? false)
                return true;

            return false;

        };

        #endregion
    }
}
