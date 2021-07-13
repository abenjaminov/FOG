using System;
using UnityEngine;

namespace Game
{
    public class GameLoader : MonoBehaviour
    {
        [Header("Scene Prefabs")]
        [SerializeField] private GameObject GUI;

        private void Awake()
        {
            Instantiate(GUI);
        }
    }
}