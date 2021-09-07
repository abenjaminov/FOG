using UnityEditor;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Scene Meta", menuName = "Game Configuration/Scene Meta")]
    public class SceneMeta : ScriptableObject
    {
        public string Id;
        public int LevelAloud;
        public SceneAsset SceneAsset;
    }
}