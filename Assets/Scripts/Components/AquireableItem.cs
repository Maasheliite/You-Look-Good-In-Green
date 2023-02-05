using Nizu.Util.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquireableItem : MonoBehaviour, IInteractable
{

    [field: SerializeField]
    public KeyCode interactButton { get; set; } = KeyCode.None;
    public GameCommand itemCommand;
    public float increaseValue;
    public GameObject getGameObject()
    {
       return gameObject;
    }

    public void Interact(GameObject actor)
    {
        itemCommand.IncreaseValue(increaseValue);
    }
    
   

}
