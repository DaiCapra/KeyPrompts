using UnityEditor;

namespace Editor
{
    public class TextureImport : AssetPostprocessor
    {
        void OnPreprocessTexture()
        {
            if (assetImporter is not TextureImporter importer)
            {
                return;
            }

            if (importer.textureType == TextureImporterType.Default)
            {
                importer.textureType = TextureImporterType.Sprite;

                importer.spriteImportMode = SpriteImportMode.Single;
            }
        }
    }
}