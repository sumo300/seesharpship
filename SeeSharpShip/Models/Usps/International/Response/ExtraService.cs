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

using System.Xml.Serialization;

namespace SeeSharpShip.Models.Usps.International.Response {
    public class ExtraService {
        [XmlElement("ServiceID")]
        public string ServiceId { get; set; }

        public string ServiceName { get; set; }
        public string Available { get; set; }
        public decimal Price { get; set; }
        public string DeclaredValueRequired { get; set; }
    }
}