using System;
using NUnit.Framework;
using SeeSharpShip.Models.Usps;
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

            if (string.IsNullOrEmpty(_userId))
            {
                throw new Exception("You must set UspsUserId in app.config to run 'Explicit' integration tests");
            }
        }

        [Test]
        public void Get_InvalidRequest_ReturnsError()
        {
            var trackRequest = new TrackRequest
            {
                TrackId = new TrackId(),
                UserId = _userId
            };

            TrackResponse trackResponse = _trackService.Get(trackRequest);

            Assert.That(trackResponse.Error, Is.Not.Null);
        }

        [Test]
        public void Get_InvalidTrackingNumber_ReturnsTrackingInfoError()
        {
            var trackRequest = new TrackRequest {
                TrackId = new TrackId{Id = "12345"},
                UserId = _userId
            };

            TrackResponse trackResponse = _trackService.Get(trackRequest);

            Assert.That(trackResponse.TrackInfo[0].Error, Is.Not.Null);
        }
    }
}