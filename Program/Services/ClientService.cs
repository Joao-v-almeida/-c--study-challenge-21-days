using program.Models;

namespace program.Services;

public class ClientService
{
    private ClientService() { }

    private static ClientService instance = default!;

    public static ClientService Get()
    {
        if(instance == null)
            instance = new ClientService();
        
        return instance;
    }

    //Methods
    public List<Client> List = new List<Client>();

}
