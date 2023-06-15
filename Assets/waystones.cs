using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waystones : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TeleportWaypoints.PlayerInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TeleportWaypoints.PlayerInRange = false;
        }
    }
}
