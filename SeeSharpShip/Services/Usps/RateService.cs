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
using System.Linq;
using System.Web;
using SeeSharpShip.Extensions;
using SeeSharpShip.Models.Usps;
using SeeSharpShip.Models.Usps.Domestic;
using SeeSharpShip.Models.Usps.Domestic.Request;
using SeeSharpShip.Models.Usps.Domestic.Response;
using SeeSharpShip.Models.Usps.International.Request;
using SeeSharpShip.Models.Usps.International.Response;

namespace SeeSharpShip.Services.Usps {
    public class RateService : IRateService {
        private readonly string _apiUrl;
        private readonly IRequest _request;

// ReSharper disable UnusedMember.Global
        [Obsolete("Remove this constructor once an IoC container is implemented.  Consider what to do about API url as well.")]
        public RateService() : this("http://production.shippingapis.com/ShippingAPI.dll", new PostRequest()) { }
// ReSharper restore UnusedMember.Global

        public RateService(string apiUrl, IRequest request) {
            if (string.IsNullOrWhiteSpace(apiUrl)) {
                throw new ArgumentNullException("apiUrl");
            }

            _apiUrl = apiUrl;
            _request = request;
        }

        #region IRateService Members

        public RateV4Response Get(RateV4Request request) {
            string response = DoRequest(request);

            return response.Contains("<Error>") && !response.Contains("<RateV4Response>")
                       ? new RateV4Response {Error = response.ToObject<RequestError>()}
                       : response.ToObject<RateV4Response>();
        }

        public IntlRateV2Response Get(IntlRateV2Request request) {
            string response = DoRequest(request);

            return response.Contains("<Error>") && !response.Contains("<IntlRateV2Response>")
                       ? new IntlRateV2Response {Error = response.ToObject<RequestError>()}
                       : response.ToObject<IntlRateV2Response>();
        }

        public IEnumerable<ServiceInfo> DomesticServices(string userId, string password, string zip) {
            var request = new RateV4Request {
                                                UserId = userId,
                                                Password = password,
                                                Packages = new List<DomesticPackage> {
                                                                                         new DomesticPackage {
                                                                                                                 Id = "1",
                                                                                                                 SelectedServiceType = ServiceTypes.All,
                                                                                                                 Pounds = 1,
                                                                                                                 ZipOrigination = zip,
                                                                                                                 ZipDestination = zip,
                                                                                                             }
                                                                                     }
                                            };

            string response = DoRequest(request);

            if (HasError(response)) {
                throw new Exception("Service Request Error\n\n" + response);
            }

            var rateResponse = response.ToObject<RateV4Response>();
            return (rateResponse.Packages.SelectMany(package => package.Postages, (package, postage) => new ServiceInfo {
                                                                                                                            Id = postage.ClassId,
                                                                                                                            FullName =
                                                                                                                                HttpUtility.HtmlDecode(
                                                                                                                                    postage.MailService),
                                                                                                                        })).Distinct();
        }

        public IEnumerable<ServiceInfo> InternationalServices(string userId, string password, string zip) {
            var request = new IntlRateV2Request {
                                                    UserId = userId,
                                                    Password = password,
                                                    Packages = new List<InternationalPackage> {
                                                                                                  new InternationalPackage {
                                                                                                                               Id = "1",
                                                                                                                               SelectedMailType = MailType.All,
                                                                                                                               Pounds = 1,
                                                                                                                               OriginZip = zip,
                                                                                                                               Country = "CANADA",
                                                                                                                               ValueOfContents = "1.00",
                                                                                                                           }
                                                                                              }
                                                };

            string response = DoRequest(request);

            if (HasError(response)) {
                throw new Exception("Service Request Error\n\n" + response);
            }

            var rateResponse = response.ToObject<IntlRateV2Response>();
            return (from package in rateResponse.Packages
                    from service in package.Services
                    select new ServiceInfo {Id = service.Id, FullName = HttpUtility.HtmlDecode(service.SvcDescription)}).Distinct();
        }

        #endregion

        private static bool HasError(string response) { return response.IndexOf("<Error>", StringComparison.InvariantCultureIgnoreCase) != -1; }

        private string DoRequest(IntlRateV2Request request) { return _request.GetResponse(_apiUrl, string.Format("API=IntlRateV2&XML={0}", request.ToXmlString())); }

        private string DoRequest(RateV4Request request) { return _request.GetResponse(_apiUrl, string.Format("API=RateV4&XML={0}", request.ToXmlString())); }
    }
}