using System;

namespace WorldBoxAPI.Compatability {
    [Serializable]
    internal class NCMod {
        public string name { get; set; }
        public string author { get; set; }
        public string version { get; set; }
        public string description { get; set; }
        public string iconPath { get; set; }
        public string path { get; set; }
        public int targetGameBuild { get; set; }
        public bool state { get; set; }
    }
}
