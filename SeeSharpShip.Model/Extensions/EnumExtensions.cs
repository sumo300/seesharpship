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
using System.ComponentModel;
using System.Linq;

namespace SeeSharpShip.Model.Extensions {
    public static class EnumExtensions {
        public static string ToDescription(this Enum value) {
            return (from m in value.GetType().GetMember(value.ToString())
                    let attr = (DescriptionAttribute) m.GetCustomAttributes(typeof (DescriptionAttribute), false).FirstOrDefault()
                    select attr == null ? value.ToString() : attr.Description).FirstOrDefault();
        }

        public static T? ToEnumSafe<T>(this string value) where T : struct {
            var type = typeof (T);

            if (!type.IsEnum) {
                throw new InvalidEnumArgumentException("Type T must be an Enum");
            }

            // Match enum value
            if (value.IsEnum<T>()) {
                return (T) Enum.Parse(typeof (T), value);
            }

            // Match description attribute
            foreach (var property in type.GetProperties()) {
                var attribute = Attribute.GetCustomAttribute(property, typeof (DescriptionAttribute)) as DescriptionAttribute;
                if ((attribute != null && attribute.Description == value) || property.Name == value) {
                    return (T) property.GetValue(type, null);
                }
            }

            return default(T);
        }

        public static bool IsEnum<T>(this string s) { return Enum.IsDefined(typeof (T), s); }
    }
}