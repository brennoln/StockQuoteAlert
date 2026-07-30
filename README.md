# Stock Quote Alert

Projeto de alertas de compra e venda de Ativos em C#

# Objetivo e Requisitos do Projeto

O projeto atende integralmente às especificações propostas:

* **Aplicação Console em C#:** executável via linha de comando, recebendo três parâmetros posicionais:

  ```text
  <ATIVO> <PRECO_VENDA> <PRECO_COMPRA>
  ```

* **Arquivo de configuração (`appsettings.json`):** armazena as credenciais do servidor SMTP e o endereço de e-mail que receberá os alertas.

* **Monitoramento contínuo:** realiza consultas periódicas ao preço do ativo enquanto a aplicação estiver em execução , gerando um relatorio continuo no terminal que mostra o valor a cada segundo e indica quando um email de alerta foi enviado.

* **Gatilhos para envio de alertas:**

  * Preço **acima** do limite de venda → envia um e-mail recomendando **venda**.
  * Preço **abaixo** do limite de compra → envia um e-mail recomendando **compra**.

---

# Funcionalidades Extras e Boas Práticas

### 1. Lógica Anti-Spam Adaptativa (Controle de Flood)

* **Limite de notificações por patamar:** limita o envio a três alertas consecutivos para um mesmo nível de preço fora da faixa configurada.
* **Filtro de variação percentual (1%):** após atingir o limite de notificações, um novo alerta só é enviado caso ocorra uma variação de pelo menos **1%** no preço.
* **Reset automático:** quando o ativo retorna para a faixa neutra (entre os limites de compra e venda), o contador de notificações é reiniciado.

### 2. Resiliência (`Resiliency Pattern`)

* Tratamento global de exceções dentro do loop de monitoramento.
* Falhas temporárias de conexão com a API ou problemas de rede não encerram a aplicação.

### 3. Injeção de Dependências e Configuração

* Utilização do `Microsoft.Extensions.Configuration` para leitura das configurações de forma **strongly typed**.

### 4. Controle de Rate Limit e Cooldown

* Intervalos controlados entre consultas e envios de e-mail para evitar bloqueios por excesso de requisições tanto da API quanto do servidor SMTP.

---

# Tecnologias Utilizadas

* **Linguagem:** C# (.NET 8 SDK)
* **Configuração:** `Microsoft.Extensions.Configuration`
* **Leitura de JSON:** `Microsoft.Extensions.Configuration.Json`
* **Envio de e-mail:** SMTP (`System.Net.Mail`)
* **Arquitetura:** Aplicação Console Assíncrona (`async/await`)

---

# Configuração (`appsettings.json`)

Antes de executar a aplicação, configure as credenciais do seu servidor SMTP no arquivo `appsettings.json` localizado na raiz do projeto.

```json
{
  "EmailDaEmpresa": "seu-email@dominio.com",
  "SenhaDoEmail": "sua-senha-ou-app-password",
  "EmailCliente": "destino-dos-alertas@dominio.com"
}
```

---

# Como Executar

## Publicando a aplicação

Abra um terminal na pasta raiz do projeto e execute:

```bash
dotnet publish -c Release -r win-x64 --self-contained true /p:SingleFile=true
```

## Localização do executável

Após a publicação, o executável será gerado em:

```text
bin/Release/net8.0/win-x64/publish/
```

## Executando a aplicação

Navegue até a pasta onde o executável foi gerado (./publish/) e execute:

```powershell
  stock-quote-alert.exe PETR4 22,67 22,59
```

Onde:

* `PETR4` → código do ativo.
* `22,67` → preço de venda.
* `22,59` → preço de compra.

(Atenção para o uso da virgula !!)

---

# Uso de Inteligência Artificial

IA utilizada : **Gemini (versão gratuita)**.

A utilização ocorreu principalmente para:

* Comparar conceitos entre **Java** e **C#**, facilitando a adaptação para uma nova linguagem.
* Esclarecer dúvidas sobre recursos da linguagem C#.
* Uso para fins didaticos.
* Ajudar a fazer o README.md 

