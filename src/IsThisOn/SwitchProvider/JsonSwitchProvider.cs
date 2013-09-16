using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace IsThisOn
{
    /// <summary>
    /// An <see cref="ISwitchProvider"/> that reads in switch configuration from a 
    /// JSON endpoint
    /// </summary>
    public class JsonSwitchProvider : ISwitchProvider
    {
        private static Regex RELATIVE_URI = new Regex(
                @"^(~|\/)", 
                RegexOptions.IgnoreCase | RegexOptions.Compiled
            );

        /// <summary>
        /// Loads switches from the URI supplied in 
        /// <see cref="SwitchBoardConfig.Instance.ProviderData"/>
        /// </summary>
        /// <returns><see cref="ISwitch"/> objects for each defined switch</returns>
        public IEnumerable<ISwitch> GetSwitches()
        {
            var endpoint = this.GetUri();
            var response = this.DownloadJson(endpoint);
                        
            var switches = SimpleJson.DeserializeObject<Switches>(response);

            foreach (var featureSwitch in switches.features)
            {
                yield return SwitchFactory.CreateSwitch(
                        featureSwitch.name,
                        featureSwitch.type,
                        featureSwitch.value
                    );
            }
        }

        protected internal virtual Uri GetUri()
        {
            var path = SwitchBoardConfig.Instance.ProviderData.TrimStart('~');

            if (path.StartsWith("/"))
            {
                var url = HttpContext.Current.Request.Url;

                path = string.Format(
                        "{0}://{1}/{2}",
                        url.Scheme,
                        url.Authority,
                        path.TrimStart('/')
                    );
            }

            return new Uri(path);
        }

        protected internal virtual string DownloadJson(Uri endpoint)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Accept, "application/json");
                return client.DownloadString(endpoint);
            }
        }

        class Switches
        {
            public IEnumerable<Switch> features = null;
        }

        class Switch
        {
            public string name = null;
            public string type = null;
            public string value = null;
        }
    }
}
