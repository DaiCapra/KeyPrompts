using System;
using System.Linq;
using KeyPrompts.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace KeyPrompts.Example
{
    public class Example : MonoBehaviour
    {
        public InputType type;
        public GameObject prefab;
        private readonly PromptManager _promptManager = new();

        public void Start()
        {
            _promptManager.Load();

            var keys = Enum.GetValues(typeof(KeyCode))
                .Cast<KeyCode>()
                .ToList();

            foreach (var key in keys)
            {
                var sprite = _promptManager.GetSprite(key, type: type);
                if (sprite != null)
                {
                    var instantiate = Instantiate(prefab, parent: prefab.transform.parent);
                    var image = instantiate.GetComponent<Image>();
                    image.sprite = sprite;
                }
            }

            prefab.gameObject.SetActive(false);
        }
    }
}