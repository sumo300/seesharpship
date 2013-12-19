#region SeeSharpShip.Tests is Copyright (C) 2011-2013 Michael J. Sumerano.

// This file is part of SeeSharpShip.Tests.
// 
// SeeSharpShip.Tests is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SeeSharpShip.Tests is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with SeeSharpShip.Tests.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SeeSharpShip.Model.Extensions;
using SeeSharpShip.Model.Usps;
using SeeSharpShip.Model.Usps.Domestic.Request;
using SeeSharpShip.Model.Usps.Domestic.Response;
using SeeSharpShip.Model.Usps.International.Request;
using SeeSharpShip.Model.Usps.International.Response;
using SeeSharpShip.Services.Usps;
using SeeSharpShip.Tests.Properties;

namespace SeeSharpShip.Tests.Usps {
    [TestFixture]
    public class RateServiceTests {
        private IRateService _rateService;

        private static string _userId;
        private static string _password;
        private static string _sourceZipCode;

        [TestFixtureSetUp]
        public void SetUp() {
            //Default hits production API URL
            //_rateService = new RateService();

            //Uses test API URL by default.  Configure in app.config.
            _rateService = new RateService(Settings.Default.UspsApiUrl, new PostRequest());

            _userId = Settings.Default.UspsUserId;
            _password = Settings.Default.UspsPassword;
            _sourceZipCode = Settings.Default.UspsSourceZip;

            if (string.IsNullOrEmpty(_userId)) {
                throw new Exception("You must set UspsUserId in app.config to run 'Explicit' integration tests");
            }

            if (string.IsNullOrEmpty(_password)) {
                throw new Exception("You must set UspsPassword in app.config to run 'Explicit' integration tests");
            }

            if (string.IsNullOrEmpty(_sourceZipCode)) {
                throw new Exception("You must set UspsSourceZip in app.config to run 'Explicit' integration tests");
            }
        }

        [Test]
        [Category("Domestic")]
        [Explicit("Integration test that hits the real API")]
        public void DomesticServices_ValidCredentials_ReturnsDistinctServices() {
            List<ServiceInfo> response = _rateService.DomesticServices(_userId, _password, _sourceZipCode).ToList();
            Assert.That(response.Count(), Is.EqualTo(response.Distinct().Count()));
        }

        [Test]
        [Category("Domestic")]
        [Explicit("Integration test that hits the real API")]
        public void DomesticServices_ValidCredentials_ReturnsListOfServices() {
            List<ServiceInfo> response = _rateService.DomesticServices(_userId, _password, _sourceZipCode).ToList();
            Assert.That(response.Count, Is.GreaterThan(0));
        }

        /// <summary>
        ///     Tests fix for defect #7 introduced by USPS API changes
        /// </summary>
        [Test]
        [Category("Domestic")]
        [Explicit("Integration test that hits the real API")]
        public void DomesticServices_ValidCredentials_ReturnsListOfServicesContainingEncodedTrademark() {
            List<ServiceInfo> response = _rateService.DomesticServices(_userId, _password, _sourceZipCode).ToList();
            bool containsEncodedTrademark = response.Any(r => r.FullName.Contains("<sup>™</sup>"));
            Assert.That(containsEncodedTrademark, Is.Not.True);
        }

        [Test]
        [Category("Domestic")]
        [Explicit("Integration test that hits the real API")]
        public void Get_DomesticOverweightFirstClass_ReturnsPackageError() {
            string request =
                string.Format(
                    "<RateV4Request PASSWORD=\"{1}\" USERID=\"{0}\"><Revision>2</Revision><Package ID=\"539720aa4cff99f7\"><Service>First Class</Service><FirstClassMailType>Letter</FirstClassMailType><ZipOrigination>18507</ZipOrigination><ZipDestination>18518</ZipDestination><Pounds>10</Pounds><Ounces>0</Ounces><Container>RECTANGULAR</Container><Size>LARGE</Size><Width>15</Width><Length>15</Length><Height>20</Height><Girth>70</Girth><Value>249.95</Value><Machinable>false</Machinable><ReturnLocations>false</ReturnLocations></Package></RateV4Request>",
                    _userId, _password);
            RateV4Response response = _rateService.Get(request.ToObject<RateV4Request>());
            Assert.That(response.Packages[0].Error, Is.Not.Null);
        }

        [Test]
        [Category("Domestic")]
        [Explicit("Integration test that hits the real API")]
        public void Get_DomesticWithZipCodeLessThanFiveCharacters_ReturnsPackageError() {
            RateV4Response response = _rateService.Get(RateServiceTestsData.GetDomesticRequestWithZipDestinationLessThan5CharactersLong());
            Assert.That(response.Packages[0].Error, Is.Not.Null);
        }

        [Test]
        [Category("Domestic")]
        [Explicit("Integration test that hits the real API")]
        public void Get_DomesticWithZipCodeLongerThanFiveDigits_RateV4ResponseWithNoErrors() {
            string request =
                string.Format(
                    "<RateV4Request PASSWORD=\"{1}\" USERID=\"{0}\"><Revision>2</Revision><Package ID=\"2022599e4dfb4687\"><Service>Priority</Service><ZipOrigination>18507</ZipOrigination><ZipDestination>27513-8629</ZipDestination><Pounds>2</Pounds><Ounces>0</Ounces><Container /><Size>REGULAR</Size><Width>1</Width><Length>1</Length><Height>1</Height><Girth>4</Girth><Value>42.95</Value><SpecialServices><SpecialService>1</SpecialService></SpecialServices><Machinable>false</Machinable><ReturnLocations>false</ReturnLocations></Package></RateV4Request>",
                    _userId, _password);
            RateV4Response response = _rateService.Get(request.ToObject<RateV4Request>());
            Assert.That(response.Error, Is.Null);
            Assert.That(response.Packages[0].Error, Is.Null);
        }

        [Test]
        [Category("Domestic")]
        [Explicit("Integration test that hits the real API")]
        public void Get_DomesticWithZipCodeNull_ThrowsNullReferenceException() {
            Assert.Throws(typeof (NullReferenceException), () => _rateService.Get(RateServiceTestsData.GetDomesticRequestWithZipDestinationNull()));
        }

        [Test]
        [Category("International")]
        [Explicit("Integration test that hits the real API")]
        public void Get_InternationalValueOfContentsOver1000_IntlRateV2Response() {
            string request =
                string.Format(
                    "<IntlRateV2Request PASSWORD=\"{1}\" USERID=\"{0}\"><Revision>2</Revision><Package ID=\"2fcd64731e0ff32b\"><Pounds>30</Pounds><Ounces>0</Ounces><Machinable>false</Machinable><MailType>Package</MailType><ValueOfContents>1550.00</ValueOfContents><Country>Canada</Country><Container /><Size>REGULAR</Size><Width>1</Width><Length>1</Length><Height>1</Height><Girth>4</Girth><OriginZip>18507</OriginZip><ExtraServices><ExtraService>1</ExtraService></ExtraServices></Package></IntlRateV2Request>",
                    _userId, _password);
            IntlRateV2Response response = _rateService.Get(request.ToObject<IntlRateV2Request>());
            Assert.That(response, Is.InstanceOf(typeof (IntlRateV2Response)));
        }

        [Test]
        [Category("International")]
        [Explicit("Integration test that hits the real API")]
        public void Get_IntlRateV2Request_IntlRateV4Response() {
            IntlRateV2Response response = _rateService.Get(RateServiceTestsData.GetInternationalRequest());
            Assert.That(response, Is.InstanceOf<IntlRateV2Response>());
        }

        [Test]
        [Category("International")]
        [Explicit("Integration test that hits the real API")]
        public void Get_InvalidIntlRateV2Request_RequestError() {
            IntlRateV2Response response = _rateService.Get(new IntlRateV2Request {UserId = "kjzsdjbh fgkjashdf jhasdf"});

            Assert.That(response.Error, Is.InstanceOf<RequestError>());
        }

        [Test]
        [Category("Domestic")]
        [Explicit("Integration test that hits the real API")]
        public void Get_InvalidRateV4Request_RequestError() {
            RateV4Response response = _rateService.Get(new RateV4Request {UserId = "kjzsdjbh fgkjashdf jhasdf"});
            Assert.That(response.Error, Is.InstanceOf<RequestError>());
        }

        [Test]
        [Category("Domestic")]
        [Explicit("Integration test that hits the real API")]
        public void Get_RateV4Request_RateV4Response() {
            RateV4Response response = _rateService.Get(RateServiceTestsData.GetDomesticRequest());
            Assert.That(response, Is.InstanceOf<RateV4Response>());
        }

        [Test]
        [Category("International")]
        [Explicit("Integration test that hits the real API")]
        public void InternationalServices_ValidCredentials_ReturnsDistinctServices() {
            IList<ServiceInfo> response = _rateService.InternationalServices(_userId, _password, _sourceZipCode).ToList();
            Assert.That(response.Count, Is.EqualTo(response.Distinct().Count()));
        }

        [Test]
        [Category("International")]
        [Explicit("Integration test that hits the real API")]
        public void InternationalServices_ValidCredentials_ReturnsListOfServices() {
            List<ServiceInfo> response = _rateService.InternationalServices(_userId, _password, _sourceZipCode).ToList();
            Assert.That(response.Count, Is.GreaterThan(0));
        }

        /// <summary>
        ///     Tests fix for defect #7 introduced by USPS API changes
        /// </summary>
        [Test]
        [Category("International")]
        [Explicit("Integration test that hits the real API")]
        public void InternationalServices_ValidCredentials_ReturnsListOfServicesContainingEncodedTrademark() {
            List<ServiceInfo> response = _rateService.InternationalServices(_userId, _password, _sourceZipCode).ToList();
            bool containsEncodedTrademark = response.Any(r => r.FullName.Contains("<sup>™</sup>"));
            Assert.That(containsEncodedTrademark, Is.Not.True);
        }
    }
}
