using HarmonyLib;
using System;
using WorldBoxAPI.ResourceTools;

namespace WorldBoxAPI.Patches {
    [HarmonyPatch]
    public class Resources {
        [HarmonyPatch(typeof(UnityEngine.Resources), "Load", typeof(string), typeof(Type))]
        [HarmonyPostfix]
        private static void Load(ref UnityEngine.Object __result, string path, Type systemTypeInstance) {
            if (__result == null && ResourceImporter.Resources.ContainsKey(path) && ResourceImporter.Resources[path].GetType() == systemTypeInstance) {
                __result = ResourceImporter.Resources[path];
            }
        }
    }
}
