//EXERCÍCIO 6
using logic.Models;
using logic.Services;

bool sair = false;


do
{
    menuPrincipal();

    var opcao = Console.ReadLine()?.Trim();
    Console.Clear();

    switch (opcao)
    {
        case "1":
            Console.WriteLine("Cadastro de cliente.\n");
            cadastrarCliente();
            break;

        case "2":
            mostrarContaCorrente();
            break;

        case "3":
            Console.WriteLine("Adicionar crédito cliente.\n");
            adicionarCreditoCliente();
            break;

        case "4":
            fazerDebitoCliente();
            break;

        case "5":
            sair = true;
            Console.WriteLine("Finalizando sistema Lina. Obrigado por acessar!");
            Thread.Sleep(3000);
            break;

        default:
            Console.WriteLine($"Opção '{opcao}' inválida. Informe uma opção válida!");
            break;
    }

    if (!sair)
    {
        Console.WriteLine("\nVoltando ao menu...");
        Thread.Sleep(3000);
    }

} while (!sair);

//MÉTODOS

void adicionarCreditoCliente()
{
    var cliente = capturaCliente();

    Console.Write("Digite o valor do crédito: ");
    double credito = Convert.ToDouble(Console.ReadLine());

    CurrentAccountService.Get().List.Add(new CurrentAccount
    {
        IdCliente = cliente.Id,
        Valor = credito,
        Data = DateTime.Now
    });
    mensagem($"Crédito adicionado com sucesso. Saldo do cliente {cliente.Nome} é de R$ {CurrentAccountService.Get().CustomerBalance(cliente.Id)}");
}

void fazerDebitoCliente()
{
    var cliente = capturaCliente();

    Console.Write("Enter the withdrawal amount: ");
    double retirada = Convert.ToDouble(Console.ReadLine());

    CurrentAccountService.Get().List.Add(new CurrentAccount
    {
        IdCliente = cliente.Id,
        Valor = retirada * -1,
        Data = DateTime.Now
    });
    mensagem($"Withdrawal completed successfully. Customer balance {cliente.Nome} is R$ {CurrentAccountService.Get().CustomerBalance(cliente.Id)}");
}

void cadastrarCliente()
{
    var idCliente = Guid.NewGuid().ToString();

    Console.Write("Informe o seu nome: ");
    var nomeCliente = Console.ReadLine();

    Console.Write($"{nomeCliente}, informe seu telefone: ");
    var telefoneCliente = Console.ReadLine();

    Console.Write($"{nomeCliente}, informe seu e-maiL: ");
    var emailCliente = Console.ReadLine();

    ClientService.Get().List.Add(new Client
    {
        Id = idCliente,
        Nome = nomeCliente != null ? nomeCliente : "[Sem nome]",
        Telefone = telefoneCliente ?? "[Sem telefone]",
        Email = emailCliente ?? "[Sem e-mail]"
    });
    mensagem("Cliente cadastrado com sucesso!");
}

dynamic capturaCliente()
{
    listarClientesCadastrados();

    Console.Write("Inform the ID: ");
    var idCLiente = Console.ReadLine()?.Trim();

    Client? client = ClientService.Get().List.Find(c => c.Id == idCLiente);

    if (client == null)
    {
        mensagem("Customer not found in list, enter ID correctly as per customer list");
        menuClienteNaoExiste();
        return capturaCliente();
    }

    return client;
}

void listarClientesCadastrados()
{
    if (ClientService.Get().List.Count == 0)
    {
        menuClienteNaoExiste();
    }
    mostrarClientes(false, 0, "===Selecione um cliente da lista===");
}

void mensagem(string msg)
{
    Console.Clear();
    Console.WriteLine(msg);
    Thread.Sleep(2000);
}

void menuClienteNaoExiste()
{
    Console.Write("""
        1 - Cadastrar Cliente
        2 - Voltar Menu
        3 - Sair do programa.

        O que você deseja fazer: 
        """);
    string? opcao = Console.ReadLine()?.Trim();

    switch (opcao)
    {
        case "1":
            cadastrarCliente();
            break;
        case "2":
            break;
        case "3":
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Opção inválida");
            Thread.Sleep(2000);
            break;
    }
}

void menuPrincipal()
{
    Console.Clear();
    Console.WriteLine("====Seja bem vindo a empresa Lina====\n");

    Console.Write("""
    ***MENU INICIAL***

    O que deseja fazer?

    1- Cadastrar o cliente.
    2- Ver extrato cliente.
    3- Crédito em conta
    4- Retirada de valores.
    5- Sair do sistema.

    Digite a opção desejada: 
    """);
}

void mostrarClientes(bool sleep = true, int timerSleep = 2000, string header = "===Lista de clientes===")
{
    Console.Clear();
    Console.WriteLine(header);

    foreach (var cliente in ClientService.Get().List)
    {
        Console.WriteLine("ID: " + cliente.Id);
        Console.WriteLine("Nome: " + cliente.Nome);
        Console.WriteLine("Telefone: " + cliente.Telefone);
        Console.WriteLine("E-mail: " + cliente.Email);
        Console.WriteLine("----------------------");

        if (sleep)
        {
            Thread.Sleep(timerSleep);
        }
    }
}

void mostrarContaCorrente()
{
    if (ClientService.Get().List.Count == 0 || CurrentAccountService.Get().List.Count == 0)
    {
        mensagem("Não existe clientes ou não existe movimentações em conta corrente, cadastre o cliente e faça crédito em conta");
        return;
    }

    var cliente = capturaCliente();
    var contaCorrenteCliente = CurrentAccountService.Get().ExtractClient(cliente.Id);

    foreach (var contaCorrente in contaCorrenteCliente)
    {
        Console.WriteLine($"Data: {contaCorrente.Data.ToString("dd/MM/yyyy HH/mm")}");
        Console.WriteLine($"Saldo: {contaCorrente.Valor}");
        Console.WriteLine("--------------------------");
    }

    Console.WriteLine($"O valor total da conta é do cliente {cliente.Nome} é de R$ {CurrentAccountService.Get().CustomerBalance(cliente.Id, contaCorrenteCliente)}");

    Console.WriteLine("Digite enter para continuar...");
    Console.Read();
}










