using Microsoft.VisualStudio.TestTools.UnitTesting;
using program.Models;
using program.Services;

namespace ProgramTest.Services;

[TestClass]
public class ClientServiceTest
{
    [TestMethod]
    public void TestingSingleInstance()
    {
        Assert.IsNotNull(ClientService.Get());
        Assert.IsNotNull(ClientService.Get().List);

        ClientService.Get().List.Add(new Client()
        {
            Nome = "test"
    });

    Assert.AreEqual(1, ClientService.Get().List.Count);
    }
}
