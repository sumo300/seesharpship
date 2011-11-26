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
using System.Xml.Serialization;

namespace SeeSharpShip.Models.Usps.International.Request {
    public abstract class GlobalExpressGuaranteed {
        private string _giftFlag;
        private string _poBoxFlag;

        protected GlobalExpressGuaranteed() {
            _poBoxFlag = "N";
            _giftFlag = "N";
        }

        [XmlElement("POBoxFlag")]
        public string PoBoxFlag {
            get { return _poBoxFlag; }
            set {
                if (value != "Y" && value != "N") {
                    throw new ArgumentException("value must be Y or N", "value");
                }
                _poBoxFlag = value;
            }
        }

        public string GiftFlag {
            get { return _giftFlag; }
            set {
                if (value != "Y" && value != "N") {
                    throw new ArgumentException("value must be Y or N", "value");
                }
                _giftFlag = value;
            }
        }
    }
}