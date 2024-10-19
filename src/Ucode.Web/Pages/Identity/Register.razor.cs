using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Ucode.Core.Handlers;
using Ucode.Core.Requests.Account;


namespace Ucode.Web.Pages.Identity
{
    public partial class RegisterPage : ComponentBase
    {
        #region Dependencie
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!; // exibi um notificação da tela
        [Inject]
        public IAccountHandler Handler { get; set; } = null!; // manipular operações das contas do usuários

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!; // para redirecionar urls

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!; // serviço de estado de autenticação do usuário

        #endregion

        #region Properties 
        public bool IsBusy { get; set; } = false;  // verifica estado da página
        public RegisterRequest InputModel { get; } = new(); //Para registrar o usuário

        #endregion

        #region overrides
        protected override async Task OnInitializedAsync()  // faz verificação do usuário
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync(); // obter o estao do usuário
            var user = authState.User; // obter o usuário

            // if (user.Identity is not null && user.Identity.IsAuthenticated) /
            if (user.Identity is { IsAuthenticated: true }) // usando o Pattern Matchin 
                NavigationManager.NavigateTo("/");
        }
        #endregion


        #region Methods
        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await Handler.RegisterAsync(InputModel);

                if (result.IsSucess)
                {
                    Snackbar.Add(result.Message, Severity.Success);
                    NavigationManager.NavigateTo("/login");
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
