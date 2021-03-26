using Modules.BattleModule.Stats.Models;
using UnityEditor;
using UnityEngine;

namespace Modules.BattleModule.Stats.Editor
{
    [CustomPropertyDrawer(typeof(PrimaryUpgrades))]
    public class PrimaryUpgradesDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var statProperty = property.FindPropertyRelative("Stat");
            var upgradeValue = property.FindPropertyRelative("UpgradeValue");

            var rect = new Rect(position)
            {
                width = position.width / 3.5f,
                height = 20
            };

            EditorGUI.PropertyField(rect, statProperty, GUIContent.none);

            rect.x += position.width / 3;
            rect.width = position.width * 2 / 3;
            
            EditorGUI.PropertyField(rect, upgradeValue, GUIContent.none);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 20;
        }
    }
}