using Microsoft.Extensions.Configuration;

public class Program 
{
    public static async Task Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        decimal valorAtual = 0m;
        if(args.Length == 3)
        {
            

            AtivoB3 ativo= new AtivoB3();
            valorAtual = await ativo.ValorAtual(args[0]);
            Console.WriteLine(valorAtual);
        }
        else
        {
            Console.WriteLine("Por favor, forneça o nome do ativo, valor máximo e valor mínimo como argumentos.");
            return;
        }

        
        
        
    }
}