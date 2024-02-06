using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using WorldBoxAPI.BepInEx;
using WorldBoxAPI.Extensions;

namespace WorldBoxAPI.ResourceTools {
    public static class LocaleImporter {
        /// <summary>
        /// Detects and loads an embeded JSON file of a dictionary of string, string into game localization.
        /// </summary>
        public static void LoadEmbededJson() {
            LoadEmbededJson(PlayerConfig.detectLanguage());
        }

        /// <summary>
        /// Loads an embeded JSON file of a dictionary of string, string into game localization.
        /// </summary>
        /// <param name="locale"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public static void LoadEmbededJson(string locale) {
            _ = locale ?? throw new ArgumentNullException(nameof(locale));
            string json = Assembly.GetCallingAssembly().GetResourceText(locale);
            
            // Handle better in future
            if (string.IsNullOrEmpty(json)) {
                Plugin.Logger.LogWarning($"Failed to find embeded JSON file for locale \"{locale}\".");
                return;
            }

            LoadJson(json);
        }

        /// <summary>
        /// Loads a JSON string of a dictionary of string, string into game localization.
        /// </summary>
        /// <param name="json"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void LoadJson(string json) {
            _ = json ?? throw new ArgumentNullException(nameof(json));
            Dictionary<string, string> locale = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            foreach (string key in locale.Keys) {
                if (LocalizedTextManager.instance.localizedText.ContainsKey(key)) {
                    throw new ArgumentException($"Localized text with key \"{key}\" already exists.", nameof(json));
                }

                string value = locale[key];
                _ = value ?? throw new ArgumentException($"Value of key \"{key}\" can't be null.", nameof(json));

                LocalizedTextManager.instance.localizedText.Add(key, value);
            }
        }

        /// <summary>
        /// Loads a JSON file containing a dictionary of string, string into game localization.
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void LoadJsonFile(string path) {
            _ = path ?? throw new ArgumentNullException(nameof(path));

            if (!File.Exists(path)) {
                throw new FileNotFoundException($"File \"{path}\" was not found.");
            }

            if (Path.GetExtension(path) != ".json") {
                throw new ArgumentException($"Invaild file format \"{Path.GetExtension(path)}\". File extension must be .json.", nameof(path));
            }

            LoadJson(File.ReadAllText(path));
        }

        /// <summary>
        /// Loads a JSON file containing a dictionary of string from a url into game localization.
        /// </summary>
        /// <param name="url"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void LoadJsonUrl(string url) {
            _ = url ?? throw new ArgumentNullException(nameof(url));

            using (HttpClient client = new HttpClient()) {
                HttpResponseMessage response = client.GetAsync(url).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode) {
                    LoadJson(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                } else {
                    Plugin.Logger.LogError($"Failed to connect to url \"{url}\".");
                }
            }
        }
    }
}