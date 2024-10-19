namespace Ucode.Core.Models.Account
{
    public class RoleClaim // role(Permissão: "Admin", "user")
    {
        public string? Issuer { get; set; }  // emissor
        public string? OriginalIssuer { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }
        public string? ValueType { get; set; }
    }
}
