using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugsDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            GameObject.Destroy(collider.gameObject);
        }
    }
}
