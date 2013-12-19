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

namespace SeeSharpShip.Core {
    /// <summary>
    ///   A simple caching mechanism that uses an internal Dictionary.
    /// </summary>
    public static class ResponseCache {
        private static readonly Dictionary<string, CacheItem> Responses = new Dictionary<string, CacheItem>();

        /// <summary>
        /// Caches a value indefinitely.
        /// </summary>
        /// <param name = "key"></param>
        /// <param name = "value"></param>
        public static void Add(string key, string value) { Add(key, value, 0); }

        /// <summary>
        /// Caches a value with an expiry in seconds.
        /// </summary>
        /// <param name = "key"></param>
        /// <param name = "value"></param>
        /// <param name = "expires">in seconds</param>
        public static void Add(string key, string value, int expires) {
            if (string.IsNullOrEmpty(key)) {
                throw new ArgumentNullException("key");
            }

            lock (Responses) {
                Responses.Add(key, new CacheItem {Value = value, InsertedOn = DateTime.Now, ExpiresIn = expires});
            }
        }

        public static string Get(string key) {
            if (!Responses.ContainsKey(key)) {
                return null;
            }

            CacheItem response = Responses[key];

            if (response == null || IsExpired(key, response)) {
                return null;
            }

            return response.Value;
        }

        private static bool IsExpired(string key, CacheItem response) {
            if (response.ExpiresIn > 0 && (DateTime.Now - response.InsertedOn).Seconds > response.ExpiresIn) {
                Remove(key);
                return true;
            }
            return false;
        }

        public static bool ContainsKey(string key) {
            if (string.IsNullOrEmpty(key)) {
                throw new ArgumentNullException("key");
            }

            return Get(key) != null;
        }

        public static bool Remove(string key) {
            lock (Responses) {
                return Responses.Remove(key);
            }
        }

        public static void Clear() {
            lock (Responses) {
                Responses.Clear();
            }
        }
    }
}