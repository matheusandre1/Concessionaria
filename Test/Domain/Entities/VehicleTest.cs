using agencia.Domain.Entities;

namespace Test.Domain.Entities
{
    [TestClass]
    public class VehicleTest
    {
        [TestMethod]
        public void VeiculoTest()
        {
            var admin = new Vehicle();

            admin.Id = 1;
            admin.Nome = "teste@teste.com";
            admin.Ano = 1999;
            admin.Marca= "BmW";

            Assert.AreEqual(1, admin.Id);
            Assert.AreEqual("teste@teste.com", admin.Nome);
            Assert.AreEqual(123456, admin.Ano);
            Assert.AreEqual("Adm", admin.Marca);

        }

    }

}

