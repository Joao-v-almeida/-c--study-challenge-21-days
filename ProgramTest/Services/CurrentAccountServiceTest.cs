using Microsoft.VisualStudio.TestTools.UnitTesting;
using program.Models;
using program.Services;

namespace ProgramTest.Services;

[TestClass]
public class CurrentAccountServiceTest
{
    #region METHODS SETUP
    [TestInitialize]
    public void StartUp()
    {
        CurrentAccountService.Get().List = new List<CurrentAccount>();
    }

    [TestCleanup]
    public void CleanUp()
    {
        CurrentAccountService.Get().List = new List<CurrentAccount>();
    }
    #endregion

    #region METHODS HELPERS
    private void CreateFakeAccountData(string idClient, double[] valores)
    {
        foreach (var valor in valores)
        {
            CurrentAccountService.Get().List.Add(new CurrentAccount()
            {
                IdCliente = idClient,
                Valor = valor,
                Data = DateTime.Now
            });
        }
    }
    #endregion

    [TestMethod]
    public void TestingSingleInstance()
    {
        Console.WriteLine("===TestingSingleInstance===");

        Assert.IsNotNull(CurrentAccountService.Get());
        Assert.IsNotNull(CurrentAccountService.Get().List);

        CurrentAccountService.Get().List.Add(new CurrentAccount()
        {
            IdCliente = "1"
        });

        Assert.AreEqual(1, CurrentAccountService.Get().List.Count);
    }

    [TestMethod]
    public void TestingExtractReturn()
    {
        Console.WriteLine("===TestingExtractReturn===");

        //PREPARATION. (ARANGE)
        var idClient = Guid.NewGuid().ToString();
        CreateFakeAccountData(idClient, new double[] {100.5, 10});

        //DATA PROCESSING. (ACT)
        var extract = CurrentAccountService.Get().ExtractClient(idClient);

        //DATA VALIDATION. (ASSERT)
        Assert.AreEqual(2, extract.Count);
    }

    [TestMethod]
    public void TestingExtractReturnWithVariousAmounts()
    {
        Console.WriteLine("===TestingExtractReturnWithVariousAmounts===");

        //PREPARATION. (ARANGE)
        var idClient = Guid.NewGuid().ToString();
        CreateFakeAccountData(idClient, new double[] {100.01, 40.05});

        var idClient2 = Guid.NewGuid().ToString();
        CreateFakeAccountData(idClient2, new double[] {85.01});

        //DATA PROCESSING. (ACT)
        var extract = CurrentAccountService.Get().ExtractClient(idClient2);

        //DATA VALIDATION. (ASSERT)
        Assert.AreEqual(1, extract.Count);
        Assert.AreEqual(3, CurrentAccountService.Get().List.Count);
    }

    [TestMethod]
    public void TestingCustomerBalance()
    {
        Console.WriteLine("===TestingCustomerBalance===");

        //PREPARATION. (ARANGE)
        var idClient = Guid.NewGuid().ToString();

        CreateFakeAccountData(idClient, new double[] {5, 5, 5, -10});
        CreateFakeAccountData(Guid.NewGuid().ToString(), new double[] {300, 45});

        //DATA PROCESSING. (ACT)
        var customerBalance = CurrentAccountService.Get().CustomerBalance(idClient);

        //DATA VALIDATION. (ASSERT)
        Assert.AreEqual(5, customerBalance);
        Assert.AreEqual(6, CurrentAccountService.Get().List.Count);
    }
}
