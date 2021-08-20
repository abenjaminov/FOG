using System;
using ScriptableObjects;
using ScriptableObjects.Channels;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class TestScene : MonoBehaviour
    {
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private SceneMeta _textScene;

        private void Start()
        {
            _locationsChannel.OnChangeLocationComplete(_textScene,null);
        }
    }
}