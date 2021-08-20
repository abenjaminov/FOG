using UnityEditor;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Scene Meta", menuName = "Game Configuration/Scene Meta")]
    public class SceneMeta : ScriptableObject
    {
        public SceneAsset SceneAsset;
        public string Id;
    }
}