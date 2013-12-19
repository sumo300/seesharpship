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

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SeeSharpShip.Model.Usps.Domestic.Response {
    public class Postage : IEquatable<Postage> {
        [XmlAttribute(AttributeName = "CLASSID")]
        public string ClassId { get; set; }

        public string MailService { get; set; }
        public decimal Rate { get; set; }
        public decimal CommercialRate { get; set; }
        public string MaxDimensions { get; set; }
        public List<SpecialService> SpecialServices { get; set; }

        #region IEquatable<Postage> Members

        public bool Equals(Postage other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return Equals(other.ClassId, ClassId) && Equals(other.MailService, MailService) && other.Rate == Rate && other.CommercialRate == CommercialRate &&
                   Equals(other.MaxDimensions, MaxDimensions) && Equals(other.SpecialServices, SpecialServices);
        }

        #endregion

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj.GetType() == typeof (Postage) && Equals((Postage) obj);
        }

        public override int GetHashCode() {
            unchecked {
                int result = (ClassId != null ? ClassId.GetHashCode() : 0);
                result = (result*397) ^ (MailService != null ? MailService.GetHashCode() : 0);
                result = (result*397) ^ Rate.GetHashCode();
                result = (result*397) ^ CommercialRate.GetHashCode();
                result = (result*397) ^ (MaxDimensions != null ? MaxDimensions.GetHashCode() : 0);
                result = (result*397) ^ (SpecialServices != null ? SpecialServices.GetHashCode() : 0);
                return result;
            }
        }
    }
}