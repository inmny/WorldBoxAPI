using System.Collections.Generic;
using UnityEngine;
using WorldBoxAPI.Constants;

namespace WorldBoxAPI.Graphics {
    internal class TabManager {
        public static Dictionary<string, TabManager> Tabs { get; private set; }
        public List<GameObject> Buttons { get; private set; }
        public List<SectionData> Sections { get; private set; }
        public GameObject Tab { get; private set; }

        static TabManager() {
            Tabs = new Dictionary<string, TabManager>();
        }

        public TabManager(GameObject tab) {
            Tab = tab;
            Buttons = new List<GameObject>();
            Sections = new List<SectionData>();
        }

        public void AddButton(GameObject button, int section, ButtonRow row) {
            while (section > Sections.Count) {
                Sections.Add(new SectionData(Tab));
            }

            switch (row) {
                case ButtonRow.Top:
                    Sections[section - 1].TopRow.Add(button);
                    break;
                case ButtonRow.Bottom:
                    Sections[section - 1].BottomRow.Add(button);
                    break;
            }

            button.transform.SetParent(Tab.transform);
            RecalculateContent();
        }

        public void RecalculateContent() {
            for (int i = 0; i < Sections.Count; i++) {
                if (i == 0) {
                    Sections[i].StartX = UI.TAB_START.x;
                } else {
                    ButtonRow biggestRow = Sections[i - 1].GetBiggestRow();
                    int biggestRowCount = Sections[i - 1].GetRowCount(biggestRow);
                    Sections[i].StartX = Sections[i - 1].StartX + UI.LINE_SPACING * 2 + Sections[i - 1].GetWidth(biggestRow, 0, biggestRowCount) + (biggestRowCount - 1) * UI.BUTTON_SPACING;
                    Sections[i].Line.transform.localPosition = new Vector3(Sections[i].StartX, UI.UI_LINE_POSITION.y);
                }
                
                for (int j = 0; j < Sections[i].TopRow.Count; j++) {
                    RectTransform rectTransform = Sections[i].TopRow[j].GetComponent<RectTransform>();
                    Sections[i].TopRow[j].transform.localPosition = new Vector2(Sections[i].StartX + UI.LINE_SPACING + rectTransform.rect.width / 2 + Sections[i].GetWidth(ButtonRow.Top, 0, j) + j * UI.BUTTON_SPACING, UI.TAB_START.y);
                }

                for (int j = 0; j < Sections[i].BottomRow.Count; j++) {
                    RectTransform rectTransform = Sections[i].BottomRow[j].GetComponent<RectTransform>();
                    Sections[i].BottomRow[j].transform.localPosition = new Vector2(Sections[i].StartX + UI.LINE_SPACING + rectTransform.rect.width / 2 + Sections[i].GetWidth(ButtonRow.Bottom, 0, j) + j * UI.BUTTON_SPACING, -UI.TAB_START.y);
                }
            }
        }
    }
}
