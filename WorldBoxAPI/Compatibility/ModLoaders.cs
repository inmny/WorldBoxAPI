using System;
using System.IO;
using System.Reflection;
using WorldBoxAPI.Constants;

namespace WorldBoxAPI.Compatibility {
    public static class ModLoaders {
        public static bool NMLLoaded { get; internal set; }
        public static bool NCMSLoaded { get; internal set; }
        public static bool ModsLoaded { get; internal set; }
        public static bool UsingNCMS { get; private set; }
        public static bool UsingNML { get; private set; }
        public static Assembly NCMS { get; private set; }
        public static Assembly NML { get; private set; }

        static ModLoaders() {
            UsingNCMS = File.Exists(Paths.NCMS) && Config.experimentalMode;
            UsingNML = File.Exists(Paths.NML) && Config.experimentalMode;
            NCMS = UsingNCMS ? Assembly.LoadFrom(Paths.NCMS) : null;
            NML = UsingNML ? Assembly.LoadFrom(Paths.NML) : null;
        }

        /// <summary>
        /// Deactivate a mod from other mod loaders by name.
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="ArgumentNullException"></exception>
        internal static void DeactivateMod(string name) {
            throw new NotImplementedException();

            /*_ = name ?? throw new ArgumentNullException(nameof(name));

            if (UsingNCMS) {

            }

            if (UsingNML) {

            }*/
        }
    }
}
