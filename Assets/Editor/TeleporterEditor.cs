using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Teleporter))]
public class TeleporterEditor : Editor
{
    // Custom in-scene UI for when ExampleScript
    // component is selected.
    public void OnSceneGUI()
    {
        Teleporter teleporter = target as Teleporter;
        if (teleporter.target == null)
        {
            return;
        }

        if (target != null)
        {
            Vector3 distance = teleporter.transform.position - teleporter.target.transform.position;
            Handles.color = Color.red;
            Handles.DrawLine(teleporter.transform.position, teleporter.target.transform.position, 2f);
            Handles.Label(teleporter.transform.position, teleporter.name);
            Handles.Label(teleporter.transform.position - distance / 2 + distance.normalized , " connected to ");
            Handles.Label(teleporter.target.transform.position, teleporter.target.name);
        }
    }
}