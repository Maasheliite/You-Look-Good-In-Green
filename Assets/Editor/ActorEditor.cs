using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Actor))]
public class ActorEditor : Editor
{
    
    // Custom in-scene UI for when ExampleScript
    // component is selected.
    public void OnSceneGUI()
    {
        Actor actor = target as Actor;
       
        if (target != null)
        {
            
            Handles.color = Color.blue;
            Handles.DrawWireDisc(actor.transform.position, Vector3.forward, actor.range.Value);
            Handles.DrawWireDisc(actor.transform.position, Vector3.left, actor.range.Value);
            Handles.DrawWireDisc(actor.transform.position, Vector3.up, actor.range.Value);
            Handles.DrawWireDisc(actor.transform.position, Vector3.forward + Vector3.right, actor.range.Value);
            Handles.DrawWireDisc(actor.transform.position, Vector3.forward + Vector3.left, actor.range.Value);
        }
    }
}