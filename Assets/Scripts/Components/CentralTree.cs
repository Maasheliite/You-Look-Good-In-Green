using Nizu.Util.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CentralTree : MonoBehaviour, IInteractable
{
    public Sprite restoredSprite;

    public SetSelfInactive costElement;
    public GameCommand essenceResource;
    public float decreaseValue;
    public UnityEvent winGame;

    private SpriteRenderer spriteRenderer;

    [field: SerializeField]
    public KeyCode interactButton { get; set; } = KeyCode.None;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public GameObject getGameObject()
    {
       return gameObject;
    }

    public void Interact(GameObject actor)
    {
        if (essenceResource.DecreaseValueUntilZero(decreaseValue))
        {
            winGame.Invoke();
            spriteRenderer.sprite = restoredSprite;
        }
        else
        {
            //tell not enough resource
        }
    }

    public void onHighlight()
    {
        costElement.gameObject.SetActive(true);
        costElement.display();
    }

}
