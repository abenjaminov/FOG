using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptableObjects.GameConfiguration
{
    [CreateAssetMenu(fileName = "Scenes List", menuName = "Game Configuration/Scenes")]
    public class ScenesList : ScriptableObject
    {
        [SerializeField] private List<SceneMeta> _scenes;

        public SceneMeta GetSceneMetaById(string sceneId)
        {
            return _scenes.FirstOrDefault(x => x.Id == sceneId);
        }
    }
}