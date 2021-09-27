using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using ScriptableObjects.GameConfiguration;
using ScriptableObjects.Traits;
using UnityEngine;

namespace Helpers
{
    public class TextPhraseMapper : MonoBehaviour
    {
        [SerializeField] private PlayerTraits _platyerTraits;
        [SerializeField] private KeyboardConfiguration _keyboardConfiguration;
        [SerializeField] private ScenesList _sceneList;
        [SerializeField] private EnemyList _enemyList;

        private Dictionary<string, Func<string>> TextPhrases;
        
        private void Awake()
        {
            TextPhrases = new Dictionary<string, Func<string>>()
            {
                {"{NAME}", GetPlayerName},
                {"{TRAITS_KEY}", () => GetKeyName(_keyboardConfiguration.OpenTraitsScreen)},
                {"{INV_KEY}", () => GetKeyName(_keyboardConfiguration.OpenInventoryScreen)},
                {"{PICKUP_KEY}", () => GetKeyName(_keyboardConfiguration.Pickup)}
            };
            
            // Add Maps
            foreach (var sceneMeta in _sceneList.Scenes)
            {
                TextPhrases.Add(sceneMeta.ReplacementPhrase, () => GetMapName(sceneMeta.ReplacementPhrase));
            }
            
            foreach (var enemyMeta in _enemyList.Enemies)
            {
                var phrase = enemyMeta.ReplacementPhrase;
                TextPhrases.Add(enemyMeta.ReplacementPhrase, () => GetEnemyName(phrase));
            }
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

        public string GetMapName(string mapPhrase)
        {
            var sceneMeta = _sceneList.GetSceneMetaByPhrase(mapPhrase);

            return sceneMeta == null ? "" : sceneMeta.AssetName;

        }
        
        public string GetEnemyName(string replacementPhrase)
        {
            var enemyMeta = _enemyList.GetEnemyMetaByPhrase(replacementPhrase);

            return enemyMeta == null ? "" : enemyMeta.AssetName;

        }
    }
}