using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    /// <summary>
    /// An enumeration defining the duration of time switch results
    /// should be stored for before calling IsActive again
    /// </summary>
    public enum StorageDuration
    {
        None = 0,
        Short,
        Medium,
        Long,
        ForEver
    }

    /// <summary>
    /// An interface for storage providers. Implementers will store switch results 
    /// for the duration specified by the switche's <see cref="ISwitch.CacheDuration"/> property
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        /// Stores the state of the specified switch
        /// </summary>
        /// <param name="name">The name of the switch</param>
        /// <param name="state">Whether or not the switch is active</param>
        /// <param name="duration">The duration to store the state for</param>
        void StoreState(string name, bool state, StorageDuration duration);

        /// <summary>
        /// Gets the state of the specified switch
        /// </summary>
        /// <param name="name">The name of the switch</param>
        /// <returns>The stored value if found, or null if not</returns>
        bool? GetState(string name);

        /// <summary>
        /// Removes stored value for the specified switch
        /// </summary>
        /// <param name="name">The name of the switch</param>
        void RemoveState(string name);
        
        /// <summary>
        /// Clears all switch values
        /// </summary>
        void ClearState();
    }
}
