using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace IsThisOn
{
    /// <summary>
    /// An <see cref="ISwitchProvider"/> that reads in switch configuration from a 
    /// JSON endpoint
    /// </summary>
    public class JsonSwitchProvider : ISwitchProvider
    {
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
            return new Uri(SwitchBoardConfig.Instance.ProviderData);
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
            public IEnumerable<Switch> features;
        }

        class Switch
        {
            public string name;
            public string type;
            public string value;
        }
    }
}
