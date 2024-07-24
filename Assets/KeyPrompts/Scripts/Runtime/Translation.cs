using System.Collections.Generic;
using UnityEngine;

namespace KeyPrompts.Runtime
{
    public class Translation
    {
        public readonly Dictionary<KeyCode, Sprite> map;

        public Translation()
        {
            map = new();
        }
    }
}