using BepInEx;
using BepInEx.Bootstrap;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WorldBoxAPI.Extensions;

namespace WorldBoxAPI.Compatibility {
    [Serializable]
    public class ModInfo {
        public string Author { get; private set; }
        public string Description { get; private set; }
        public string GUID { get; private set; }
        public string IconPath { get; private set; }
        public string ModLoader { get; private set; }
        public string Name { get; private set; }
        public string Version { get; private set; }

        /// <summary>
        /// Finds every currently active mod.
        /// </summary>
        /// <returns>A list of every currently active mod.</returns>
        public static List<ModInfo> GetActiveMods() {
            List<ModInfo> modList = new List<ModInfo>();

            foreach (PluginInfo plugin in Chainloader.PluginInfos.Values) {
                BepInPlugin metaData = plugin.Metadata;
                modList.Add(new ModInfo() {
                    Name = metaData.Name,
                    GUID = metaData.GUID,
                    Version = metaData.Version.ToString(),
                    ModLoader = "BepInEx"
                });
            }

            if (ModLoaders.UsingNML) {
                IList mods = ModLoaders.NML.GetClass("NeoModLoader.WorldBoxMod").GetFieldValue<IList>("LoadedMods");

                foreach (object obj in mods) {
                    ModDeclare declaration = JsonConvert.DeserializeObject<ModDeclare>(JsonConvert.SerializeObject(obj.CallMethod("GetDeclaration")));

                    modList.Add(new ModInfo() {
                        Name = declaration.Name,
                        Author = declaration.Author,
                        GUID = declaration.UID,
                        IconPath = declaration.IconPath,
                        Version = declaration.Version,
                        Description = declaration.Description,
                        ModLoader = declaration.ModType == 0 ? declaration.IsNCMSMod ? "NCMS" : "NML" : "BepInEx"
                    });
                }
            }

            if (ModLoaders.UsingNCMS) {
                IList mods = ModLoaders.NCMS.GetClass("NCMS.ModLoader").GetFieldValue<IList>("Mods");

                foreach (object obj in mods) {
                    NCMod mod = JsonConvert.DeserializeObject<NCMod>(JsonConvert.SerializeObject(obj));

                    if (mod.state) {
                        modList.Add(new ModInfo() {
                            Name = mod.name,
                            Author = mod.author,
                            IconPath = mod.iconPath,
                            Version = mod.version,
                            Description = mod.description,
                            ModLoader = "NCMS"
                        });
                    }
                }
            }

            return modList;
        }

        public override string ToString() {
            return new StringBuilder($"Author: {Author},")
                .AppendLine($"Description: {Description},")
                .AppendLine($"GUID: {GUID},")
                .AppendLine($"IconPath: {IconPath},")
                .AppendLine($"ModLoader: {ModLoader},")
                .AppendLine($"Name: {Name},")
                .AppendLine($"Version: {Version}").ToString();
        }
    }
}
