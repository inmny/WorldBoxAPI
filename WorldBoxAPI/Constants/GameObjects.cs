using UnityEngine;

namespace WorldBoxAPI.Constants {
    internal static class GameObjects {
        public static GameObject NCMS { get; }
        public static GameObject NML { get; }
        public static GameObject TABS { get; }
        public static GameObject TAB_BUTTONS { get; }
        public static GameObject TAB_MAIN { get; }
        public static GameObject TAB_DRAWING { get; }
        public static GameObject TAB_KINGDOMS { get; }
        public static GameObject TAB_CREATURES { get; }
        public static GameObject TAB_NATURE { get; }
        public static GameObject TAB_BOMBS { get; }
        public static GameObject TAB_OTHER { get; }

        static GameObjects() {
            NCMS = GameObject.Find("/Services/ModLoader/NCMS");
            NML = GameObject.Find("/Services/ModLoader/NeoModLoader");
            TABS = GameObject.Find("CanvasBottom/BottomElements/BottomElementsMover/CanvasScrollView/Scroll View/Viewport/Content/buttons");
            TAB_BUTTONS = GameObject.Find("CanvasBottom/BottomElements/BottomElementsMover/TabsButtons");
            TAB_MAIN = GameObject.Find("CanvasBottom/BottomElements/BottomElementsMover/CanvasScrollView/Scroll View/Viewport/Content/buttons/Tab_Main");
            TAB_DRAWING = GameObject.Find("CanvasBottom/BottomElements/BottomElementsMover/CanvasScrollView/Scroll View/Viewport/Content/buttons/Tab_Drawing");
            TAB_KINGDOMS = GameObject.Find("CanvasBottom/BottomElements/BottomElementsMover/CanvasScrollView/Scroll View/Viewport/Content/buttons/Tab_Kingdoms");
            TAB_CREATURES = GameObject.Find("CanvasBottom/BottomElements/BottomElementsMover/CanvasScrollView/Scroll View/Viewport/Content/buttons/Tab_Creatures");
            TAB_NATURE = GameObject.Find("CanvasBottom/BottomElements/BottomElementsMover/CanvasScrollView/Scroll View/Viewport/Content/buttons/Tab_Nature");
            TAB_BOMBS = GameObject.Find("CanvasBottom/BottomElements/BottomElementsMover/CanvasScrollView/Scroll View/Viewport/Content/buttons/Tab_Bombs");
            TAB_OTHER = GameObject.Find("CanvasBottom/BottomElements/BottomElementsMover/CanvasScrollView/Scroll View/Viewport/Content/buttons/Tab_Other");
        }
    }
}
