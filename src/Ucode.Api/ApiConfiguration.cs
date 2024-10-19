using System.Reflection.Metadata;

namespace Ucode.Api
{
    public class ApiConfiguration
    {
        public const string CorsPolicyName = "wasm";
        public static string StripeApiKey { get; set; } = string.Empty;
    }
}
