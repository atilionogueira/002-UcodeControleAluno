using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Globalization;
using Ucode.Core.Handlers;
using Ucode.Core.Models.Reports;
using Ucode.Core.Requests.Reports;

namespace Ucode.Web.Components.Reports
{
    public class AConcluirAndConcluidoChartComponent : ComponentBase
    {
        #region Properties

        public ChartOptions Options { get; set; } = new();
        public List<ChartSeries>? Series { get; set; }
        public List<string> Labels { get; set; } = [];

        #endregion

        #region Services

        [Inject]
        public IReportHandler Handler { get; set; } = null!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion

        #region Override
        protected override async Task OnInitializedAsync()
        {
            await LoadChartDataAsync();
            ConfigureChartOptions();
            StateHasChanged();
        }

        private async Task LoadChartDataAsync()
        {
            try
            {
                var request = new GetConcluidoAndAConcluirRequest();
                var result = await Handler.GetConcluidoAndAConcluirAsync(request);

                if (!result.IsSucess || result.Data is null)
                {
                    Snackbar.Add("Não foi possível obter os dados do relatório", Severity.Error);
                    return;
                }

                PopulateChartData(result.Data);
            }
            catch
            {
               Snackbar.Add("Erro ao obter dados do relatório", Severity.Error);
            }
        }

        private void PopulateChartData(List<ConcluidoAndAConcluir> data)
        {
            var aconcluir = new List<double>();
            var concluido = new List<double>();

            foreach (var item in data)
            {
                aconcluir.Add((double)item.AConcluir);
                concluido.Add((double)item.Concluido);
                Labels.Add(GetMonthName(item.Mes));
            }

            Series = new List<ChartSeries>
    {
        new ChartSeries { Name = "A Concluir", Data = aconcluir.ToArray() },
        new ChartSeries { Name = "Concluído", Data = concluido.ToArray() }
    };
        }

        private void ConfigureChartOptions()
        {
            Options = new ChartOptions
            {
                YAxisTicks = 2,
                LineStrokeWidth = 5,              
                ChartPalette = new[] { "#594AE2", Colors.Red.Default }
            };
        }

        private static string GetMonthName(int month)
            => new DateTime(DateTime.Now.Year, month, 1).ToString("MMMM", CultureInfo.CurrentCulture);

        #endregion
    }
}

