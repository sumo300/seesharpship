#region SeeSharpShip.Tests is Copyright (C) 2012-2012 Michael J. Sumerano.

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
using SeeSharpShip.Model.Usps.Domestic;
using SeeSharpShip.Model.Usps.Domestic.Request;

namespace SeeSharpShip.Tests.Usps.DomesticBuilders {
    public static class Domestic {
        #region Nested type: Package

        public class Package {
            private readonly DomesticPackage _package;

            public Package() { _package = new DomesticPackage(); }

            public Package WithContainer(string container) {
                _package.Container = container;
                return this;
            }

            public Package WithRectangularContainer() { return WithContainer("RECTANGULAR"); }

            public Package WithHeight(decimal height) {
                _package.Height = height;
                return this;
            }

            public DomesticPackage Build() { return _package; }

            public Package WithLength(decimal length) {
                _package.Length = length;
                return this;
            }

            public Package WithOunces(decimal ounces) {
                _package.Ounces = ounces;
                return this;
            }

            public Package WithPounds(int pounds) {
                _package.Pounds = pounds;
                return this;
            }

            public Package WithShipDate(DateTime shipDate) {
                _package.ShipDate = string.Format("{0:dd-MMM-yyyy}", shipDate);
                return this;
            }

            public Package WithValue(decimal value) {
                _package.Value = string.Format("{0:0.00}", value);
                return this;
            }

            public Package WithWidth(decimal width) {
                _package.Width = width;
                return this;
            }

            public Package WithZipDestination(string zip) {
                _package.ZipDestination = zip;
                return this;
            }

            public Package WithZipOrigination(string zip) {
                _package.ZipOrigination = zip;
                return this;
            }

            public Package WithServiceType(ServiceTypes serviceType) {
                _package.SelectedServiceType = serviceType;
                return this;
            }

            public Package WithInsurance() {
                _package.SpecialServices = new SpecialServices {SpecialService = new[] {"1"}};
                return this;
            }

            public Package With(Func<DomesticPackage, string> selector) {
                selector.Invoke(_package);
                return this;
            }
        }

        #endregion

        #region Nested type: Request

        public class Request {
            private readonly RateV4Request _request;

            public Request() { _request = new RateV4Request(); }

            public Request WithCredentials(string userId, string password) {
                _request.UserId = userId;
                _request.Password = password;
                return this;
            }

            public Request WithPackage(Package package) {
                if (_request.Packages == null) {
                    _request.Packages = new List<DomesticPackage>();
                }

                _request.Packages.Add(package.Build());

                return this;
            }

            public RateV4Request Build() { return _request; }
        }

        #endregion
    }
}