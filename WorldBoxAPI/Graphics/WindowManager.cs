using System.Collections.Generic;

namespace WorldBoxAPI.Graphics {
    internal class WindowManager {
        public static Dictionary<string, TabManager> Windows { get; private set; }

        static WindowManager() {
            Windows = new Dictionary<string, TabManager>();
        }
    }
}
