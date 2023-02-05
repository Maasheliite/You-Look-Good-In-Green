using Nizu.Util.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquireableItem : MonoBehaviour, IInteractable
{

    [field: SerializeField]
    public KeyCode interactButton { get; set; } = KeyCode.None;
    private SpriteRenderer spriteRenderer;
    private bool interactable = true;
    private float timer = 0;
    public float deathTimerMax = 1f;
    public GameCommand itemCommand;
    public float increaseValue;
    public GameObject getGameObject()
    {
       return gameObject;
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (!interactable && timer > deathTimerMax)
        {
            GameObject.Destroy(gameObject);
        }
        else if (!interactable)
        {
            timer += Time.deltaTime;
            spriteRenderer.color = new Color(255, 255, 255, Mathf.Lerp(255, 0, timer/ deathTimerMax));
        }
    }
    public void Interact(GameObject actor)
    {
        if (interactable)
        {
            itemCommand.IncreaseValue(increaseValue);
            interactable = false;
        }
    }
    

}
