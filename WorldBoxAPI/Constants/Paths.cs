using UnityEngine;

namespace WorldBoxAPI.Constants {
    internal class Paths {
        public static string NCMS { get; } = $"{Application.streamingAssetsPath}/mods/NCMS_memload.dll";
        public static string NML { get; } = $"{Application.streamingAssetsPath}/mods/NeoModLoader.dll";
    }
}