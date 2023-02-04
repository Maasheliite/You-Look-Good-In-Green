using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Actor))]
public class ActorEditor : Editor
{
    public void OnSceneGUI()
    {
        Actor actor = target as Actor;
        if (target != null)
        {
            Handles.color = Color.blue;
            Handles.DrawWireDisc(actor.transform.position, Vector3.forward, actor.range.Value);
        }
    }
}