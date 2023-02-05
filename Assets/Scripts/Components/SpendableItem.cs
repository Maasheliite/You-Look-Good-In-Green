using Nizu.Util.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpendableItem : MonoBehaviour, IInteractable
{

    [field: SerializeField]
    public KeyCode interactButton { get; set; } = KeyCode.None;
    public GameCommand itemCommand;
    public float decreaseValue;
    public UnityEvent effect;
    public GameObject getGameObject()
    {
        return gameObject;
    }

    public void Interact(GameObject actor)
    {
        if (itemCommand.DecreaseValueUntilZero(decreaseValue))
        {
            effect.Invoke();
        }
    }

    public void onHighlight()
    {
        //show cost;
    }
}
