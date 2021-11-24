using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;

namespace ScriptableObjects.GameConfiguration
{
    [CreateAssetMenu(fileName = "Scenes List", menuName = "Game Configuration/Scenes")]
    public class ScenesList : ScriptableObject
    {
        public SceneMeta DefaultFirstScene;
        public List<SceneMeta> Scenes;

        private void OnEnable()
        {
#if UNITY_EDITOR
            var sceneAssets = AssetsHelper.GetAllAssets<SceneMeta>();

            Scenes ??= new List<SceneMeta>();
            Scenes.Clear();

            UnityEditor.AssetDatabase.Refresh();
            
            foreach (var scene in sceneAssets)
            {
                if (string.IsNullOrEmpty(scene.Id))
                {
                    scene.Id = Guid.NewGuid().ToString();
                    scene.SceneName = scene.SceneAsset.name;
                    UnityEditor.EditorUtility.SetDirty(scene);
                }
                Scenes.Add(scene);
            }
            
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }

        public SceneMeta GetSceneMetaById(string sceneId)
        {
            return Scenes.FirstOrDefault(x => x.Id == sceneId);
        }

        public SceneMeta GetSceneMetaByPhrase(string phrase)
        {
            return Scenes.FirstOrDefault(x => x.ReplacementPhrase == phrase);
        }
    }
}