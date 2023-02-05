using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Root))]
public class RootEditor : Editor
{
    public void OnSceneGUI()
    {
        Root actor = target as Root;
        if (target != null)
        {
            Handles.color = Color.blue;
            Handles.Label(actor.transform.position,actor.getStatePlusTimeRemaining());
        }
    }
}