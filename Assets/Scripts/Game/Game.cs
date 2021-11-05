using System;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameChannel _gameChannel;
        [SerializeField] private GameObject HomeScreen;
        [SerializeField] private GameObject PlayGame;

        private void Awake()
        {
            _gameChannel.PlayGameEvent += PlayGameEvent;
        }

        private void PlayGameEvent()
        {
            HomeScreen.SetActive(false);
            PlayGame.SetActive(true);
        }
    }
}