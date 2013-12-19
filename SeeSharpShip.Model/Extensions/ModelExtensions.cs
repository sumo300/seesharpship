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

using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace SeeSharpShip.Model.Extensions {
    public static class ModelExtensions {
        public static string ToXmlString<T>(this T value) where T : new() {
            var serializer = new XmlSerializer(typeof (T), string.Empty);
            var settings = new XmlWriterSettings {Indent = false, NewLineHandling = NewLineHandling.None};

            using (var stream = new MemoryStream()) {
                XmlWriter writer = XmlWriter.Create(stream, settings);
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");
                serializer.Serialize(writer, value, namespaces);

                stream.Position = 0;
                var reader = new StreamReader(stream);
                string output = reader.ReadToEnd();
                var regEx = new Regex(@"<\?xml.*?\?>");
                return regEx.Replace(output, string.Empty).Trim();
            }
        }

        public static T ToObject<T>(this string value) where T : new() {
            var serializer = new XmlSerializer(typeof (T));
            using (var reader = new StringReader(value)) {
                var obj = serializer.Deserialize(reader);

                if (obj == null) {
                    return default(T);
                }

                return (T) obj;
            }
        }
    }
}