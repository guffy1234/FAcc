using Asp.Versioning;

namespace FuelAcc.WebApi.Api
{
    public static class ApiDef
    {
        public const string ServiceName = "fuelacc";

        public const string v1 = "1";

        public static string GroupNameFormat { get; set; } = "'v'VVV";
        public static IEnumerable<string> Versions { get; set; } = new string[] { v1 };
        public static ApiVersion DefaultVersion { get; set; } = new ApiVersion(1, 0);
    }
}