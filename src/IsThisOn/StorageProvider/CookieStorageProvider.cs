using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace IsThisOn
{
    /// <summary>
    /// An implementation of <see cref="IStorageProvider"/> the maintains switch 
    /// persistence in cookies
    /// </summary>
    public class CookieStorageProvider : IStorageProvider
    {   
        protected internal virtual HttpCookieCollection IncomingCookies
        {
            get { return HttpContext.Current.Request.Cookies; }
        }

        protected internal virtual HttpCookieCollection OutgoingCookies
        {
            get { return HttpContext.Current.Response.Cookies; }
        }

        /// <summary>
        /// Stores the state of the specified switch
        /// </summary>
        /// <param name="name">The name of the switch</param>
        /// <param name="state">Whether or not the switch is active</param>
        /// <param name="duration">The duration to store the state for</param>
        public void StoreState(string name, bool state, StorageDuration duration)
        {
            var key = MakeKey(name);
            this.OutgoingCookies.Remove(key);

            if (duration != StorageDuration.None)
            {
                var cookie = new HttpCookie(key);
                cookie.Value = state.ToString();

                this.SetCookieExpiration(duration, cookie);
                this.OutgoingCookies.Add(cookie);
            }
        }

        /// <summary>
        /// Gets the state of the specified switch
        /// </summary>
        /// <param name="name">The name of the switch</param>
        /// <returns>The stored value if found, or null if not</returns>
        public bool? GetState(string name)
        {
            name = MakeKey(name);

            if (this.KeyDefinedSinceLastRequest(name))
            {
                return bool.Parse(this.OutgoingCookies[name].Value);
            }

            if (!this.IncomingCookies.AllKeys.Contains(name)) return null;

            return bool.Parse(this.IncomingCookies[name].Value);
        }

        /// <summary>
        /// Removes stored value for the specified switch
        /// </summary>
        /// <param name="name">The name of the switch</param>
        public void RemoveState(string name)
        {
            var key = MakeKey(name);
            this.OutgoingCookies.Remove(key);
            this.IncomingCookies.Remove(key);
        }

        /// <summary>
        /// Clears all switch values
        /// </summary>
        public void ClearState()
        {
            this.ClearSwitches(this.IncomingCookies);
            this.ClearSwitches(this.OutgoingCookies);
        }

        protected void ClearSwitches(HttpCookieCollection cookies)
        {
            foreach (var key in cookies.AllKeys)
            {
                if (Regex.IsMatch(key, @"^itofs_"))
                {
                    cookies.Remove(key);
                }
            }
        }

        protected static string MakeKey(string name)
        {
            name = string.Format(
                    "itofs_{0}",
                    Regex.Replace(name, @"\W", "_").ToLower()
                );

            return name;
        }

        protected bool KeyDefinedSinceLastRequest(string name)
        {
            return this.OutgoingCookies.AllKeys.Contains(name);
        }

        protected virtual void SetCookieExpiration(StorageDuration duration, HttpCookie cookie)
        {
            switch (duration)
            {
                case StorageDuration.Medium:
                    cookie.Expires = DateTime.Now.AddDays(1);
                    break;
                case StorageDuration.Long:
                    cookie.Expires = DateTime.Now.AddDays(14);
                    break;
                case StorageDuration.ForEver:
                    cookie.Expires = DateTime.Now.AddYears(1);
                    break;
            }
        }
    }
}
