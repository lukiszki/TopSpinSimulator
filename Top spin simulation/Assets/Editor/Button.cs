using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Naped))]
public class Button : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Naped naped = (Naped)target;

        if(GUILayout.Button("Randomize AI"))
        {
            //naped.RandomizeVals();
        }
    }
}
