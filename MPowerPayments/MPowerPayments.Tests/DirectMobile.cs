using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MPowerPayments.Tests
{
    [TestClass]
    public class DirectMobile
    {
        private readonly MPowerSetup _setup = new MPowerSetup()
        {
            MasterKey = "8ddbcff0-ee7b-4986-96aa-b7003fd37157",
            PrivateKey = "live_private_MC5E0gc6pXoe5jFICY8WdERFsvk",
            Token = "73f26869ee40513a58a2",
            Mode = "live",
        };

        private readonly MPowerStore _store = new MPowerStore()
        {
            Name = "Awesome Store",
        };

        [TestMethod]
        public void DirectMobileCharge()
        {
            var directMobile = new MPowerDirectMobile(_setup, _store);
            var response = directMobile.Charge("Alfred Rowe", "0244124661"
                , "alfred@example.com", "MTN", 1);
            Assert.IsTrue(response);
        }

        [TestMethod]
        public void DirectMobileStatus()
        {
            var directMobile = new MPowerDirectMobile(_setup, _store);
            var response = directMobile.Confirm("95c45ebe083a495392b6e1a4");
            Assert.AreEqual("00", directMobile.ResponseCode);
        }
    }
}
