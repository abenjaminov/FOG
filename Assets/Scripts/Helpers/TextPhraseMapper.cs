using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.GameConfiguration;
using ScriptableObjects.Traits;
using UnityEngine;

namespace Helpers
{
    public class TextPhraseMapper : MonoBehaviour
    {
        [SerializeField] private PlayerTraits _platyerTraits;
        [SerializeField] private KeyboardConfiguration _keyboardConfiguration;
        
        private Dictionary<string, Func<string>> TextPhrases;
        
        public TextPhraseMapper()
        {
            TextPhrases = new Dictionary<string, Func<string>>()
            {
                {"{NAME}", GetPlayerName},
                {"{TRAITS_KEY}", () => GetKeyName(_keyboardConfiguration.OpenTraitsScreen)},
                {"{INV_KEY}", () => GetKeyName(_keyboardConfiguration.OpenInventoryScreen)},
                {"{PICKUP_KEY}", () => GetKeyName(_keyboardConfiguration.Pickup)}
            };    
        }

        public List<string> GetPhrases()
        {
            return TextPhrases.Keys.ToList();
        }

        public string GetPhraseReplacement(string phrase)
        {
            if (!TextPhrases.ContainsKey(phrase)) return "";
            
            return TextPhrases[phrase]();
        }
        
        public string GetPlayerName()
        {
            return _platyerTraits.Name;
        }

        public string GetKeyName(KeyCode keyCode)
        {
            return keyCode.ToString() + " Key";
        }
    }
}