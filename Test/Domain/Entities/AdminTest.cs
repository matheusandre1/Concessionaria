using agencia.Domain.Entities;

namespace Test.Domain.Entities
{
    [TestClass]
    public class AdminstradorTest
    {
        [TestMethod]
        public void AdminTest()
        {
            var admin = new Admin();

            admin.Id = 1;
            admin.Email = "teste@teste.com";
            admin.Senha = "123456";
            admin.Perfil = "Adm";

            Assert.AreEqual(1, admin.Id);
            Assert.AreEqual("teste@teste.com", admin.Email);
            Assert.AreEqual("123456", admin.Senha);
            Assert.AreEqual("Adm", admin.Perfil);

        }

    }

}
