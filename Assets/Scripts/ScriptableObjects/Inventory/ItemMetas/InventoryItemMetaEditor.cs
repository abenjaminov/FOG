using UnityEditor;
using UnityEngine;

namespace ScriptableObjects.Inventory.ItemMetas
{
#if UNITY_EDITOR
    [CustomEditor(typeof(InventoryItemMeta), true)]
    public class InventoryItemMetaEditor : Editor
    {
        private InventoryItemMeta _target;

        private void OnEnable()
        {
            _target = target as InventoryItemMeta;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EditorGUILayout.Space(50);
            
            var verticalGroup = EditorGUILayout.BeginVertical("Icons");
            
            var iconRect = EditorGUILayout.BeginHorizontal("Icon");
            
            GUILayout.Label("Item Icon");
         
            _target.ItemSprite =
                (Sprite)EditorGUILayout.ObjectField(_target.ItemSprite,typeof(Sprite), false, GUILayout.Width(65f), GUILayout.Height(65f));
            
            EditorGUILayout.EndHorizontal();
            
            var inventoryIconRect = EditorGUILayout.BeginHorizontal("Inventory Icon");
            
            GUILayout.Label("Inventory Icon");
            
            _target.InventoryItemSprite =
                (Sprite)EditorGUILayout.ObjectField(_target.InventoryItemSprite,typeof(Sprite), false, GUILayout.Width(65f), GUILayout.Height(65f));
            
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }
#endif
}