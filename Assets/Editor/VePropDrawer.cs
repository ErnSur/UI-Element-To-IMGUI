using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class VeImguiPropertyDrawer : PropertyDrawer
{
    private bool inited;
    protected ImVisualElement propertyDrawer;

    public override bool CanCacheInspectorGUI(SerializedProperty property)
    {
        return true;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (!inited && ImVisualElement.root != null)
        {
            inited = true;
            var root = ImVisualElement.root;
            //TODO: Fix, visual elements are added to the same root multiple times
            EditorApplication.delayCall += () =>
            {
                Debug.Log($"Added Property Drawer to root");
                propertyDrawer = new ImVisualElement();
                propertyDrawer.Add(CreatePropertyGUI(property));
                root.Add(propertyDrawer);
            };
        }

        propertyDrawer?.OnGUI(position);
    }
}

[CustomPropertyDrawer(typeof(VeAttribute))]
public class VePropDrawer : VeImguiPropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        //var att = attribute as RangeAttribute;
        var ve = new IntegerField("main");
        ve.name = "main";
        var style = Resources.Load<StyleSheet>("test");
        ve.styleSheets.Add(style);
        ve.BindProperty(property);
        return ve;
    }
}

[CustomPropertyDrawer(typeof(IMGUIAttribute))]
public class ImguiPropDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (Event.current.type != EventType.Layout)
            Debug.Log($"IMGUI: {position}");
        EditorGUI.PropertyField(position, property, label);
    }
}