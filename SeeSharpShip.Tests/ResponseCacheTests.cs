#region SeeSharpShip.Tests is Copyright (C) 2011-2011 Michael J. Sumerano.

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
using SeeSharpShip.Utilities;

namespace SeeSharpShip.Tests {
    [TestFixture]
    public class ResponseCacheTests {
        [Test]
        public void Add_KeyIsNotNullValueIsNotNull_AddsNonNullValue() {
            ResponseCache.Clear();
            ResponseCache.Add("test", "value");
            var item = ResponseCache.Get("test");
            Assert.That(item, Is.EqualTo("value"));
        }

        [Test]
        public void Add_KeyIsNotNullValueIsNull_AddsNullValue() {
            ResponseCache.Clear();
            ResponseCache.Add("test", null);
            var item = ResponseCache.Get("test");
            Assert.That(string.IsNullOrEmpty(item), Is.True);
        }

        [Test]
        public void Add_KeyIsNull_ThrowsArgumentNullException() { Assert.Throws(typeof (ArgumentNullException), () => ResponseCache.Add(null, null)); }
    }
}