using Newtonsoft.Json;
using System;

namespace WorldBoxAPI.Compatibility {
    [Serializable]
    internal class ModDeclare {
        [JsonProperty("name")]
        public string Name { get; set; }
        public string UID { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        public string RepoUrl { get; set; }
        public string[] Dependencies { get; set; }
        public string[] OptionalDependencies { get; set; }
        public string[] IncompatibleWith { get; set; }
        public string FolderPath { get; set; }
        [JsonProperty("targetGameBuild")]
        public int TargetGameBuild { get; set; }
        [JsonProperty("iconPath")]
        public string IconPath { get; set; }
        public int ModType { get; set; }
        public bool IsNCMSMod { get; set; }
    }
}
