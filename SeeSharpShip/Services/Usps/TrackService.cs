#region SeeSharpShip is Copyright (C) 2013-2013 Michael J. Sumerano.

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
using System.Linq;
using System.Xml.Linq;
using SeeSharpShip.Extensions;
using SeeSharpShip.Models.Usps;

namespace SeeSharpShip.Services.Usps {
    public class TrackService : ITrackService {
        private readonly string _apiUrl;
        private readonly IRequest _request;

        [Obsolete("Remove this constructor once an IoC container is implemented.  Consider what to do about API url as well.")]
        public TrackService() : this("http://production.shippingapis.com/ShippingAPI.dll", new PostRequest()) { }

        public TrackService(string apiUrl, IRequest request)
        {
            if (string.IsNullOrWhiteSpace(apiUrl)) {
                throw new ArgumentNullException("apiUrl");
            }

            _apiUrl = apiUrl;
            _request = request;
        }

        #region ITrackService Members

        public TrackResponse Get(TrackRequest request)
        {
            string response = DoRequest(request);
            var responseXml = XElement.Parse(response);
            var trackInfo = responseXml.Descendants("TrackInfo").ToList();

            if (trackInfo.Any()) {
                var trackInfoErrors = trackInfo.Elements("Error").ToList();

                if (trackInfoErrors.Any()) {
                    var trackResponse = new TrackResponse {TrackInfo = new List<ITrackInfo>()};

                    foreach (var error in trackInfoErrors) {
                        trackResponse.TrackInfo.Add(new TrackInfo {Error = error.ToString().ToObject<RequestError>()});
                    }

                    return trackResponse;
                }
            }

            return responseXml.Name == "Error"
                       ? new TrackResponse {Error = responseXml.ToString().ToObject<RequestError>()}
                       : response.ToObject<TrackResponse>();
        }

        #endregion

        private string DoRequest(TrackRequest request) { return _request.GetResponse(_apiUrl, string.Format("API=TrackV2&XML={0}", request.ToXmlString())); }
    }
}
