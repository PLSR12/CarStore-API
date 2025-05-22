using System.Diagnostics.CodeAnalysis;

namespace CarStore.Domain.Extensions
{
    public static class StringExtension
    {
        public static bool NotEmpty([NotNullWhen(true)] this string? value) => !string.IsNullOrEmpty(value);
    }
}
