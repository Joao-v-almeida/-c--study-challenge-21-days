using Microsoft.VisualStudio.TestTools.UnitTesting;
using program.Models;

namespace program.Infrastructure;

[TestClass]
public class JsonDriverTest
{
    public JsonDriverTest()
    {
        var path = Environment.GetEnvironmentVariable("TEST_FILE_PATH_DOTNET") ?? "/tmp";
        this.testFilePath = path;
        this.jsonDriver = new JsonDriver(this.testFilePath);

    }

    private JsonDriver jsonDriver;
    private string testFilePath;

    [TestMethod]
    public async Task PropertiesTestDriverClient()
    {
        
        var client = new Client()
        {
            Id = Guid.NewGuid().ToString(),
            Nome = "João Almeida",
            Telefone = "43 99644-7033",
            Email = "joao@teste.com"
        };

        await jsonDriver.Save(client);

        var exist = File.Exists(testFilePath) + "/clients.json";
    }

    [TestMethod]
    public async Task PropertiesTestDriverCurrentAccount()
    {

        var currentAccount = new CurrentAccount()
        {
            IdCliente = Guid.NewGuid().ToString(),
            Valor = 100.01,
            Data = DateTime.Now
    };

        await jsonDriver.Save(currentAccount);

        var exist = File.Exists(testFilePath) + "currentAccounts/.json";
    }
}