using YahooFinanceApi;

public class AtivoB3
{
    public async Task<decimal> ValorAtual(String artigo)
    {
        string simbolo = artigo.EndsWith(".SA") ? artigo : $"{artigo}.SA";

        var resultado = await Yahoo.Symbols(simbolo)
                                   .Fields(Field.RegularMarketPrice)
                                   .QueryAsync();

        return Convert.ToDecimal(resultado[simbolo].RegularMarketPrice);
        
    }
    

}