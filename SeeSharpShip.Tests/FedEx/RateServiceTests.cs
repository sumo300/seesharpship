using System;
using NUnit.Framework;

namespace SeeSharpShip.Tests.FedEx
{
    [TestFixture]
    public class RateServiceTests
    {
        [Test]
        public void Get_EmptyRequest_ThrowsException() { 
            var service = new Services.FedEx.RateService();
            RateRequest request = new RateRequest();
            Assert.Throws(typeof(NotImplementedException), () => service.Get(request));
        }
    }
}
