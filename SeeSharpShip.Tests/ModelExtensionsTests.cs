#region SeeSharpShip.Tests is Copyright (C) 2011-2011 Michael J. Sumerano.

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
using System.Collections.Generic;
using NUnit.Framework;
using SeeSharpShip.Extensions;
using SeeSharpShip.Models.Usps.Domestic;
using SeeSharpShip.Models.Usps.Domestic.Request;

namespace SeeSharpShip.Tests {
    /* [MethodName_StateUnderTest_ExpectedBehavior]

        Examples:
        Public void Sum_NegativeNumberAs1stParam_ExceptionThrown()
        Public void Sum_NegativeNumberAs2ndParam_ExceptionThrown ()
        Public void Sum_simpleValues_Calculated ()
    */

    [TestFixture]
    public class ModelExtensionsTests {
        [Test]
        public void ToXmlString_RateV4Request_ValidXmlString() {
            var request = new RateV4Request {
                                                UserId = "test",
                                                Password = "test",
                                                Packages = new List<DomesticPackage> {
                                                                                         new DomesticPackage {
                                                                                                                 Container = "RECTANGULAR",
                                                                                                                 Height = 10,
                                                                                                                 Length = 10,
                                                                                                                 //Machinable = true,
                                                                                                                 Ounces = 6,
                                                                                                                 Pounds = 5,
                                                                                                                 ReturnLocations = false,
                                                                                                                 ShipDate =
                                                                                                                     string.Format(
                                                                                                                         "{0:dd-MMM-yyyy}",
                                                                                                                         DateTime.Now.Date),
                                                                                                                 Value = "300.00",
                                                                                                                 Width = 10,
                                                                                                                 ZipDestination = "90210",
                                                                                                                 ZipOrigination = "18507",
                                                                                                                 SelectedServiceType =
                                                                                                                     ServiceTypes.Express
                                                                                                             },
                                                                                         new DomesticPackage {
                                                                                                                 Container = "RECTANGULAR",
                                                                                                                 Height = 10,
                                                                                                                 Length = 10,
                                                                                                                 //Machinable = true,
                                                                                                                 Ounces = 6,
                                                                                                                 Pounds = 5,
                                                                                                                 ReturnLocations = false,
                                                                                                                 ShipDate =
                                                                                                                     string.Format(
                                                                                                                         "{0:dd-MMM-yyyy}",
                                                                                                                         DateTime.Now.Date),
                                                                                                                 Value = "300.00",
                                                                                                                 Width = 10,
                                                                                                                 ZipDestination = "90210",
                                                                                                                 ZipOrigination = "18507",
                                                                                                                 SelectedServiceType =
                                                                                                                     ServiceTypes.Priority,
                                                                                                                 SpecialServices =
                                                                                                                     new SpecialServices
                                                                                                                     {SpecialService = new[] {"1"}},
                                                                                                             },
                                                                                     }
                                            };


            string output = request.ToXmlString();
            Assert.That(output.Length, Is.GreaterThan(0));
        }
    }
}