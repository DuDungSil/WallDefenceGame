using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShowIfAttribute : PropertyAttribute
{
    public string ConditionProperty { get; }

    public ShowIfAttribute(string conditionProperty)
    {
        ConditionProperty = conditionProperty;
    }
}

[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class CustumInspectorShowIf : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIf = (ShowIfAttribute)attribute;
        SerializedProperty conditionProperty = property.serializedObject.FindProperty(showIf.ConditionProperty);

        if (conditionProperty == null)
        {
            conditionProperty = property.serializedObject.FindProperty(showIf.ConditionProperty);
        }

        if (conditionProperty != null && conditionProperty.boolValue)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIf = (ShowIfAttribute)attribute;
        SerializedProperty conditionProperty = property.serializedObject.FindProperty(showIf.ConditionProperty);

        if (conditionProperty == null)
        {
            conditionProperty = property.serializedObject.FindProperty(showIf.ConditionProperty);
        }

        if (conditionProperty != null && conditionProperty.boolValue)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        else
        {
            return 0; // Return 0 height to hide the property
        }
    }
}