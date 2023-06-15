using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nizu.Util.ScriptableObjects;
using Nizu.InventorySystem;

public class Birds : MonoBehaviour, IInteractable
{
    [field: SerializeField]

    private SpriteRenderer spriteRenderer;
    public SetSelfInactive costElement;
    public ItemDetails stickResource;
    public ItemDetails leafResource;
    public ItemDetails clayResource;
    public ItemDetails wormResource;
    public int stickCost;
    public int leafCost;
    public int clayCost;
    public int wormCost;
    public Sprite built;

    public Inventory inventory;

    private bool builtBool = false;

    public GameObject mainCanvas;
    public GameObject BirdMap;
    private bool mapOpen;

    public GameObject getGameObject()
    {
        return gameObject;
    }

    public void Interact(GameObject actor)
    {
        if (builtBool && !mapOpen)
        {
            BirdMap.SetActive(true);
            mainCanvas.SetActive(false);

        }
        else if (mapOpen)
        {
            BirdMap.SetActive(false);
            mainCanvas.SetActive(true);

        }
        else if (inventory.removeItemOfType(stickResource, stickCost) && inventory.removeItemOfType(leafResource, leafCost) &&  inventory.removeItemOfType(clayResource, clayCost) && inventory.removeItemOfType(wormResource, wormCost))
        {
            spriteRenderer.sprite = built;
            builtBool = true;
        }
    }

    public void onHighlight()
    {
        if (!builtBool)
        {
            costElement.gameObject.SetActive(true);
        }

    }
        // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    
}
