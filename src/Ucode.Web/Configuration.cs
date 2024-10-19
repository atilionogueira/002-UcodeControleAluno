using MudBlazor.Utilities;
using MudBlazor;

namespace Ucode.Web
{
    public static class Configuration
    {
        public const string HttpClientName = "ucode";
        public static string BackendUrl { get; set; } = "http://localhost:5270";
   

        public static MudTheme Theme = new() 
        {
            Typography = new Typography
            {
                Default = new Default
                {
                    FontFamily = ["Raleway", "sans-serif" ]
                }
            },

            Palette = new PaletteLight
            {
                Primary = new MudColor("594AE2"), // Roxo profundo
                PrimaryContrastText = new MudColor("#FFFFFF"), // Texto branco para contraste
                Secondary = Colors.LightGreen.Darken2, // Um verde mais suave
                Background = Colors.Grey.Lighten5, // Um fundo mais claro para suavizar
                AppbarBackground = new MudColor("3A2F78"), // Roxo mais escuro para contraste com o Primary
                AppbarText = Colors.Shades.White, // Branco para legibilidade
                TextPrimary = Colors.Shades.Black, // Texto principal preto para contraste
                DrawerText = Colors.Shades.White, // Texto da gaveta preto
                DrawerBackground = new("3A2F78") // Cinza escuro para contraste
            },
            PaletteDark = new PaletteDark
            {
                Primary = new MudColor("594AE2"), // Mantém o roxo como primário
                PrimaryContrastText = new MudColor("#FFFFFF"), // Texto branco para contraste
                Secondary = Colors.LightGreen.Accent3, // Um verde suave que complementa bem o roxo
                Background = Colors.Grey.Darken3, // Fundo cinza escuro, mas não tão pesado
                AppbarBackground = new MudColor("3A2F78"), // Tom mais escuro do roxo
                AppbarText = Colors.Shades.White, // Branco para contraste
                DrawerBackground = new("3A2F78"), // Fundo mais escuro para a gaveta
                DrawerText = Colors.Shades.White // Texto branco para contraste na gaveta
            }

        };
            


    }

}