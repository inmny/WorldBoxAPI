using System;
using UnityEngine;

namespace WorldBoxAPI.Graphics {
    public class WindowBuilder {
        private string Id { get; set; }
        private string TitleKey { get; set; }

        /// <summary>
        /// Initialzes a new instance of the WindowBuilder class using the specfied id.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
        public WindowBuilder(string id) {
            if (ScrollWindow.allWindows.ContainsKey(id)) {
                throw new ArgumentException($"Window with same id \"{id}\" already exists.", nameof(id));
            }

            Next(id);
        }

        /// <summary>
        /// Build the window.
        /// </summary>
        /// <returns>The WindowBuilder instance.</returns>
        public WindowBuilder Build() {
            ScrollWindow scrollWindow = GameObject.Instantiate(Resources.Load<ScrollWindow>("windows/empty"), CanvasMain.instance.transformWindows);
            scrollWindow.screen_id = Id;
            scrollWindow.name = Id;

            LocalizedText localizedText = scrollWindow.titleText.GetComponent<LocalizedText>();
            localizedText.key = TitleKey;
            LocalizedTextManager.instance.texts.Add(localizedText);
            scrollWindow.create(true);
            ScrollWindow.allWindows.Add(Id, scrollWindow);

            return this;
        }

        /// <summary>
        /// Resets WindowBuilder to create a new window. Same as creating a new WindowBuilder object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The WindowBuilder instance.</returns>
        /// <exception cref="ArgumentException"></exception>
        public WindowBuilder Next(string id) {
            if (ScrollWindow.allWindows.ContainsKey(id)) {
                throw new ArgumentException($"Window with same id \"{id}\" already exists.", nameof(id));
            }

            Id = id;
            return this;
        }

        /// <summary>
        /// Set localization key for the windows's title.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The WindowBuilder instance.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public WindowBuilder SetTitleKey(string key) {
            _ = key ?? throw new ArgumentNullException(nameof(key));

            TitleKey = key;
            return this;
        }
    }
}
