#if DEBUG
using BepInEx;
using UnityEngine;
using WorldBoxAPI.Graphics;

namespace WorldBoxAPI.BepInEx {
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Main : BaseUnityPlugin {
        private const string pluginGuid = "apexlite.worldbox.wbapitest";
        private const string pluginName = "WorldBoxAPI Test Mod";
        private const string pluginVersion = "0.1.1";

        void OnGUI() {
            if (GUI.Button(new Rect(10, 10, 100, 25), "Test")) {
                new WindowBuilder("testing")
                    .SetTitleKey("test")
                    .Build();
                new TabBuilder("Test")
                    .AddButton(new ButtonBuilder("Test1", ButtonStyle.Small).SetWindowId("testing").SetRow(ButtonRow.Top), 1)
                    .AddButton(new ButtonBuilder("Test2", ButtonStyle.Medium).SetWindowId("testing").SetRow(ButtonRow.Top), 1)
                    .AddButton(new ButtonBuilder("Test3", ButtonStyle.Long).SetWindowId("testing").SetRow(ButtonRow.Top), 1)
                    .AddButton(new ButtonBuilder("Test4", ButtonStyle.SpecialRed).SetWindowId("testing").SetRow(ButtonRow.Bottom), 1)
                    .AddButton(new ButtonBuilder("Test5", ButtonStyle.SpecialRedBorder).SetWindowId("testing").SetRow(ButtonRow.Bottom), 1)
                    .Build();
            }
        }
    }
}
#endif