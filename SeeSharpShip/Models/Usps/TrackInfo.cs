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

using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SeeSharpShip.Models.Usps {
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class TrackInfo {
        [XmlElement(Form = XmlSchemaForm.Unqualified)]
        public string TrackSummary { get; set; }

        [XmlElement(IsNullable = true)]
        public string GuaranteedDeliveryDate { get; set; }

        [XmlElement(ElementName = "TrackDetail", Form = XmlSchemaForm.Unqualified, IsNullable = true)]
        public List<TrackDetail> TrackDetail { get; set; }

        [XmlAttribute(AttributeName = "ID")]
        public string Id { get; set; }

        [XmlElement(IsNullable = true)]
        public RequestError Error { get; set; }
    }
}
