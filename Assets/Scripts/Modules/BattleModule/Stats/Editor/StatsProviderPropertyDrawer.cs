using UnityEditor;
using UnityEngine;

namespace Modules.BattleModule.Stats.Editor
{
    public abstract class StatsProviderPropertyDrawer : PropertyDrawer
    {
        private const int _labelHeight = 30;
        private const int _statFieldHeight = 20;
        private const int _heightOffset = 2;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            DrawLabel(ref position, StatLabelName);
            DrawArray(StatNames, StatsPropertyName, property, ref position);
            EditorGUI.EndProperty();
        }

        protected abstract string StatLabelName { get; }
        protected abstract string StatsPropertyName { get; }
        protected abstract string[] StatNames { get; }

        private static void DrawArray(string[] namesArray, string propertyName, SerializedProperty property,
            ref Rect position)
        {
            var mainStats = namesArray;
            var mainStatsProperty = property.FindPropertyRelative(propertyName);
            
            var statRect = new Rect(position)
            {
                height = _statFieldHeight
            };

            mainStatsProperty.arraySize = mainStats.Length;

            for (var i = 0; i < mainStatsProperty.arraySize; i++)
            {
                var stat = mainStatsProperty.GetArrayElementAtIndex(i);
                EditorGUI.PropertyField(statRect, stat, new GUIContent(mainStats[i]));
                statRect.y += _statFieldHeight + _heightOffset;
            }

            position.y = statRect.y;            
        }

        private static void DrawLabel(ref Rect position, string labelName, float centerMultiplier = .65f,
            int fontSize = 14)
        {
            var labelRect = new Rect(position)
            {
                x = position.center.x * centerMultiplier,
                height = _labelHeight
            };

            var boldText = new GUIStyle(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold,
                fontSize = fontSize
            };

            EditorGUI.LabelField(labelRect, labelName, boldText);
            position.y += _labelHeight;
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (_statFieldHeight + _heightOffset) * StatNames.Length + _labelHeight;
        }
    }
}