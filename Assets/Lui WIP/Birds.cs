using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nizu.Util.ScriptableObjects;



public class Birds : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public KeyCode interactButton { get; set; } = KeyCode.None;

    private SpriteRenderer spriteRenderer;
    public SetSelfInactive costElement;
    public GameCommand stickResource;
    public float stickCost;
    public Sprite built;


    private bool builtBool = false;


    public GameObject getGameObject()
    {
        return gameObject;
    }

    public void Interact(GameObject actor)
    {
        if (stickResource.DecreaseValueUntilZero(stickCost))
        {
            spriteRenderer.sprite = built;
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
