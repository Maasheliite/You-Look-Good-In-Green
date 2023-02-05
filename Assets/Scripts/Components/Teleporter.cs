using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nizu.Util;
using UnityEditor;

[System.Serializable]
[RequireComponent(typeof(Collider))]
public class Teleporter : MonoBehaviour, IInteractable
{
    public GameObject target;
    [field: SerializeField]
    public KeyCode interactButton { get; set; } = KeyCode.None;

    public GameObject getGameObject()
    {
        return gameObject;
    }

    public void Interact(GameObject actor)
    {
       Vector3 distance = gameObject.transform.position - target.transform.position;
        actor.transform.position = actor.transform.position - distance;
    }
}
