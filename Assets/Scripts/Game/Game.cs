using System;
using System.IO;
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
            Application.logMessageReceived += ApplicationOnlogMessageReceived;
        }

        private void OnDestroy()
        {
            _gameChannel.PlayGameEvent -= PlayGameEvent;
            Application.logMessageReceived -= ApplicationOnlogMessageReceived;
        }

        private void ApplicationOnlogMessageReceived(string condition, string stacktrace, LogType type)
        {
            if (Debug.isDebugBuild && (type == LogType.Exception || type == LogType.Warning))
            {
                Debug.Log(condition);
            }

            if (type != LogType.Exception) return;
            
            var directory = Application.persistentDataPath + "\\Logs\\";

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            var errorsFilePath = directory + "Errors.txt";
            var fileMode = File.Exists(errorsFilePath) ? FileMode.Append : FileMode.OpenOrCreate;
            
            using var stream = File.Open(errorsFilePath, fileMode);
            var writer = new StreamWriter(stream);

            var time = DateTime.Now;
            
            var text = "###########################\nDate:\t" + time.ToLongDateString() + " " + time.ToLongTimeString() + "\nError:\t";
            text += condition +"\nStackTrace:\t" + stacktrace + "Type:\t" + Enum.GetName(typeof(LogType), type);
            
            writer.WriteLine(text);
            writer.Close();
            stream.Close();
        }

        private void PlayGameEvent()
        {
            HomeScreen.SetActive(false);
            PlayGame.SetActive(true);
        }
    }
}