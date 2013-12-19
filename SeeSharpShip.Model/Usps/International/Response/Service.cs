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
using SeeSharpShip.Model.Usps.International.Request;

namespace SeeSharpShip.Model.Usps.International.Response {
    public class Service {
        [XmlAttribute(AttributeName = "ID")]
        public string Id { get; set; }

        public decimal Pounds { get; set; }
        public decimal Ounces { get; set; }
        public string Machinable { get; set; }
        public string MailType { get; set; }

        [XmlElement("GXG")]
        public GlobalExpressGuaranteed Gxg { get; set; }

        public string Container { get; set; }
        public string Size { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string Height { get; set; }
        public string Girth { get; set; }
        public string Country { get; set; }
        public decimal Postage { get; set; }
        public decimal CommercialPostage { get; set; }
        public List<ExtraService> ExtraServices { get; set; }
        public decimal ValueOfContents { get; set; }
        public string InsComment { get; set; }
        public decimal ParcelIndemnityCoverage { get; set; }
        public string SvcCommitments { get; set; }
        public string SvcDescription { get; set; }
        public string MaxDimensions { get; set; }
        public string MaxWeight { get; set; }

        [XmlElement("GXGLocations")]
        public PostOffice GxgLocations { get; set; }
    }
}