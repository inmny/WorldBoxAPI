using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldBoxAPI.BepInEx;
using WorldBoxAPI.Constants;
using WorldBoxAPI.ResourceTools;

namespace WorldBoxAPI.Graphics {
    public class TabBuilder {
        private static TabPosition LastPosition { get; set; }
        private static Dictionary<TabPosition, int> TabCounts { get; set; }
        private List<ButtonBuilder> Buttons { get; set; }
        private Sprite Icon { get; set; }
        private string Id { get; set; }
        private Sprite Normal { get; set; }
        private TabPosition Position { get; set; }
        private Sprite Selected { get; set; }
        private string TitleKey { get; set; }

        static TabBuilder() {
            TabCounts = new Dictionary<TabPosition, int> {
                { TabPosition.Left, 0 },
                { TabPosition.Right, 0 },
            };
            LastPosition = TabPosition.Left;
        }

        /// <summary>
        /// Initialzes a new instance of the TabBuilder class using the specfied id.
        /// </summary>
        /// <param name="id"></param>
        public TabBuilder(string id) {
            Next(id);
            Normal = AtlasTool.GetSprite("tab_buttons", AtlasType.SpriteAtlasUI);
            Selected = AtlasTool.GetSprite("tab_buttons_selected", AtlasType.SpriteAtlasUI);
        }

        public TabBuilder AddButton(ButtonBuilder buttonBuilder, int section) {
            _ = buttonBuilder ?? throw new ArgumentNullException(nameof(buttonBuilder));

            buttonBuilder.SetSection(section);
            Buttons.Add(buttonBuilder);
            return this;
        }

        /// <summary>
        /// Add back tab button. Not implemented.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TabBuilder AddBackButton() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add time control buttons. Not implemented.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TabBuilder AddTimeButtons() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Builds the tab.
        /// </summary>
        /// <returns>The TabBuilder instance.</returns>
        public TabBuilder Build() {
            if (!LocalizedTextManager.instance.localizedText.ContainsKey(TitleKey)) {
                Plugin.Logger.LogWarning($"Localized text does not contain key \"{TitleKey}\"!");
            }

            // Create GameObjects
            GameObject tabObject = new GameObject($"Tab_{Id}");
            GameObject buttonObject = new GameObject($"Button_{Id}");
            GameObject iconObject = new GameObject("Icon");

            // Add components
            Image image = buttonObject.AddComponent<Image>();
            Button button = buttonObject.AddComponent<Button>();
            TipButton tipButton = buttonObject.AddComponent<TipButton>();
            Image icon = iconObject.AddComponent<Image>();
            buttonObject.AddComponent<ButtonSfx>();

            // Set parents
            tabObject.transform.SetParent(GameObjects.TABS.transform);
            buttonObject.transform.SetParent(GameObjects.TAB_BUTTONS.transform);
            iconObject.transform.SetParent(buttonObject.transform);

            // Resize and move
            buttonObject.GetComponent<RectTransform>().sizeDelta = UI.TAB_BUTTON_SIZE;
            iconObject.GetComponent<RectTransform>().sizeDelta = UI.TAB_BUTTON_ICON_SIZE;
            buttonObject.transform.localScale = Vector3.one;
            iconObject.transform.localScale = Vector3.one;
            buttonObject.transform.localPosition = Position == TabPosition.Left ?
                new Vector3(UI.TAB_BUTTONS_START.x + UI.TAB_BUTTON_SIZE.x * (TabCounts[Position] + 1), UI.TAB_BUTTONS_START.y) :
                new Vector3(-UI.TAB_BUTTONS_START.x - UI.TAB_BUTTON_SIZE.x * (TabCounts[Position] + 1), UI.TAB_BUTTONS_START.y);


            // Change images
            button.image = image;
            button.image.sprite = Normal;
            icon.sprite = Icon;

            // Add button to PowersTabController (so you can press tab to cycle through)
            PowerTabController.instance._buttons.Add(button);

            // Setup tip so it uses the custom key and has the hotkey in its description
            tipButton.text_description_2 = "hotkey_tip_tab_other";
            tipButton.textOnClick = TitleKey;
            tipButton.showOnClick = false;

            // Disable tab so PowersTab.OnEnable() does not run until we want it too
            tabObject.SetActive(false);

            // Add PowersTab component and setup default variables
            PowersTab powersTab = tabObject.AddComponent<PowersTab>();
            powersTab.image_normal = Normal;
            powersTab.image_selected = Selected;
            powersTab.mainTab = PowersTab.mainTabb;
            powersTab.powerButton = button;
            button.onClick.AddListener(() => { powersTab.showTab(powersTab.powerButton); });

            // Reactivate tab
            tabObject.SetActive(true);

            // Update static variables
            TabManager.Tabs.Add(Id, new TabManager(tabObject));
            TabCounts[Position]++;
            LastPosition = Position;

            // Rearrange tabs so they fit nicely
            if (TabCounts[TabPosition.Left] == TabCounts[TabPosition.Right]) {
                GameObjects.TAB_BUTTONS.transform.localPosition = UI.TAB_BUTTONS_POSITION;
            } else if (TabCounts[TabPosition.Left] > TabCounts[TabPosition.Right]) {
                GameObjects.TAB_BUTTONS.transform.localPosition += new Vector3(-UI.TAB_BUTTON_SIZE.x / 2, 0);
            } else if (TabCounts[TabPosition.Left] < TabCounts[TabPosition.Right]) {
                GameObjects.TAB_BUTTONS.transform.localPosition += new Vector3(UI.TAB_BUTTON_SIZE.x / 2, 0);
            }

            // Build buttons
            foreach (ButtonBuilder buttonBuilder in Buttons) {
                buttonBuilder.AddToTab(Id);
                buttonBuilder.Build();
            }

            // Return the instance
            return this;
        }

        /// <summary>
        /// Resets TabBuilder to create a new tab. Same as creating a new TabBuilder object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The TabBuilder instance.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public TabBuilder Next(string id) {
            _ = id ?? throw new ArgumentNullException(nameof(id));

            if (TabManager.Tabs.ContainsKey(id)) {
                throw new ArgumentException($"Tab with id \"{id}\" already exists.", nameof(id));
            }

            Id = id;
            TitleKey = $"tab_{Id}";
            Icon = Resources.Load<Sprite>("WorldBoxAPI/UI/Icons/IconTemp");
            Position = (TabPosition)(((int)LastPosition + 1) % Enum.GetNames(typeof(TabPosition)).Length);
            Buttons = new List<ButtonBuilder>();

            return this;
        }

        /// <summary>
        /// Set tab icon with path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The TabBuilder instance.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TabBuilder SetIcon(string path) {
            _ = path ?? throw new ArgumentNullException(nameof(path));
            Icon = Resources.Load<Sprite>(path);
            return this;
        }

        /// <summary>
        /// Set tab icon with sprite.
        /// </summary>
        /// <param name="icon"></param>
        /// <returns>The TabBuilder instance.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TabBuilder SetIcon(Sprite icon) {
            _ = icon ?? throw new ArgumentNullException(nameof(icon));
            Icon = icon;
            return this;
        }

        /// <summary>
        /// Forces the tabs position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>The TabBuilder instance.</returns>
        public TabBuilder SetPosition(TabPosition position) {
            if (!Enum.IsDefined(typeof(TabPosition), position)) {
                throw new ArgumentException($"Position \"{position}\" is not defined by TabPosition.", nameof(position));
            }

            Position = position;
            return this;
        }

        /// <summary>
        /// Set localization key for the tab's title.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The TabBuilder instance.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TabBuilder SetTitleKey(string key) {
            _ = key ?? throw new ArgumentNullException(nameof(key));

            TitleKey = key;
            return this;
        }
    }
}
