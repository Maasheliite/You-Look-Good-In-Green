using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BugsDestroy : MonoBehaviour
{

    public Animator animator;
    public AnimatorOverrideController Spikes;
    public AnimatorOverrideController Wave;
    private int attackNumber;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            GameObject.Destroy(collider.gameObject);
        }
    }

    private void Start()
    {

        attackNumber = Random.Range(1, 3);

        if (attackNumber == 1)
        {
            animator.runtimeAnimatorController = Spikes as RuntimeAnimatorController;
        }

        else
        {
            animator.runtimeAnimatorController = Wave as RuntimeAnimatorController;
        }

    }
}
