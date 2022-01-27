using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Person))]
public class PersonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //ImVisualElement.window = this.win;
        base.OnInspectorGUI();
    }

    public override VisualElement CreateInspectorGUI()
    {
        var inspectorRoot = new VisualElement();
        var inspectorIMGUIContainer = new IMGUIContainer(() =>
        {
            using (new ImVisualElementScope(inspectorRoot))
            {
                OnInspectorGUI();
            }
        });
        inspectorRoot.Add(inspectorIMGUIContainer);
        var s = Resources.Load<StyleSheet>("test");
        inspectorRoot.styleSheets.Add(s);
        return inspectorRoot;
    }
}