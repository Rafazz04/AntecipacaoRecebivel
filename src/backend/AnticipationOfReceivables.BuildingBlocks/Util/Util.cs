using System.Text.RegularExpressions;

namespace AnticipationOfReceivables.BuildingBlocks.Util;

public static class Util
{
    public static string ClearFormatting(string number) =>
        string.IsNullOrWhiteSpace(number) ? string.Empty : Regex.Replace(number, "[^0-9]", "");
    public static string ClearCnpj(string cnpj) => new string(cnpj.Where(char.IsDigit).ToArray());
}
