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
using SeeSharpShip.Models.Usps.Domestic;
using SeeSharpShip.Models.Usps.Domestic.Request;
using SeeSharpShip.Models.Usps.International.Request;
using SeeSharpShip.Tests.Properties;

namespace SeeSharpShip.Tests.Usps {
    internal static class RateServiceTestsData {
        public static RateV4Request GetDomesticRequest() {
            var request = new RateV4Request {
                                                UserId = Settings.Default.UspsUserId,
                                                Password = Settings.Default.UspsPassword,
                                                Packages = new List<DomesticPackage> {
                                                                                         new DomesticPackage {
                                                                                                                 Container = "RECTANGULAR",
                                                                                                                 Height = 10,
                                                                                                                 Length = 10,
                                                                                                                 Ounces = 6,
                                                                                                                 Pounds = 5,
                                                                                                                 ReturnLocations = false,
                                                                                                                 ShipDate =
                                                                                                                     String.Format(
                                                                                                                         "{0:dd-MMM-yyyy}",
                                                                                                                         DateTime.Now.Date),
                                                                                                                 Value = "600.00",
                                                                                                                 Width = 10,
                                                                                                                 ZipDestination = "90210",
                                                                                                                 ZipOrigination = "18507",
                                                                                                                 SelectedServiceType =
                                                                                                                     ServiceTypes.Express,
                                                                                                                 SpecialServices =
                                                                                                                     new SpecialServices
                                                                                                                     {SpecialService = new[] {"11"}},
                                                                                                             },
                                                                                         new DomesticPackage {
                                                                                                                 Container = "RECTANGULAR",
                                                                                                                 Height = 10,
                                                                                                                 Length = 10,
                                                                                                                 Ounces = 6,
                                                                                                                 Pounds = 5,
                                                                                                                 ReturnLocations = false,
                                                                                                                 ShipDate =
                                                                                                                     String.Format(
                                                                                                                         "{0:dd-MMM-yyyy}",
                                                                                                                         DateTime.Now.Date),
                                                                                                                 Value = "600.00",
                                                                                                                 Width = 10,
                                                                                                                 ZipDestination = "90210",
                                                                                                                 ZipOrigination = "18507",
                                                                                                                 SelectedServiceType =
                                                                                                                     ServiceTypes.Priority,
                                                                                                                 SpecialServices =
                                                                                                                     new SpecialServices
                                                                                                                     {SpecialService = new[] {"1"}},
                                                                                                             },
                                                                                         new DomesticPackage {
                                                                                                                 Container = "RECTANGULAR",
                                                                                                                 Height = 10,
                                                                                                                 Length = 10,
                                                                                                                 Ounces = 6,
                                                                                                                 Pounds = 5,
                                                                                                                 ReturnLocations = false,
                                                                                                                 ShipDate =
                                                                                                                     String.Format(
                                                                                                                         "{0:dd-MMM-yyyy}",
                                                                                                                         DateTime.Now.Date),
                                                                                                                 Value = "600.00",
                                                                                                                 Width = 10,
                                                                                                                 ZipDestination = "90210",
                                                                                                                 ZipOrigination = "18507",
                                                                                                                 SelectedServiceType =
                                                                                                                     ServiceTypes.All,
                                                                                                             },
                                                                                         new DomesticPackage {
                                                                                                                 Container = "RECTANGULAR",
                                                                                                                 Height = 10,
                                                                                                                 Length = 10,
                                                                                                                 Ounces = 6,
                                                                                                                 Pounds = 5,
                                                                                                                 ReturnLocations = false,
                                                                                                                 ShipDate =
                                                                                                                     String.Format(
                                                                                                                         "{0:dd-MMM-yyyy}",
                                                                                                                         DateTime.Now.Date),
                                                                                                                 Value = "600.00",
                                                                                                                 Width = 10,
                                                                                                                 ZipDestination = "90210",
                                                                                                                 ZipOrigination = "18507",
                                                                                                                 SelectedServiceType =
                                                                                                                     ServiceTypes.Online,
                                                                                                             },
                                                                                         new DomesticPackage {
                                                                                                                 Container = "RECTANGULAR",
                                                                                                                 Height = 10,
                                                                                                                 Length = 10,
                                                                                                                 Ounces = 6,
                                                                                                                 Pounds = 5,
                                                                                                                 ReturnLocations = false,
                                                                                                                 ShipDate =
                                                                                                                     String.Format(
                                                                                                                         "{0:dd-MMM-yyyy}",
                                                                                                                         DateTime.Now.Date),
                                                                                                                 Value = "600.00",
                                                                                                                 Width = 10,
                                                                                                                 ZipDestination = "90210",
                                                                                                                 ZipOrigination = "18507",
                                                                                                                 SelectedServiceType =
                                                                                                                     ServiceTypes.Parcel,
                                                                                                                 SpecialServices =
                                                                                                                     new SpecialServices
                                                                                                                     {SpecialService = new[] {"1"}},
                                                                                                             },
                                                                                     }
                                            };
            return request;
        }

        public static IntlRateV2Request GetInternationalRequest() {
            var request = new IntlRateV2Request {
                                                    UserId = Settings.Default.UspsUserId,
                                                    Password = Settings.Default.UspsPassword,
                                                    Packages = new List<InternationalPackage> {
                                                                                                  new InternationalPackage {
                                                                                                                               Pounds = 5,
                                                                                                                               Ounces = 6,
                                                                                                                               Machinable = true,
                                                                                                                               SelectedMailType =
                                                                                                                                   MailType.Package,
                                                                                                                               ValueOfContents = "600.00",
                                                                                                                               Country = "Canada",
                                                                                                                               Container = "RECTANGULAR",
                                                                                                                               Width = 10,
                                                                                                                               Length = 10,
                                                                                                                               Height = 10,
                                                                                                                               OriginZip = "18507",
                                                                                                                               ExtraServices =
                                                                                                                                   new ExtraServices
                                                                                                                                   {ExtraService = new[] {"1"}}
                                                                                                                           },
                                                                                                  new InternationalPackage {
                                                                                                                               Pounds = 5,
                                                                                                                               Ounces = 6,
                                                                                                                               Machinable = true,
                                                                                                                               SelectedMailType =
                                                                                                                                   MailType.All,
                                                                                                                               ValueOfContents = "600.00",
                                                                                                                               Country = "Canada",
                                                                                                                               Container = "RECTANGULAR",
                                                                                                                               Width = 10,
                                                                                                                               Length = 10,
                                                                                                                               Height = 10,
                                                                                                                               OriginZip = "18507",
                                                                                                                           },
                                                                                              }
                                                };
            return request;
        }

        public static RateV4Request GetDomesticRequestWithZipDestinationLessThan5CharactersLong() {
            var request = new RateV4Request {
                                                UserId = Settings.Default.UspsUserId,
                                                Password = Settings.Default.UspsPassword,
                                                Packages = new List<DomesticPackage> {
                                                                                         new DomesticPackage {
                                                                                                                 Container = "RECTANGULAR",
                                                                                                                 Height = 10,
                                                                                                                 Length = 10,
                                                                                                                 Ounces = 6,
                                                                                                                 Pounds = 5,
                                                                                                                 ReturnLocations = false,
                                                                                                                 ShipDate =
                                                                                                                     String.Format(
                                                                                                                         "{0:dd-MMM-yyyy}",
                                                                                                                         DateTime.Now.Date),
                                                                                                                 Value = "600.00",
                                                                                                                 Width = 10,
                                                                                                                 ZipDestination = "9",
                                                                                                                 ZipOrigination = "18507",
                                                                                                                 SelectedServiceType =
                                                                                                                     ServiceTypes.Express,
                                                                                                                 SpecialServices =
                                                                                                                     new SpecialServices
                                                                                                                     {SpecialService = new[] {"11"}},
                                                                                                             },
                                                                                     }
                                            };
            return request;
        }

        public static RateV4Request GetDomesticRequestWithZipDestinationNull()
        {
            var request = new RateV4Request
            {
                UserId = Settings.Default.UspsUserId,
                Password = Settings.Default.UspsPassword,
                Packages = new List<DomesticPackage> {
                                                                                         new DomesticPackage {
                                                                                                                 Container = "RECTANGULAR",
                                                                                                                 Height = 10,
                                                                                                                 Length = 10,
                                                                                                                 Ounces = 6,
                                                                                                                 Pounds = 5,
                                                                                                                 ReturnLocations = false,
                                                                                                                 ShipDate =
                                                                                                                     String.Format(
                                                                                                                         "{0:dd-MMM-yyyy}",
                                                                                                                         DateTime.Now.Date),
                                                                                                                 Value = "600.00",
                                                                                                                 Width = 10,
                                                                                                                 ZipDestination = null,
                                                                                                                 ZipOrigination = "18507",
                                                                                                                 SelectedServiceType =
                                                                                                                     ServiceTypes.Express,
                                                                                                                 SpecialServices =
                                                                                                                     new SpecialServices
                                                                                                                     {SpecialService = new[] {"11"}},
                                                                                                             },
                                                                                     }
            };
            return request;
        }

    }
}