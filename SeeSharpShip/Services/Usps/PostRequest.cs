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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using SeeSharpShip.Core;
using SeeSharpShip.Core.Extensions;

namespace SeeSharpShip.Services.Usps {
    public class PostRequest : IRequest
    {
        #region IRequest Members

        public string GetResponse(string requestUrl, string requestContents) {
            string requestHash = requestContents.ToSha1Hash();

            if (ResponseCache.ContainsKey(requestHash)) {
                return ResponseCache.Get(requestHash);
            }

            byte[] bytes = new ASCIIEncoding().GetBytes(requestContents);
            var webRequest = (HttpWebRequest) WebRequest.Create(requestUrl);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = bytes.Length;
            Stream requestStream = webRequest.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            WebResponse response = webRequest.GetResponse();

            string responseXml;
            using (Stream stream = response.GetResponseStream()) {
                if (stream == null) {
                    return String.Empty;
                }

                var reader = new StreamReader(stream);
                responseXml = reader.ReadToEnd();
            }

            responseXml = RemoveCommasFromDecimalValues(responseXml);

            ResponseCache.Add(requestHash, responseXml, 30);

            return responseXml;
        }

        #endregion

        /// <summary>
        ///   Removes commas invalidly returned in USPS's rate response for values that should pass validation for xs:decimal type.
        ///   See: http://www.w3.org/TR/xmlschema-2/#decimal
        /// </summary>
        private static string RemoveCommasFromDecimalValues(string responseXml) {
            var responseDoc = XElement.Parse(responseXml);

            foreach (var item in responseDoc.Descendants("Value").Where(v => v.Value.Contains(","))) {
                item.Value = item.Value.Replace(",", String.Empty);
            }

            foreach (var item in responseDoc.Descendants("ValueOfContents").Where(v => v.Value.Contains(","))) {
                item.Value = item.Value.Replace(",", String.Empty);
            }

            return responseDoc.ToString();
        }
    }
}