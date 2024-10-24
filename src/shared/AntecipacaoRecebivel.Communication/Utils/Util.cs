namespace AntecipacaoRecebivel.Communication.Utils;

public static class Util
{
	public static string LimpaCnpj(string cnpj) => new string(cnpj.Where(char.IsDigit).ToArray());
}
