using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MechStats))]
public class MechStatsEditor : Editor
{
    MechStats selected;
    string newName = "";
    void OnEnable()
    {
        selected = (MechStats)target;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if(selected._dictionary.Count == 0)
        {
            GUILayout.Label("There are no entries in the list");
        }
        else
        {
            foreach(string entry in selected._dictionary.Keys)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(entry);
                GUILayout.Label(selected._dictionary[entry].currentValue.ToString());
                GUILayout.EndHorizontal();
            }
        }

        GUILayout.BeginHorizontal();
        newName = GUILayout.TextField(newName);
        EditorGUI.BeginDisabledGroup(newName == "" || selected._dictionary.ContainsKey(newName));
        if(GUILayout.Button("Add Entry"))
        {
            selected._dictionary.Add(newName, new StatBlock());
        }
        EditorGUI.EndDisabledGroup();
        GUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}
