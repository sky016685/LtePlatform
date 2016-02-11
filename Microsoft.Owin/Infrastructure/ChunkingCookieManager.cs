﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Owin.Properties;

namespace Microsoft.Owin.Infrastructure
{
    /// <summary>
    /// This handles cookies that are limited by per cookie length. It breaks down long cookies for responses, and reassembles them
    /// from requests.
    /// </summary>
    public class ChunkingCookieManager : ICookieManager
    {
        public ChunkingCookieManager()
        {
            ChunkSize = 0xffa;
            ThrowForPartialCookies = true;
        }

        /// <summary>
        /// Appends a new response cookie to the Set-Cookie header. If the cookie is larger than the given size limit
        /// then it will be broken down into multiple cookies as follows:
        ///     Set-Cookie: CookieName=chunks:3; path=/
        ///     Set-Cookie: CookieNameC1=Segment1; path=/
        ///     Set-Cookie: CookieNameC2=Segment2; path=/
        ///     Set-Cookie: CookieNameC3=Segment3; path=/
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public void AppendResponseCookie(IOwinContext context, string key, string value, CookieOptions options)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            var flag = !string.IsNullOrEmpty(options.Domain);
            var flag2 = !string.IsNullOrEmpty(options.Path);
            var hasValue = options.Expires.HasValue;
            var str = Uri.EscapeDataString(key);
            var str2 = str + "=";
            var str3 = (!flag ? null : "; domain=") + (!flag ? null : options.Domain) + (!flag2 ? null : "; path=") +
                       (!flag2 ? null : options.Path) + (!hasValue ? null : "; expires=") +
                       (!hasValue
                           ? null
                           : (options.Expires.Value.ToString("ddd, dd-MMM-yyyy HH:mm:ss ", CultureInfo.InvariantCulture) +
                              "GMT")) + (!options.Secure ? null : "; secure") +
                       (!options.HttpOnly ? null : "; HttpOnly");
            value = value ?? string.Empty;
            var flag4 = false;
            if (IsQuoted(value))
            {
                flag4 = true;
                value = RemoveQuotes(value);
            }
            var str4 = Uri.EscapeDataString(value);
            var headers = context.Response.Headers;
            if (!ChunkSize.HasValue || (ChunkSize.Value > (((str2.Length + str4.Length) + str3.Length) + (flag4 ? 2 : 0))))
            {
                var str5 = str2 + (flag4 ? Quote(str4) : str4) + str3;
                headers.AppendValues(Constants.Headers.SetCookie, str5);
            }
            else
            {
                if (ChunkSize.Value < (((str2.Length + str3.Length) + (flag4 ? 2 : 0)) + 10))
                {
                    throw new InvalidOperationException(Resources.Exception_CookieLimitTooSmall);
                }
                var num = (((ChunkSize.Value - str2.Length) - str3.Length) - (flag4 ? 2 : 0)) - 3;
                var num2 = (int)Math.Ceiling((str4.Length * 1.0) / num);
                headers.AppendValues(Constants.Headers.SetCookie, str2 + "chunks:" + num2.ToString(CultureInfo.InvariantCulture) + str3);
                var values = new string[num2];
                var startIndex = 0;
                for (var i = 1; i <= num2; i++)
                {
                    var num5 = str4.Length - startIndex;
                    var length = Math.Min(num, num5);
                    var str6 = str4.Substring(startIndex, length);
                    startIndex += length;
                    values[i - 1] = str + "C" + i.ToString(CultureInfo.InvariantCulture) + "=" +
                                    (flag4 ? "\"" : string.Empty) + str6 + (flag4 ? "\"" : string.Empty) + str3;
                }
                headers.AppendValues(Constants.Headers.SetCookie, values);
            }
        }

        /// <summary>
        /// Deletes the cookie with the given key by setting an expired state. If a matching chunked cookie exists on
        /// the request, delete each chunk.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <param name="options"></param>
        public void DeleteCookie(IOwinContext context, string key, CookieOptions options)
        {
            Func<string, bool> rejectPredicate;
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            var str = Uri.EscapeDataString(key);
            var keys = new List<string> {
                str + "="
            };
            var str2 = context.Request.Cookies[key];
            var num = ParseChunksCount(str2);
            if (num > 0)
            {
                for (var j = 1; j <= (num + 1); j++)
                {
                    keys.Add((str + "C" + j.ToString(CultureInfo.InvariantCulture)) + "=");
                }
            }
            var flag = !string.IsNullOrEmpty(options.Domain);
            var flag2 = !string.IsNullOrEmpty(options.Path);
            Func<string, bool> predicate = value => keys.Any(k => value.StartsWith(k, StringComparison.OrdinalIgnoreCase));
            if (flag)
            {
                Func<string, bool> func =
                    value =>
                        predicate(value) &&
                        (value.IndexOf("domain=" + options.Domain, StringComparison.OrdinalIgnoreCase) != -1);
                rejectPredicate = func;
            }
            else if (flag2)
            {
                Func<string, bool> func2 =
                    value =>
                        predicate(value) &&
                        (value.IndexOf("path=" + options.Path, StringComparison.OrdinalIgnoreCase) != -1);
                rejectPredicate = func2;
            }
            else
            {
                Func<string, bool> func3 = value => predicate(value);
                rejectPredicate = func3;
            }
            var headers = context.Response.Headers;
            var values = headers.GetValues(Constants.Headers.SetCookie);
            if (values != null)
            {
                Func<string, bool> func4 = value => !rejectPredicate(value);
                headers.SetValues(Constants.Headers.SetCookie, values.Where(func4).ToArray());
            }
            var options3 = new CookieOptions
            {
                Path = options.Path,
                Domain = options.Domain,
                Expires = new DateTime(0x7b2, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            };
            AppendResponseCookie(context, key, string.Empty, options3);
            for (var i = 1; i <= num; i++)
            {
                var options2 = new CookieOptions
                {
                    Path = options.Path,
                    Domain = options.Domain,
                    Expires = new DateTime(0x7b2, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                };
                AppendResponseCookie(context, key + "C" + i.ToString(CultureInfo.InvariantCulture), string.Empty, options2);
            }
        }

        /// <summary>
        /// Get the reassembled cookie. Non chunked cookies are returned normally.
        /// Cookies with missing chunks just have their "chunks:XX" header returned.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <returns>The reassembled cookie, if any, or null.</returns>
        public string GetRequestCookie(IOwinContext context, string key)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var cookies = context.Request.Cookies;
            var str = cookies[key];
            var num = ParseChunksCount(str);
            if (num <= 0)
            {
                return str;
            }
            var flag = false;
            var strArray = new string[num];
            for (var i = 1; i <= num; i++)
            {
                var str2 = cookies[key + "C" + i.ToString(CultureInfo.InvariantCulture)];
                if (str2 == null)
                {
                    if (!ThrowForPartialCookies)
                    {
                        return str;
                    }
                    var num3 = 0;
                    for (var j = 0; j < (i - 1); j++)
                    {
                        num3 += strArray[j].Length;
                    }
                    throw new FormatException(string.Format(CultureInfo.CurrentCulture,
                        Resources.Exception_ImcompleteChunkedCookie, i - 1, num, num3));
                }
                if (IsQuoted(str2))
                {
                    flag = true;
                    str2 = RemoveQuotes(str2);
                }
                strArray[i - 1] = str2;
            }
            var str3 = string.Join(string.Empty, strArray);
            if (flag)
            {
                str3 = Quote(str3);
            }
            return str3;
        }

        private static bool IsQuoted(string value)
        {
            return (((value.Length >= 2) && (value[0] == '"')) && (value[value.Length - 1] == '"'));
        }

        private static int ParseChunksCount(string value)
        {
            int num;
            if (((value != null) && value.StartsWith("chunks:", StringComparison.Ordinal)) &&
                int.TryParse(value.Substring("chunks:".Length), NumberStyles.None, CultureInfo.InvariantCulture, out num))
            {
                return num;
            }
            return 0;
        }

        private static string Quote(string value)
        {
            return ('"' + value + '"');
        }

        private static string RemoveQuotes(string value)
        {
            return value.Substring(1, value.Length - 2);
        }

        /// <summary>
        /// The maximum size of cookie to send back to the client. If a cookie exceeds this size it will be broken down into multiple
        /// cookies.Set this value to null to disable this behavior.The default is 4090 characters, which is supported by all
        /// common browsers.
        ///
        /// Note that browsers may also have limits on the total size of all cookies per domain, and on the number of cookies per domain.
        /// </summary>
        public int? ChunkSize { get; set; }

        /// <summary>
        /// Throw if not all chunks of a cookie are available on a request for re-assembly.
        /// </summary>
        public bool ThrowForPartialCookies { get; set; }
    }
}