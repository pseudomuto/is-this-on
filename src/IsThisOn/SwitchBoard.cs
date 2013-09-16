using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsThisOn
{
    /// <summary>
    /// A class the manages the set of feature switches and queries their status
    /// </summary>
    public sealed class SwitchBoard
    {
        private static Lazy<ISwitchProvider> switchProvider =
            new Lazy<ISwitchProvider>(() =>
            {
                var type = Type.GetType(SwitchBoardConfig.Instance.Provider);
                return Activator.CreateInstance(type) as ISwitchProvider;
            }, true);

        private static Lazy<IStorageProvider> storageProvider =
            new Lazy<IStorageProvider>(() =>
            {
                var type = Type.GetType(SwitchBoardConfig.Instance.StorageProvider);
                return Activator.CreateInstance(type) as IStorageProvider;
            }, true);

        private static Lazy<IEnumerable<ISwitch>> switches =
            new Lazy<IEnumerable<ISwitch>>(() =>
            {
                return switchProvider.Value.GetSwitches();
            }, true);

        /// <summary>
        /// Gets the number of switches defined
        /// </summary>
        public static int SwitchCount
        {
            get { return switches.Value.Count(); }
        }

        /// <summary>
        /// Gets all of the defined switches
        /// </summary>
        /// <returns>An <code>IEnumerable</code> of all defined switches</returns>
        public static IEnumerable<ISwitch> GetSwitches()
        {
            return switches.Value;
        }

        /// <summary>
        /// Reloads the switches from the configured <see cref="ISwitchProvider"/>
        /// </summary>
        public static void ReloadSwitches()
        {
            switches = new Lazy<IEnumerable<ISwitch>>(() =>
            {
                return switchProvider.Value.GetSwitches();
            }, true);

            storageProvider.Value.ClearState();
        }

        /// <summary>
        /// Gets a value indicating whether a switch is on
        /// </summary>
        /// <param name="name">The name of the switch</param>
        /// <returns>True is the feature is found and on, otherwise false</returns>
        public static bool IsOn(string name)
        {
            var thisSwitch = switches.Value.FirstOrDefault(s => s.Name.Equals(
                    name, 
                    StringComparison.OrdinalIgnoreCase
                ));

            // stop here when not found
            if (thisSwitch == null) return false;

            if (thisSwitch.CacheDuration != StorageDuration.None)
            {
                var storedState = storageProvider.Value.GetState(name);
                if (storedState.HasValue) return storedState.Value;
            }

            var state = thisSwitch.IsActive();

            StoreSwitchState(thisSwitch, state);

            return state;
        }

        private static void StoreSwitchState(ISwitch thisSwitch, bool state)
        {
            if (thisSwitch.CacheDuration != StorageDuration.None)
            {
                storageProvider.Value.StoreState(
                        thisSwitch.Name,
                        state,
                        thisSwitch.CacheDuration
                    );
            }
        }        

        internal static void SetStorageProvider(IStorageProvider someStorageProvider)
        {
            storageProvider = new Lazy<IStorageProvider>(() => 
            {
                var type = Type.GetType(SwitchBoardConfig.Instance.StorageProvider);
                
                return someStorageProvider == null ?
                    Activator.CreateInstance(type) as IStorageProvider :
                    someStorageProvider;
            });

            // preload
            var value = storageProvider.Value;
        }
    }
}
