using UnityEditor;
using UnityEngine;

namespace Modules.BattleModule.Stats.Editor
{
    public abstract class StatsProviderPropertyDrawer : PropertyDrawer
    {
        private const int _labelHeight = 30;
        
        protected const int _statFieldHeight = 20;
        protected const int _heightOffset = 5;

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

        private void DrawArray(string[] namesArray, string propertyName, SerializedProperty property,
            ref Rect position)
        {
            var mainStats = namesArray;
            var mainStatsProperty = property.FindPropertyRelative(propertyName);
            
            var statRect = new Rect(position)
            {
                height = _statFieldHeight
            };

            mainStatsProperty.arraySize = mainStats.Length;

            DrawStatArray(ref statRect, property, mainStatsProperty, mainStats);
            
            position.y = statRect.y;            
        }

        private void DrawStatArray(ref Rect position, SerializedProperty property,
            SerializedProperty arrayStatsProperty, string[] statNames)
        {
            for (var i = 0; i < arrayStatsProperty.arraySize; i++)
            {
                DrawStat(ref position, property, arrayStatsProperty, i, statNames);
            }            
        }

        protected virtual void DrawStat(ref Rect position, SerializedProperty property,
            SerializedProperty arrayStatsProperty, int index, string[] statNames)
        {
            var stat = arrayStatsProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(position, stat, new GUIContent(statNames[index]));
            position.y += _statFieldHeight + _heightOffset;
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