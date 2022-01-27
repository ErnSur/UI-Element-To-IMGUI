using System;
using UnityEditor;
using UnityEngine;

public class ExampleWindow : EditorWindow
{
    private ImVisualElement ve;

    [MenuItem("Tests/Example Window")]
    public static void Open()
    {
        GetWindow<ExampleWindow>();
    }

    private void OnGUI()
    {
        GUILayout.Button("test");
        ve?.OnGUI();
        GUILayout.Label("IMGUI label");
        var r = GUILayoutUtility.GetLastRect();
        EditorGUI.DrawRect(r, Color.green);
    }

    private void CreateGUI()
    {
        rootVisualElement.Add(ve = new ImVisualElement());
    }
}