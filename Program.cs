using Microsoft.Extensions.Configuration;

public class Program 
{
    public static async Task Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        decimal valorAtual = 0m;
        decimal valMax = 0m;
        decimal valMin = 0m;
        if(args.Length == 3)
        {
            

            AtivoB3 ativo= new AtivoB3();
            valMax = decimal.Parse(args[1]);
            valMin = decimal.Parse(args[2]);
            int timer = 1000;
            int delay = 5;
            int contador = delay;

            while (true)
            {
                valorAtual = await ativo.ValorAtual(args[0]);
                Console.WriteLine(valorAtual);
                contador ++;
                if(valorAtual > valMax)
                {   
                    if(contador >= delay)
                    {
                        Console.WriteLine("valor acima , venda");
                        contador = 0;
                    }
            
                }else if(valorAtual < valMin)
                {   
                    if(contador >= delay)
                    {
                        Console.WriteLine("valor abaixo , compre");
                        contador = 0;
                    }
                    
                }
                await Task.Delay(timer);

            }
            
            
            
        }
        else
        {
            Console.WriteLine("Por favor, forneça o nome do ativo, valor máximo e valor mínimo como argumentos.");
            return;
        }

        
        
        
    }
}