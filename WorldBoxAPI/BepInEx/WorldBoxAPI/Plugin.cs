using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using WorldBoxAPI.Compatibility;
using WorldBoxAPI.Constants;
using WorldBoxAPI.Extensions;
using WorldBoxAPI.ResourceTools;

namespace WorldBoxAPI.BepInEx {
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    internal class Plugin : BaseUnityPlugin {
        public static new ManualLogSource Logger;
        private const string pluginGuid = "apexlite.worldbox.wbapi";
        private const string pluginName = "WorldBoxAPI";
        private const string pluginVersion = "0.1.1";
        private bool gameLoaded = false;

        void Awake() {
            Logger = base.Logger;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        void OnGameLoad() {
            if (Version.OUTDATED_VERSION) {
                Logger.LogError("Outdated version detected! Should be " + global::Config.versionCodeText + " is " + Version.TARGET_VERSION_CODE + ".");
            }

            LocaleImporter.LoadEmbededJson();
            ResourceImporter.LoadEmbededResources();
        }

        void Update() {
            if (global::Config.gameLoaded) {
                if (!gameLoaded) {
                    gameLoaded = true;
                    SendMessage("OnGameLoad");
                }

                if (!ModLoaders.NMLLoaded && ModLoaders.UsingNML) {;
                    if (GameObjects.NML != null && GameObjects.NML.GetComponent("WorldBoxMod").GetFieldValue<bool>("initialized_successfully")) {
                        ModLoaders.NMLLoaded = true;
                        SendMessage("OnNMLLoad");
                    }
                }

                if (!ModLoaders.NCMSLoaded && ModLoaders.UsingNCMS) {
                    if (GameObjects.NCMS != null && GameObjects.NCMS.GetComponent("WorldBoxMod").GetFieldValue<bool>("initialized")) {
                        ModLoaders.NCMSLoaded = true;
                        SendMessage("OnNCMSLoad");
                    }
                }

                if (!ModLoaders.ModsLoaded && (ModLoaders.NMLLoaded || !ModLoaders.UsingNML) && (ModLoaders.NCMSLoaded || !ModLoaders.UsingNCMS)) {
                    ModLoaders.ModsLoaded = true;
                    SendMessage("OnModsLoad");
                }
            }
        }
    }
}