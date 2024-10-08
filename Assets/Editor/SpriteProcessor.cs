using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class SpriteProcessor : AssetPostprocessor
    {
        private void OnPreprocessTexture()
        {
            var importer = this.assetImporter as TextureImporter;
            if(importer == null)
                return;
            
            importer.textureType = TextureImporterType.Sprite;
            importer.spritePixelsPerUnit = 100;
            importer.filterMode = FilterMode.Point;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
        }
    }
}
