using Microsoft.VisualStudio.TestTools.UnitTesting;
using program.Models;

namespace ProgramTest.Models;

[TestClass]
public class CurrentAccountTest
{
    [TestMethod]
    public void PropertiesTest()
    {
        var currentAccount = new CurrentAccount();
        currentAccount.IdCliente = "1";
        currentAccount.Valor = 1;
        currentAccount.Data = DateTime.Now;

        Assert.AreEqual("1", currentAccount.IdCliente);
        Assert.AreEqual(1, currentAccount.Valor);
        Assert.IsNotNull(currentAccount.Data);

    }
}
