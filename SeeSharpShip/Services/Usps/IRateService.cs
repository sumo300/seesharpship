#region SeeSharpShip is Copyright (C) 2011-2011 Michael J. Sumerano.

// This file is part of SeeSharpShip.
// 
// SeeSharpShip is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SeeSharpShip is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with SeeSharpShip.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System.Collections.Generic;
using SeeSharpShip.Models.Usps.Domestic.Request;
using SeeSharpShip.Models.Usps.Domestic.Response;
using SeeSharpShip.Models.Usps.International.Request;
using SeeSharpShip.Models.Usps.International.Response;

namespace SeeSharpShip.Services.Usps {
    public interface IRateService {
        RateV4Response Get(RateV4Request request);
        IntlRateV2Response Get(IntlRateV2Request request);
        IEnumerable<ServiceInfo> DomesticServices(string userId, string password, string zip);
        IEnumerable<ServiceInfo> InternationalServices(string userId, string password, string zip);
    }
}