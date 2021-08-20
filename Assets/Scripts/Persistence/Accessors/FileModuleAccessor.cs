using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Persistence.Accessors
{
    public class FileModuleAccessor : IPersistenceModuleAccessor
    {
        private const string _filePrefix = ".dat";
        private string _moduleName;
        private PersistantModuleTypes _moduleType;

        private Dictionary<string, object> _allData;
        
        public FileModuleAccessor(PersistantModuleTypes moduleType)
        {
            _moduleName = Enum.GetName(typeof(PersistantModuleTypes), moduleType);
            _moduleType = moduleType;
        }
        
        public PersistantModuleTypes GetModuleType()
        {
            return _moduleType;
        }
        
        public T GetValue<T>(string key)
        {
            if (_allData.TryGetValue(key, out var value))
            {
                return (T) value;
            }

            return default(T);
        }

        public void PersistData<T>(string key, T data)
        {
            _allData ??= new Dictionary<string, object>();

            _allData[key] = data;
        }

        public void LoadModule()
        {
            _allData = new Dictionary<string, object>();

            if (!DirectoryExists()) return;
            
            using var stream = File.Open(GetPath(), FileMode.OpenOrCreate);

            if (stream.Length == 0) return;
            
            BinaryFormatter bin = new BinaryFormatter();
            try
            {
                _allData = (Dictionary<string, object>)bin.Deserialize(stream);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        
        public void CloseModule()
        {
            _allData ??= new Dictionary<string, object>();

            if (!DirectoryExists())
            {
                Directory.CreateDirectory(GetDirectoryPath());
            }

            using var stream = File.Open(GetPath(), FileMode.OpenOrCreate);
            
            BinaryFormatter bin = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            bin.Serialize(stream, _allData);
        }

        private string GetDirectoryPath()
        {
            return Application.persistentDataPath;
        }
        
        private bool DirectoryExists()
        {
            return Directory.Exists(GetDirectoryPath());
        }
        
        private string GetPath()
        {
            return GetDirectoryPath() + "\\" + _moduleName + _filePrefix;
        }
    }
}