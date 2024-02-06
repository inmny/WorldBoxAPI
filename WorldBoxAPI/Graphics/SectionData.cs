using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldBoxAPI.Constants;
using WorldBoxAPI.ResourceTools;

namespace WorldBoxAPI.Graphics {
    internal class SectionData {
        public List<GameObject> TopRow { get; set; }
        public List<GameObject> BottomRow { get; set; }
        public GameObject Line { get; set; }
        public float StartX { get; set; }

        public SectionData(GameObject tab) {
            TopRow = new List<GameObject>();
            BottomRow = new List<GameObject>();
            Line = new GameObject("LINE");

            Image image = Line.AddComponent<Image>();
            RectTransform transform = Line.GetComponent<RectTransform>();
            transform.sizeDelta = UI.UI_LINE_SIZE;
            transform.pivot = UI.UI_LINE_PIVOT;
            Line.transform.localScale = UI.UI_LINE_SCALE;
            Line.transform.SetParent(tab.transform);
            image.sprite = AtlasTool.GetSprite("ui_line", AtlasType.SpriteAtlasUI);
        }

        public ButtonRow GetBiggestRow() {
            return TopRow.Count > BottomRow.Count ? ButtonRow.Top : ButtonRow.Bottom;
        }

        public int GetRowCount(ButtonRow row) {
            switch (row) {
                case ButtonRow.Top:
                    return TopRow.Count;
                case ButtonRow.Bottom:
                    return BottomRow.Count;
                default: 
                    return -1;
            }
        }

        // Precondition: end < TopRow.Count || end < BottomRow.Count
        public float GetWidth(ButtonRow row, int start, int end) {
            float width = 0;

            for (int i = start; i < end; i++) {
                switch (row) {
                    case ButtonRow.Top:
                        width += TopRow[i].GetComponent<RectTransform>().rect.width;
                        break;
                    case ButtonRow.Bottom:
                        width += BottomRow[i].GetComponent<RectTransform>().rect.width;
                        break;
                }
            }

            return width;
        }
    }
}
