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
        string emailDaEmpresa = config["EmailDaEmpresa"] ?? "";
        string senhaDoEmail = config["SenhaDoEmail"] ?? "";
        string emailCliente = config["EmailCliente"] ?? "";
        if(args.Length == 3)
        {
            
            

            AtivoB3 ativo= new AtivoB3();
            EnviarEmail email = new EnviarEmail();
            valMax = decimal.Parse(args[1]);
            valMin = decimal.Parse(args[2]);
            int timer = 1000;
            int delay = 60;
            int contador = delay;

            if (valMax <= valMin)
            {
            Console.WriteLine("O valor máximo deve ser maior que o valor mínimo.");
            return;
           }

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
                        await email.Email(emailDaEmpresa, senhaDoEmail, emailCliente, "Alerta de Ação"," Venda a Ação!! ");
                        contador = 0;
                    }
            
                }else if(valorAtual < valMin)
                {   
                    if(contador >= delay)
                    {
                        Console.WriteLine("valor abaixo , compre");
                        await email.Email(emailDaEmpresa, senhaDoEmail, emailCliente, "Alerta de Ação"," Compre a Ação!! ");
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