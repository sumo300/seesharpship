#region SeeSharpShip.Core is Copyright (C) 2011-2013 Michael J. Sumerano.

// This file is part of SeeSharpShip.Core.
// 
// SeeSharpShip.Core is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SeeSharpShip.Core is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with SeeSharpShip.Core.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace SeeSharpShip.Core.Extensions {
    public static class StringExtensions {
        public static string ToSha1Hash(this string value) {
            using (var provider = new SHA1CryptoServiceProvider()) {
                return Convert.ToBase64String(provider.ComputeHash(Encoding.ASCII.GetBytes(value)));
            }
        }

        public static string ToAbsHashCodeString(this string value) {
            return Math.Abs(value.GetHashCode()).ToString(CultureInfo.InvariantCulture);
        }
    }
}
