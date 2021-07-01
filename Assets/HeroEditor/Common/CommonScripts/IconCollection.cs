using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.HeroEditor.Common.CommonScripts
{
    /// <summary>
    /// Global object that automatically grabs all required images.
    /// </summary>
    [CreateAssetMenu(fileName = "IconCollection", menuName = "ScriptableObjects/IconCollection")]
    public class IconCollection : ScriptableObject
    {
        public List<Sprite> Backgrounds;

        public static Dictionary<string, IconCollection> Instances = new Dictionary<string, IconCollection>();
        public static IconCollection Active;

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            Instances = Resources.LoadAll<IconCollection>("").ToDictionary(i => i.Id, i => i);
        }

        public string Id;
        public List<Object> IconFolders;
        public List<ItemIcon> Icons;
        public Sprite DefaultItemIcon;

        /// <summary>
        /// Find item icon by ID.
        /// </summary>
        /// <param name="id">Id of a sprite/icon/item.</param>
        /// <returns></returns>
        public Sprite FindIcon(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var icons = Icons.Where(i => i.Id == id).ToList();

            switch (icons.Count)
            {
                case 0:
                    Debug.LogWarning("Icon not found: " + id);
                    return DefaultItemIcon;
                case 1:
                    return icons[0].Sprite;
                default:
                    icons.ForEach(i => Debug.LogWarning(i.Path));
                    throw new Exception($"{icons.Count} icons found for {id}.");
            }
        }

		#if UNITY_EDITOR

		public void Refresh()
        {
            Icons.Clear();

            foreach (var folder in IconFolders)
            {
                if (folder == null) continue;

                var root = AssetDatabase.GetAssetPath(folder);
                var files = Directory.GetFiles(root, "*.png", SearchOption.AllDirectories).ToList();

                foreach (var path in files.Select(i => i.Replace("\\", "/")))
                {
                    var match = Regex.Match(path, @"Assets\/HeroEditor\/(?<Edition>\w+)\/(.+?\/)*Icons\/WithoutBackground\/\w+\/(?<Type>\w+)\/(?<Collection>.+?)\/(.+\/)*(?<Name>.+?)\.png");

                    var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                    var edition = match.Groups["Edition"].Value;
                    var collection = match.Groups["Collection"].Value;
                    var type = match.Groups["Type"].Value;
                    var iconName = match.Groups["Name"].Value;
                    var icon = new ItemIcon(edition, collection, type, iconName, path, sprite);

                    if (Icons.Any(i => i.Path == icon.Path))
                    {
                        Debug.LogErrorFormat($"Duplicated icon: {icon.Path}");
                    }
                    else
                    {
                        Icons.Add(icon);
                    }
                }
            }

			Icons = Icons.OrderBy(i => i.Name).ToList();
            EditorUtility.SetDirty(this);
        }

        #endif
    }
}