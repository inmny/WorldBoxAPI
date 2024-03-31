using UnityEngine;
using System.IO;
using System.Linq;

namespace WorldBoxAPI.Constants {
    internal class Paths {
        public static string NCMS { get; } = Directory.GetFiles($"{Application.streamingAssetsPath}/mods").First(x=>x.StartsWith("NCMS") && x.EndsWith(".dll"));
        public static string NML { get; } = Directory.GetFiles($"{Application.streamingAssetsPath}/mods").FirstOrDefault(file =>
                                           Path.GetFileName(file).StartsWith("NeoModLoader") &&
                                           Path.GetFileName(file).EndsWith(".dll") &&
                                           !Path.GetFileName(file).Contains("AutoUpdate")) ??
                                       $"{Application.streamingAssetsPath}/mods/NeoModLoader.dll";
    }
}