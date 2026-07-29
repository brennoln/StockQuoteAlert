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
            int spam = 0;
            decimal valorConstante = 0m;

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
                        if(spam <3)
                        {
                            Console.WriteLine("valor acima , venda");
                            await email.Email(emailDaEmpresa, senhaDoEmail, emailCliente, "Alerta de Ação"," Venda a Ação!! ");
                            contador = 0;
                            valorConstante = valorAtual;
                            spam++;
                        }else if (spam >= 3 && valorAtual != valorConstante)
                        {
                            Console.WriteLine("valor acima , venda");
                            await email.Email(emailDaEmpresa, senhaDoEmail, emailCliente, "Alerta de Ação"," Venda a Ação!! ");
                            valorConstante = valorAtual;
                            contador = 0;
                            spam = 1;
                        }
                        else
                        {
                            Console.WriteLine("Spam de email atingido, não será enviado mais emails até que o valor mude.");
                            contador = 0;
                        }
                        
                    }
            
                }else if(valorAtual < valMin)
                {   
                    if(contador >= delay)
                    {
                        if(spam <3)
                        {
                            Console.WriteLine("valor abaixo , compra");
                            await email.Email(emailDaEmpresa, senhaDoEmail, emailCliente, "Alerta de Ação"," Compre a Ação!! ");
                            contador = 0;
                            valorConstante = valorAtual;
                            spam++;
                        }else if (spam >= 3 && valorAtual != valorConstante)
                        {
                            Console.WriteLine("valor abaixo , compra");
                            await email.Email(emailDaEmpresa, senhaDoEmail, emailCliente, "Alerta de Ação"," Compre a Ação!! ");
                            valorConstante = valorAtual;
                            contador = 0;
                            spam = 1;
                        }
                        else
                        {
                            Console.WriteLine("Spam de email atingido, não será enviado mais emails até que o valor mude.");
                            contador = 0;
                        }
                    }

                }
                else
                {
                    spam = 0;
                    Console.WriteLine("Valor dentro da faixa, sem ação necessária.");
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