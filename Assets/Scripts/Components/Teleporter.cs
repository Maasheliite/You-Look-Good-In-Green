using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nizu.Util;
using UnityEditor;

[System.Serializable]
public class Teleporter : MonoBehaviour, IInteractable
{
    public GameObject target;
    [field:SerializeField]
    public KeyCode interactButton { get; set; } = KeyCode.None;

    public void Interact(GameObject actor)
    {
       Vector3 distance = gameObject.transform.position - target.transform.position;
        actor.transform.position = actor.transform.position - distance;
    }
}
