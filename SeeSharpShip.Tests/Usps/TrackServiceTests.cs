#region SeeSharpShip.Tests is Copyright (C) 2013-2013 Michael J. Sumerano.

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
using NUnit.Framework;
using SeeSharpShip.Model.Usps;
using SeeSharpShip.Services.Usps;
using SeeSharpShip.Tests.Properties;

namespace SeeSharpShip.Tests.Usps {
    [TestFixture]
    public class TrackServiceTests {
        private ITrackService _trackService;
        private static string _userId;

        [TestFixtureSetUp]
        public void SetUp()
        {
            //Uses test API URL by default.  Configure in app.config.
            _trackService = new TrackService(Settings.Default.UspsApiUrl, new PostRequest());

            _userId = Settings.Default.UspsUserId;

            if (string.IsNullOrEmpty(_userId)) {
                throw new Exception("You must set UspsUserId in app.config to run 'Explicit' integration tests");
            }
        }

        [Test]
        public void Get_InvalidRequest_ReturnsError()
        {
            var trackRequest = new TrackRequest {
                TrackId = new TrackId(),
                UserId = _userId
            };

            TrackResponse trackResponse = _trackService.Get(trackRequest);

            Assert.That(trackResponse.Error, Is.Not.Null);
        }

        [Test]
        public void Get_InvalidTrackingNumber_ReturnsNoRecordSummary()
        {
            var trackRequest = new TrackRequest {
                TrackId = new TrackId {Id = "EJ888888888US"},
                UserId = _userId
            };

            TrackResponse trackResponse = _trackService.Get(trackRequest);

            Assert.That(trackResponse.TrackInfo[0].TrackSummary, Contains.Substring("There is no record"));
        }

        [Test]
        public void Get_InvalidTrackingNumber_ReturnsTrackingInfoError()
        {
            var trackRequest = new TrackRequest {
                TrackId = new TrackId {Id = "12345"},
                UserId = _userId
            };

            TrackResponse trackResponse = _trackService.Get(trackRequest);

            Assert.That(trackResponse.TrackInfo[0].Error, Is.Not.Null);
        }

        [Test]
        [Ignore("Enable this test when you have a valid test tracking number")]
        public void Get_ValidTrackingNumber1_ReturnsTrackingInfo()
        {
            var trackRequest = new TrackRequest {
                TrackId = new TrackId {Id = "EJ958088694US"},
                UserId = _userId
            };

            TrackResponse trackResponse = _trackService.Get(trackRequest);

            Assert.That(trackResponse.TrackInfo[0].TrackDetail.Count, Is.GreaterThan(0));
        }
    }
}
