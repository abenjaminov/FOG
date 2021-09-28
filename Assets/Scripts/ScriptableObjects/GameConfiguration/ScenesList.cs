using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptableObjects.GameConfiguration
{
    [CreateAssetMenu(fileName = "Scenes List", menuName = "Game Configuration/Scenes")]
    public class ScenesList : ScriptableObject
    {
        public SceneMeta DefaultFirstScene;
        public List<SceneMeta> Scenes;

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