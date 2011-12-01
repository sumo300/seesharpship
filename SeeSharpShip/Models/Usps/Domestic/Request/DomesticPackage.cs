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

namespace SeeSharpShip.Models.Usps.Domestic.Request {
    public class DomesticPackage {
        private string _container;

        public DomesticPackage() {
            // Default ID to a new GUID upon instantiation
            Id = Guid.NewGuid().ToShortId();
        }

        /// <summary>
        ///   No restrictions on number or type of characters provided valid XML syntax and unique to request.
        /// </summary>
        [XmlAttribute(AttributeName = "ID")]
        public string Id { get; set; }

        /// <summary>
        ///   Web Tool validates the entry to one of the service types.
        /// </summary>
        public string Service {
            get { return SelectedServiceType.ToDescription(); }
            set {
                var selectedServiceType = value.ToEnumSafe<ServiceTypes>();
                if (selectedServiceType.HasValue) {
                    SelectedServiceType = selectedServiceType.Value;
                }
            }
        }

        [XmlIgnore]
        public ServiceTypes SelectedServiceType { get; set; }

        /// <summary>
        ///   Required when:
        ///   Service='FIRST CLASS', 'FIRST CLASS COMMERCIAL', or 'FIRST CLASS HFP COMMERCIAL'
        /// </summary>
        public string FirstClassMailType {
            get {
                switch (SelectedServiceType) {
                    case ServiceTypes.FirstClass:
                    case ServiceTypes.FirstClassCommercial:
                    case ServiceTypes.FirstClassHfpCommercial:
                        return SelectedFirstClassMailType.ToDescription();
                }
                return null;
            }
            // ReSharper disable ValueParameterNotUsed
            set {
                /* Required for XML Serialization */
            }
            // ReSharper restore ValueParameterNotUsed
        }

        [XmlIgnore]
        public FirstClassMailTypes SelectedFirstClassMailType { get; set; }

        /// <summary>
        ///   ZIP code must be valid
        /// </summary>
        public string ZipOrigination { get; set; }

        private string _zipDestination;

        /// <summary>
        ///   ZIP code must be valid.
        /// </summary>
        public string ZipDestination {
            get { return _zipDestination; }
            set { _zipDestination = value.Substring(0, Math.Min(value.Length, 5)); }
        }

        /// <summary>
        ///   Value must be numeric. Package weight in ounces is computed by (16 * Pounds) + Ounces. Package weight cannot exceed 70 pounds
        /// </summary>
        public int Pounds { get; set; }

        /// <summary>
        ///   Value must be numeric. Package weight in ounces is computed by (16 * Pounds) + Ounces. Package weight cannot exceed 70 pounds (1120 ounces).
        /// </summary>
        public decimal Ounces { get; set; }

        /// <summary>
        ///   Use to specify special containers or container attributes that may affect postage; otherwise, leave blank.
        ///   RECTANGULAR or NONRECTANGULAR must be indicated when Size=LARGE
        /// </summary>
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

        /// <summary>
        ///   Defined as follows:
        ///   REGULAR: Package dimensions are 12" or less;
        ///   LARGE: Any package dimension is larger than 12".
        /// </summary>
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

        /// <summary>
        ///   Value must be numeric. Units are inches. Required when Size is LARGE.
        /// </summary>
        public decimal Width { get; set; }

        /// <summary>
        ///   Value must be numeric. Units are inches. Required when Size is LARGE
        /// </summary>
        public decimal Length { get; set; }

        /// <summary>
        ///   Value must be numeric. Units are inches. Required when Size is LARGE.
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        ///   Value must be numeric. Units are inches. Required when Size is LARGE, and Container is NONRECTANGULAR or VARIABLE/NULL.
        /// </summary>
        public decimal Girth {
            get { return 2*(Height + Width); }
            // ReSharper disable ValueParameterNotUsed
            set {
                /* Required for XML Serialization */
            }
            // ReSharper restore ValueParameterNotUsed
        }

        /// <summary>
        ///   Available when Revision='2'. 
        ///   Package value.  Used to determine availability and cost of extra services.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///   Available when Revision='2'.
        ///   Collect on delivery amount.  Used to determine availability and cost of extra services.
        /// </summary>
        public string AmountToCollect { get; set; }

        /// <summary>
        ///   Available when Revision='2'.
        ///   Groups the SpecialServices elements.  
        ///   Special Services prices and availability will not be returned when Service = "ALL" or "ONLINE"
        /// </summary>
        public SpecialServices SpecialServices { get; set; }

        /// <summary>
        ///   Available when Revision='2'.
        ///   Returns all mailing services available based on item shape.  When specified, value in Container is ignored. 
        ///   Available when: Service='ALL' or Service='ONLINE'
        /// </summary>
        public string SortBy { get; set; }

        ///<summary>
        ///  A machinable parcel is any piece that is (see Exhibit 3.3):
        ///
        ///  a. Not less than 6 inches long, 3 inches high, 1/4 inch thick, and 6 ounces in weight. (A mailpiece exactly 1/4 inch thick is subject to the 3 1/2-inch height minimum under 601.1.2.)
        ///  b. Not more than 34 inches long, or 17 inches high, or 17 inches thick, or 35 pounds in weight. For books, or other printed matter, the maximum weight is 25 pounds.
        /// 
        ///  Machinable is required when:
        ///  Service='FIRST CLASS' and (FirstClassMailType='LETTER' or FirstClassMailType='FLAT') 
        ///  Service='PARCEL POST' 
        ///  Service='ALL'
        ///  Service='ONLINE'
        ///</summary>
        public bool Machinable {
            get {
                bool meetsLength = Length >= 6 && Length <= 34;
                bool meetsHeight = Height >= 3 && Height <= 17;
                bool meetsWidth = Width >= 0.25M && Width <= 17;
                bool meetsWeight = Weight >= 0.375M && Weight <= 35;

                return meetsLength && meetsHeight && meetsWidth && meetsWeight;
            }
            // ReSharper disable ValueParameterNotUsed
            set {
                /* Required for XML Serialization */
            }
            // ReSharper restore ValueParameterNotUsed
        }

        /// <summary>
        ///   Include Dropoff Locations in Response if available. Requires "ShipDate" tag.
        /// </summary>
        public bool ReturnLocations { get; set; }

        private string _shipDate;

        /// <summary>
        ///   Date Package Will Be Mailed. Ship date may be today plus 0 to 3 days in advance. Enter the date in format: dd-mmm-yyyy, such as 14-Feb-2011.
        /// </summary>
        public string ShipDate {
            get {
                if (string.IsNullOrEmpty(_shipDate)) {
                    _shipDate = string.Format("{0:dd-MMM-yyyy}", DateTime.Now.Date);
                }
                return _shipDate;
            }
            set { _shipDate = value; }
        }

        [XmlIgnore]
        private decimal Weight {
            get { return Pounds + (Ounces/16); }
        }
    }
}