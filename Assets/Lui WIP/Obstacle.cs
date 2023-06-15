using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float delayBetweenAnimations = 0.5f;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerMovement.CanUnbush)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                ChildScript[] childScripts = GetComponentsInChildren<ChildScript>();

                StartCoroutine(TriggerChildren(childScripts));
            }

        }
    }
    private IEnumerator TriggerChildren(ChildScript[] childScripts)
    {
        for (int i = 0; i < childScripts.Length; i++)
        {
            childScripts[i].TriggerAnimationAndDestroy();

            yield return new WaitForSeconds(delayBetweenAnimations);
        }
    }
}