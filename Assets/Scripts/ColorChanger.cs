using System;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private void SetColor(Transform trans, Color color)
    {
        trans.GetComponent<Renderer>().material.color = color;
    }

    public void UpdateColor(string[] values)
    {
        Debug.Log("Update!" + values[1]);
        var colorString = values[0];
        var shapeString = values[1];

        if (!ColorUtility.TryParseHtmlString(colorString, out var color)) return;
        if (string.IsNullOrEmpty(shapeString)) return;

        foreach (Transform child in transform)
        {
            if (child.name.IndexOf(shapeString, StringComparison.OrdinalIgnoreCase) != -1)
            {
                Debug.Log("match!");
                SetColor(child, color);
                return;
            }
        }
    }
}