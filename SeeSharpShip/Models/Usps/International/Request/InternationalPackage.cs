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
using SeeSharpShip.Extensions;

namespace SeeSharpShip.Models.Usps.International.Request {
    public class InternationalPackage {
        private string _container;

        public InternationalPackage() {
            // Default ID to a new GUID upon instantiation
            Id = Guid.NewGuid().ToShortId();
        }

        /// <summary>
        ///   No restrictions on number or type of characters provided valid XML syntax and unique to request.
        /// </summary>
        [XmlAttribute(AttributeName = "ID")]
        public string Id { get; set; }

        /// <summary>
        ///   Value must be numeric. Package weight in ounces is computed by (16 * Pounds) + Ounces. Package weight cannot exceed 70 pounds
        /// </summary>
        [XmlElement(Order = 1)]
        public int Pounds { get; set; }

        [XmlElement(Order = 4)]
        public string MailType {
            get { return SelectedMailType.ToDescription(); }
            set {
                var selectedMailType = value.ToEnumSafe<MailType>();
                if (selectedMailType.HasValue) {
                    SelectedMailType = selectedMailType.Value;
                }
            }
        }

        [XmlIgnore]
        public MailType SelectedMailType { get; set; }

        [XmlElement("GXG", Order = 5)]
        public GlobalExpressGuaranteed Gxg { get; set; }

        [XmlElement(Order = 6)]
        public string ValueOfContents { get; set; }

        [XmlElement(Order = 7)]
        public string Country { get; set; }

        [XmlElement(Order = 14)]
        public string OriginZip { get; set; }

        [XmlElement(Order = 15)]
        public string CommercialFlag { get; set; }

        /// <summary>
        ///   Available when Revision='2'.
        ///   Groups the SpecialServices elements.  
        ///   Special Services prices and availability will not be returned when Service = "ALL" or "ONLINE"
        /// </summary>
        [XmlElement(Order = 16)]
        public ExtraServices ExtraServices { get; set; }

        #region Container - Same as domestic

        /// <summary>
        ///   Use to specify special containers or container attributes that may affect postage; otherwise, leave blank.
        ///   RECTANGULAR or NONRECTANGULAR must be indicated when Size=LARGE
        /// </summary>
        [XmlElement(Order = 8)]
        public string Container {
            get { return Size.Equals("REGULAR") ? string.Empty : _container; }
            set {
                if (string.IsNullOrEmpty(value)) {
                    return;
                }

                if (Size.Equals("LARGE", StringComparison.CurrentCultureIgnoreCase) &&
                    !value.Equals("RECTANGULAR", StringComparison.CurrentCultureIgnoreCase) &&
                    !value.Equals("NONRECTANGULAR", StringComparison.CurrentCultureIgnoreCase)) {
                    throw new ArgumentException("Size is set to LARGE.  value must be RECTANGULAR or NONRECTANGLUAR.", "value");
                }
                _container = value;
            }
        }

        #endregion

        #region Size - Same as domestic

        /// <summary>
        ///   Defined as follows:
        ///   REGULAR: Package dimensions are 12" or less;
        ///   LARGE: Any package dimension is larger than 12".
        /// </summary>
        [XmlElement(Order = 9)]
        public string Size {
            get {
                if (Width <= 12 && Length <= 12 && Height <= 12) {
                    return "REGULAR";
                }

                return "LARGE";
            }
            // ReSharper disable ValueParameterNotUsed
            set {
                /* Required for XML Serialization */
            }
            // ReSharper restore ValueParameterNotUsed
        }

        #endregion

        #region Width - same as domestic

        /// <summary>
        ///   Value must be numeric. Units are inches. Required when Size is LARGE.
        /// </summary>
        [XmlElement(Order = 10)]
        public decimal Width { get; set; }

        #endregion

        #region Length - same as domestic

        /// <summary>
        ///   Value must be numeric. Units are inches. Required when Size is LARGE
        /// </summary>
        [XmlElement(Order = 11)]
        public decimal Length { get; set; }

        #endregion

        #region Height - same as domestic

        /// <summary>
        ///   Value must be numeric. Units are inches. Required when Size is LARGE.
        /// </summary>
        [XmlElement(Order = 12)]
        public decimal Height { get; set; }

        #endregion

        #region Girth - same as domestic

        /// <summary>
        ///   Value must be numeric. Units are inches. Required when Size is LARGE, and Container is NONRECTANGULAR or VARIABLE/NULL.
        /// </summary>
        [XmlElement(Order = 13)]
        public decimal Girth {
            get { return 2 * (Height + Width); }
            // ReSharper disable ValueParameterNotUsed
            set {
                /* Required for XML Serialization */
            }
            // ReSharper restore ValueParameterNotUsed
        }

        #endregion

        #region Ounces - Same as domestic

        /// <summary>
        ///   Value must be numeric. Package weight in ounces is computed by (16 * Pounds) + Ounces. Package weight cannot exceed 70 pounds (1120 ounces).
        /// </summary>
        [XmlElement(Order = 2)]
        public decimal Ounces { get; set; }

        #endregion

        #region Machinable - Same as domestic

        /// <summary>
        ///   Machinable is required when:
        ///   Service='FIRST CLASS' and (FirstClassMailType='LETTER' or FirstClassMailType='FLAT') 
        ///   Service='PARCEL POST' 
        ///   Service='ALL'
        ///   Service='ONLINE'
        /// </summary>
        [XmlElement(Order = 3)]
        public bool Machinable { get; set; }

        #endregion
    }
}