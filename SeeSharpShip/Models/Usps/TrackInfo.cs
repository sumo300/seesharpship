#region SeeSharpShip is Copyright (C) 2013-2013 Michael J. Sumerano.

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

namespace SeeSharpShip.Models.Usps {
    public class TrackInfo : ITrackInfo {
        public string GuaranteedDeliveryDate { get; set; }

        public string TrackSummary { get; set; }

        public List<string> TrackDetail { get; set; }

        #region ITrackInfo Members

        public string Id { get; set; }

        public RequestError Error { get; set; }

        #endregion
    }
}
