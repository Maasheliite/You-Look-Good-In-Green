using Nizu.Util.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<GameCommand> inventory = new List<GameCommand>();

    void Start()
    {
        inventory.ForEach(x => x.SetValue(0));
    }

}
