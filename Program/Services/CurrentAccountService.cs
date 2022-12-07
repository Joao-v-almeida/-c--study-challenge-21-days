using program.Models;

namespace program.Services;

public class CurrentAccountService
{
    private CurrentAccountService() { }

    private static CurrentAccountService instance = default!;

    public static CurrentAccountService Get()
    {
        if(instance == null)
            instance = new CurrentAccountService();
        
        return instance;
    }

    //Methods
    public List<CurrentAccount> List = new List<CurrentAccount>();

    public List<CurrentAccount> ExtractClient(string idClient)
    {
        var accountClient = List.FindAll(c => c.IdCliente == idClient);
        if (List.Count == 0)
            return new List<CurrentAccount>();

        return accountClient;
    }

    public double CustomerBalance(string idClient, List<CurrentAccount>? currentAccountClient = null)
    {
        if (currentAccountClient == null)
            currentAccountClient = ExtractClient(idClient);

        if (currentAccountClient.Count == 0) return 0;

        return Convert.ToDouble(currentAccountClient.Sum(cc => cc.Valor));
    }
}
