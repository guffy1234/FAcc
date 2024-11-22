using System.Globalization;

namespace FuelAcc.Localization
{
    public static class Localizations
    {
        public static ICollection<CultureInfo> SupportedCultures { get; } = new[] {
            new CultureInfo("en-US"),
            new CultureInfo("ru-RU"),
            new CultureInfo("uk-UA"),
        };
    }
}