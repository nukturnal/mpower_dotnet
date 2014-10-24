using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MPowerPayments.Tests
{
    [TestClass]
    public class DirectMobile
    {
        private readonly MPowerSetup _setup = new MPowerSetup()
        {
            MasterKey = "",
            PrivateKey = "",
            Token = "",
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
            var response = directMobile.Charge("Alfred Rowe", "0244000001"
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
