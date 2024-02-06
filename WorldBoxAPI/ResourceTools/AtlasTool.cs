using UnityEngine;
using UnityEngine.U2D;

namespace WorldBoxAPI.ResourceTools {
    internal static class AtlasTool {
        private static SpriteAtlas SpriteAtlasWorld { get; set; }
        private static SpriteAtlas SpriteAtlasUI { get; set; }

        static AtlasTool() {
            SpriteAtlas[] atlases = Resources.FindObjectsOfTypeAll<SpriteAtlas>();
            SpriteAtlasWorld = atlases[0];
            SpriteAtlasUI = atlases[1];
        }

        public static Sprite GetSprite(string name, AtlasType type) {
            switch (type) {
                case AtlasType.SpriteAtlasWorld:
                    return SpriteAtlasWorld.GetSprite(name);
                case AtlasType.SpriteAtlasUI:
                    /*Sprite[] sprites = new Sprite[SpriteAtlasUI.spriteCount];
                    SpriteAtlasUI.GetSprites(sprites);

                    foreach (Sprite sprite in sprites) {
                        Debug.Log(sprite.name);
                    }*/

                    return SpriteAtlasUI.GetSprite(name);
                default:
                    return null;
            }
        }
    }
}
