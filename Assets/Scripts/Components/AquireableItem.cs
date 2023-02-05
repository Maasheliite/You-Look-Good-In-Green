using Nizu.Util.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquireableItem : MonoBehaviour, IInteractable
{

    [field: SerializeField]
    public KeyCode interactButton { get; set; } = KeyCode.None;
    private SpriteRenderer[] spriteRenderers;
    private bool interactable = true;
    private float timer = 0;
    public float deathTimerMax = 100f;
    public GameCommand itemCommand;
    public float increaseValue;
    public GameObject getGameObject()
    {
       return gameObject;
    }
    private void Start()
    {
        spriteRenderers = GetComponents<SpriteRenderer>();
        if (spriteRenderers == null || spriteRenderers.Length == 0)
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }
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
            float val = Mathf.Lerp(1, 0, timer / deathTimerMax);
            foreach (var renderer in spriteRenderers)
            {
                renderer.material.color = new Color(1, 1, 1, val);
                renderer.color = new Color(1, 1, 1, val);
            }
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
