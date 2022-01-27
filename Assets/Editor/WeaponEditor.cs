using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        //return base.CreateInspectorGUI();
        var root = new VisualElement();
        BuildInspectorProperties(serializedObject, root);
        return root;
    }

    private static void BuildInspectorProperties(SerializedObject obj, VisualElement container)
    {
        // TODO [Header()] and [Space()] are manually added until Unity supports them.

        var iterator = obj.GetIterator();
        var targetType = obj.targetObject.GetType();
        var members = new List<MemberInfo>(targetType.GetMembers());

        var style = Resources.Load<StyleSheet>("test");
        container.styleSheets.Add(style);

        if (!iterator.NextVisible(true)) return;
        do
        {
            var propertyField = new PropertyField(iterator.Copy())
            {
                name = "PropertyField:" + iterator.propertyPath
            };
            propertyField.AddToClassList("propField");
            DebugImgui(propertyField);
            var member = members.Find(x => x.Name == propertyField.bindingPath);
            if (member != null)
            {
                var headers = member.GetCustomAttributes(typeof(HeaderAttribute));
                var spaces = member.GetCustomAttributes(typeof(SpaceAttribute));

                foreach (var x in headers)
                {
                    var actual = (HeaderAttribute)x;
                    var header = new Label { text = actual.header };
                    header.style.unityFontStyleAndWeight = FontStyle.Bold;
                    container.Add(new Label { text = " ", name = "Header Spacer" });
                    container.Add(header);
                }

                foreach (var unused in spaces)
                {
                    container.Add(new Label { text = " " });
                }
            }

            if (iterator.propertyPath == "m_Script" && obj.targetObject != null)
            {
                propertyField.SetEnabled(value: false);
            }

            container.Add(propertyField);
        } while (iterator.NextVisible(false));
    }

    private static void DebugImgui(PropertyField propertyField)
    {
        propertyField.RegisterCallback<AttachToPanelEvent>(evt =>
        {
            var child = propertyField[0];
            if (child is IMGUIContainer container)
            {
            }
        });
    }

    /*public override VisualElement CreateInspectorGUI()
    {
        var container = new VisualElement();
 
        var iterator = serializedObject.GetIterator();
        if (iterator.NextVisible(true))
        {
            do
            {
                var propertyField = new PropertyField(iterator.Copy()) { name = "PropertyField:" + iterator.propertyPath };
 
                if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
                    propertyField.SetEnabled(value: false);
 
                container.Add(propertyField);
            }
            while (iterator.NextVisible(false));
        }
 
        return container;
    }*/
}