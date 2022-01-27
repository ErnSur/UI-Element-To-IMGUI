using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ImVisualElementScope : IDisposable
{
    public ImVisualElementScope(VisualElement root)
    {
        ImVisualElement.root = root;
    }

    public void Dispose()
    {
        ImVisualElement.root = null;
    }
}

public class ImVisualElement : VisualElement
{
    public static VisualElement root;
    private Rect lastRect;
    private Rect layoutRect = new Rect(0,0,1,1);

    public ImVisualElement()
    {

    }

    public void OnGUI()
    {
        var width = resolvedStyle.width;
        var height = resolvedStyle.height;
        width = layout.width;
        height = layout.height;

        var newRect = GUILayoutUtility.GetRect(width, height);
        if (Event.current.type != EventType.Layout)
            OnGUI(newRect);
    }

    public void OnGUI(Rect newRect)
    {
        if (lastRect == newRect || newRect == layoutRect)
            return;
        lastRect = newRect;
        Debug.Log($"Ve: {newRect}");
        style.position = Position.Absolute;
        transform.position = new Vector3(newRect.x, newRect.y, 0);
        // style.top = newRect.y;
        // style.left = newRect.x;
         style.width = newRect.width;
         style.height = newRect.height;
    }
}