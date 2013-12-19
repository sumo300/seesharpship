#region SeeSharpShip.Tests is Copyright (C) 2011-2012 Michael J. Sumerano.

// This file is part of SeeSharpShip.Tests.
// 
// SeeSharpShip.Tests is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SeeSharpShip.Tests is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with SeeSharpShip.Tests.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using NUnit.Framework;
using SeeSharpShip.Model.Extensions;
using SeeSharpShip.Model.Usps.Domestic;
using SeeSharpShip.Tests.Usps.DomesticBuilders;

namespace SeeSharpShip.Tests {
    [TestFixture]
    public class ModelExtensionsTests {
        [Test]
        public void ToXmlString_RateV4Request_ValidXmlString() {
            Domestic.Request requestBuilder = new Domestic.Request().WithCredentials("test", "test");
            Domestic.Package package1 = new Domestic.Package()
                .WithRectangularContainer()
                .WithHeight(10)
                .WithLength(10)
                .WithOunces(6)
                .WithPounds(5)
                .WithShipDate(DateTime.Now.Date)
                .WithValue(300)
                .WithWidth(10)
                .WithZipDestination("90210")
                .WithZipOrigination("18507")
                .WithServiceType(ServiceTypes.Express);

            Domestic.Package package2 = new Domestic.Package()
                .WithRectangularContainer()
                .WithHeight(10)
                .WithLength(10)
                .WithOunces(6)
                .WithPounds(5)
                .WithShipDate(DateTime.Now.Date)
                .WithValue(300)
                .WithWidth(10)
                .WithZipDestination("90210")
                .WithZipOrigination("18507")
                .WithServiceType(ServiceTypes.Priority)
                .WithInsurance();

            string output = requestBuilder.WithPackage(package1).WithPackage(package2).Build().ToXmlString();
            Assert.That(output.Length, Is.GreaterThan(0));
        }
    }
}