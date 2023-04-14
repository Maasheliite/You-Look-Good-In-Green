using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayEffect : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Occludable"))
        {
            SpriteRenderer spriteRenderer = collider2D.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                Debug.Log("X-Ray effect...");
            }
        }
    }
    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Occludable"))
        {
            SpriteRenderer spriteRenderer = collider2D.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }
}
