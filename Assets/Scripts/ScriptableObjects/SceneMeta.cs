using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Scene Meta", menuName = "Game Configuration/Scene Meta")]
    public class SceneMeta : AssetMeta
    {
        public string Id;
        public int LevelAloud;
        public string SceneName;
        public SceneMeta RespawnScene;
        public AudioClip SceneAudio;

#if UNITY_EDITOR
        public UnityEditor.SceneAsset SceneAsset;
#endif
    }
}