#if DEBUG
using BepInEx;
using UnityEngine;
using WorldBoxAPI.Graphics;

namespace WorldBoxAPI.BepInEx {
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Main : BaseUnityPlugin {
        private const string pluginGuid = "apexlite.worldbox.wbapitest";
        private const string pluginName = "WorldBoxAPI Test Mod";
        private const string pluginVersion = "0.1.0";

        void OnGUI() {
            if (GUI.Button(new Rect(10, 10, 100, 25), "Test")) {
                new WindowBuilder("testing")
                    .SetTitleKey("test")
                    .Build();
                new TabBuilder("Test")
                    .AddButton(new ButtonBuilder("Test", ButtonStyle.Small).SetWindowId("testing").SetRow(ButtonRow.Top), 1)
                    .Build();
            }
        }
    }
}
#endif