using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    public class NullStorageProvider : IStorageProvider
    {
        /// <summary>
        /// Does absolutely nothing...
        /// </summary>
        /// <param name="name">The name of the switch</param>
        /// <param name="state">Whether or not the switch is active</param>
        /// <param name="duration">The duration to store the state for</param>
        public void StoreState(string name, bool state, StorageDuration duration) { }

        /// <summary>
        /// Always returns null
        /// </summary>
        /// <param name="name">The name of the switch</param>
        /// <returns>null</returns>
        public bool? GetState(string name)
        {
            return null;
        }

        /// <summary>
        /// Does nothing...
        /// </summary>
        /// <param name="name">The name of the switch</param>
        public void RemoveState(string name) { }

        /// <summary>
        /// Does nothing
        /// </summary>
        public void ClearState() { }
    }
}
