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

namespace SeeSharpShip.Services.Usps {
    public class ServiceInfo : IEquatable<ServiceInfo> {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string ServiceName { get; set; }

        #region IEquatable<ServiceInfo> Members

        public bool Equals(ServiceInfo other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return Equals(other.Id, Id) && Equals(other.FullName, FullName) && Equals(other.ServiceName, ServiceName);
        }

        #endregion

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            return obj.GetType() == typeof (ServiceInfo) && Equals((ServiceInfo) obj);
        }

        public override int GetHashCode() {
            unchecked {
                int result = (Id != null ? Id.GetHashCode() : 0);
                result = (result*397) ^ (FullName != null ? FullName.GetHashCode() : 0);
                result = (result*397) ^ (ServiceName != null ? ServiceName.GetHashCode() : 0);
                return result;
            }
        }
    }
}