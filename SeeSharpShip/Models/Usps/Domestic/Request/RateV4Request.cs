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
using System.Xml.Serialization;

namespace SeeSharpShip.Models.Usps.Domestic.Request {
    public class RateV4Request : IRateRequest {
        public RateV4Request() {
            // Enables full V4 rate functionality
            Revision = "2";
        }

        [XmlElement("Package", Order = 2)]
        public List<DomesticPackage> Packages { get; set; }

        #region IRateRequest Members

        [XmlAttribute(AttributeName = "USERID")]
        public string UserId { get; set; }

        [XmlAttribute(AttributeName = "PASSWORD")]
        public string Password { get; set; }

        [XmlAttribute]
        public string Version { get; set; }

        /// <summary>
        ///   Optional for "Base" RateV4 functionality.  For full RateV4 functionality use Revision="2"
        /// </summary>
        /// <remarks>
        ///   "2" is the default
        /// </remarks>
        [XmlElement(Order = 1)]
        public string Revision { get; set; }

        #endregion
    }
}