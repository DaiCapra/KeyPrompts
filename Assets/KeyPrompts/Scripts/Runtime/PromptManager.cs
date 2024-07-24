using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace KeyPrompts.Runtime
{
    public class PromptManager
    {
        private readonly Dictionary<string, Sprite> _sprites;
        private readonly Dictionary<InputType, Translation> _translations;

        public PromptManager()
        {
            _sprites = new(StringComparer.OrdinalIgnoreCase);
            _translations = new();
        }

        public void Load()
        {
            LoadSprites("Prompts");
            LoadTranslation(InputType.KeyboardDark, "Translations/KeyboardDark");
            LoadTranslation(InputType.KeyboardLight, "Translations/KeyboardLight");
        }

        public Sprite GetSprite(KeyCode keyCode, InputType type = InputType.KeyboardDark)
        {
            if (!_translations.TryGetValue(type, out var translation))
            {
                Debug.LogError($"No translation found for: {type}");
                return null;
            }

            if (translation.map.TryGetValue(keyCode, out var sprite))
            {
                return sprite;
            }

            return null;
        }

        private void LoadTranslation(InputType type, string path)
        {
            var map = ReadJson(path);

            var translation = new Translation();
            foreach (var kv in map)
            {
                var keyCode = kv.Key;
                var spriteKey = kv.Value;

                if (_sprites.TryGetValue(spriteKey, out var sprite))
                {
                    translation.map[keyCode] = sprite;
                }
            }

            _translations[type] = translation;
        }

        private Dictionary<KeyCode, string> ReadJson(string path)
        {
            var map = new Dictionary<KeyCode, string>();

            var textAsset = Resources.Load(path) as TextAsset;
            if (textAsset == null)
            {
                Debug.LogError($"Cannot load text asset from path: {path}");
                return map;
            }

            try
            {
                var json = textAsset.text;
                map = JsonConvert.DeserializeObject<Dictionary<KeyCode, string>>(json);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            return map;
        }


        private void LoadSprites(string path)
        {
            var sprites = Resources.LoadAll<Sprite>(path);
            foreach (var sprite in sprites)
            {
                _sprites[sprite.name] = sprite;
            }
        }
    }
}