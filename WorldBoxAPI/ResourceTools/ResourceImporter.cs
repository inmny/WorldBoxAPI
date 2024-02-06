using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using WorldBoxAPI.Extensions;

namespace WorldBoxAPI.ResourceTools {
    public static class ResourceImporter {
        internal static Dictionary<string, UnityEngine.Object> Resources { get; private set; }

        static ResourceImporter() {
            Resources = new Dictionary<string, UnityEngine.Object>();
        }

        /// <summary>
        /// Loads all embeded resources using the default Paths.json file.
        /// </summary>
        public static void LoadEmbededResources() {
            LoadEmbededResources("Paths");
        }

        /// <summary>
        /// Loads all embeded resources using a custom json file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void LoadEmbededResources(string fileName) {
            _ = fileName ?? throw new ArgumentNullException(nameof(fileName));
            string json = Assembly.GetCallingAssembly().GetResourceText(fileName);
            _ = json ?? throw new FileNotFoundException($"Failed to find embeded JSON \"{fileName}\" file.");
            Dictionary<string, string> paths = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            foreach (string resource in paths.Keys) {
                byte[] bytes = Assembly.GetCallingAssembly().GetResourceBytes(Path.GetFileNameWithoutExtension(resource));

                switch (FileToResource(Path.GetExtension(resource))) {
                    case ResourceType.Sprite:
                        LoadSprite(bytes, AddSlash(paths[resource]) + Path.GetFileNameWithoutExtension(resource));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(fileName), $"File type \"{Path.GetExtension(resource)}\" is not supported.");
                }
            }
        }

        /// <summary>
        /// Loads a sprite into the game from a image file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="location"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void LoadSprite(string filePath, string resourcePath) {
            _ = filePath ?? throw new ArgumentNullException(nameof(filePath));
            _ = resourcePath ?? throw new ArgumentNullException(nameof(resourcePath));

            if (!File.Exists(filePath)) {
                throw new FileNotFoundException($"File \"{filePath}\" was not found.");
            }

            if (Path.GetExtension(filePath) != ".png" || Path.GetExtension(filePath) != ".jpg" || Path.GetExtension(filePath) != ".jpeg") {
                throw new ArgumentOutOfRangeException(nameof(filePath), $"File type {Path.GetExtension(filePath)} is not supported.");
            }

            LoadSprite(File.ReadAllBytes(filePath), AddSlash(resourcePath) + Path.GetFileNameWithoutExtension(filePath));
        }

        /// <summary>
        /// Loads a sprite into the game from bytes.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="path"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void LoadSprite(byte[] bytes, string path) {
            _ = bytes ?? throw new ArgumentNullException(nameof(bytes));
            _ = path ?? throw new ArgumentNullException(nameof(path));

            if (Resources.ContainsKey(path)) {
                throw new ArgumentException($"Resource at path \"{path}\" already exists.", nameof(path));
            }

            Texture2D texture2D = new Texture2D(1, 1);
            texture2D.filterMode = FilterMode.Point;

            if (!texture2D.LoadImage(bytes)) {
                throw new ArgumentException($"Unable to load image at path \"{path}\" from bytes.", nameof(bytes));
            }

            Resources.Add(RemoveSlash(path), Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 1f));
        }

        private static ResourceType FileToResource(string extension) {
            // todo: figure out which of these you can actually import into a unity game without the editor
            switch (extension) {
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".bmp":
                case ".tif":
                case ".tiff":
                case ".gif":
                case ".iff":
                case ".pict":
                case ".pic":
                case ".pct":
                case ".tga":
                case ".psd":
                case ".exr":
                case ".hdr":
                    return ResourceType.Sprite;
                case ".ogg":
                case ".aif":
                case ".aiff":
                case ".flac":
                case ".wav":
                case ".mp3":
                case ".mod":
                case ".it":
                case ".s3m":
                case ".xm":
                    return ResourceType.AudioClip;
                case ".prefab":
                    return ResourceType.Prefab;
                case "asmdef":
                    return ResourceType.AssemblyDefinition;
                case "asmref":
                    return ResourceType.AssemblyDefinitionReference;
                case ".compute":
                    return ResourceType.ComputeShader;
                case ".fbx":
                case ".mb":
                case ".ma":
                case ".max":
                case ".jas":
                case ".dae":
                case ".dxf":
                case ".obj":
                case ".c4d":
                case ".blend":
                case ".lxo":
                    return ResourceType.FBX;
                case ".astc":
                case ".dds":
                case ".ktx":
                case ".pvr":
                    return ResourceType.Texture;
                case ".po":
                    return ResourceType.PortableObject;
                case ".3ds":
                    return ResourceType.Mesh3DS;
                case ".json":
                    return ResourceType.PackageManifest;
                default: 
                    return ResourceType.None;
            }
        }

        private static string AddSlash(string path) {
            return path.EndsWith("/") ? path : path + "/";
        }

        private static string RemoveSlash(string path) {
            return path.EndsWith("/") ? path.Remove(path.Length - 1) : path;
        }
    }
}
