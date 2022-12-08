using Microsoft.VisualStudio.TestTools.UnitTesting;
using program.Models;

namespace ProgramTest.Models;

[TestClass]
public class ClientTest
{
    [TestMethod]
    public void PropertiesTest()
    {
        var client = new Client();
        client.Id = "1";
        client.Nome = "João Almeida";
        client.Telefone = "43 99644-7033";
        client.Email = "joao@teste.com";

        Assert.AreEqual("1", client.Id);
        Assert.AreEqual("João Almeida", client.Nome);
        Assert.AreEqual("43 99644-7033", client.Telefone);
        Assert.AreEqual("joao@teste.com", client.Email);
    }
}
