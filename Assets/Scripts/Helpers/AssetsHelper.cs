using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public static class AssetsHelper
    {
        public static List<T> GetAllAssets<T>() where T : UnityEngine.Object
        {
            if (!Application.isEditor) return new List<T>();
            
            #if UNITY_EDITOR
            var assetGuids = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(T).Name}");

            var assets = new List<T>();

            foreach (var guid in assetGuids)
            {
                var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                
                var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
                
                assets.Add(asset);
            }

            return assets;
            #endif
            return null;
        }
    }
}