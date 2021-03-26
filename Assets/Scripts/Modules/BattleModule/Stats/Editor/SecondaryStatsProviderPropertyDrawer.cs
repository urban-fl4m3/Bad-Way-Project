using System;
using Modules.BattleModule.Stats.Providers;
using UnityEditor;
using UnityEngine;

namespace Modules.BattleModule.Stats.Editor
{
    [CustomPropertyDrawer(typeof(SecondaryStatsProvider))]
    public class SecondaryStatsProviderPropertyDrawer : StatsProviderPropertyDrawer
    {
        protected override string StatLabelName => "Secondary Stats";
        protected override string StatsPropertyName => "_secondaryStats";
        protected override string[] StatNames => Enum.GetNames(typeof(SecondaryStat));

        protected override void DrawStat(ref Rect position, SerializedProperty property, SerializedProperty arrayStatsProperty, int index,
            string[] statNames)
        {
            var statAndUpgradesProperty = arrayStatsProperty.GetArrayElementAtIndex(index);
            var statValueProperty = statAndUpgradesProperty.FindPropertyRelative("Value");
            var upgradeListProperty = statAndUpgradesProperty.FindPropertyRelative("UpgradeList");
            
            EditorGUI.PropertyField(position, statValueProperty, new GUIContent(statNames[index]));
            position.y += _statFieldHeight;

            EditorGUI.PropertyField(position, upgradeListProperty, new GUIContent("Upgrades"));
            position.y += EditorGUI.GetPropertyHeight(upgradeListProperty) + _heightOffset * 2;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var statsProperty = property.FindPropertyRelative(StatsPropertyName);
            var upgradesCount = 0;
            float totalHeight = 0;
            
            for (var i = 0; i < statsProperty.arraySize; i++)
            {
                var statAndUpgradesProperty = statsProperty.GetArrayElementAtIndex(i);
                var upgradeList = statAndUpgradesProperty.FindPropertyRelative("UpgradeList");

                if (upgradeList.isExpanded)
                {
                    if (upgradeList.arraySize == 0)
                    {
                        totalHeight += _statFieldHeight * 2 + _heightOffset * 2;
                    }
                    else
                    {
                        upgradesCount += upgradeList.arraySize;
                        totalHeight += (float)_statFieldHeight * 2 / 3;
                    }
                }
            }

            totalHeight += upgradesCount * (_statFieldHeight + _heightOffset)
                           + statsProperty.arraySize * (_statFieldHeight + _heightOffset) * 1.5f;
            
            return base.GetPropertyHeight(property, label) + totalHeight;
        }
    }
}