using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public GameObject AttackObject;
    public Transform AttackLocation;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 direction;

    private bool isAttacking;
    private float attackDelay = 2f;

    private Vector2 dist;


    public static bool notClimbing;

    private void Start()
    {
        dist = new Vector2(0.8f, 0.8f);

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

    }



    void Update()
    {
        if (GameStateManager.gameState == GameStateManager.GameState.Running)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (!isAttacking)
            {
                animator.SetFloat("Horizontal", direction.x);
                animator.SetFloat("Vertical", direction.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);
            }

            else if (isAttacking)
            {
                animator.SetFloat("Horizontal", direction.x);
                animator.SetFloat("Vertical", direction.y);
                animator.SetFloat("Speed", 0);
            }



            if (movement.x < 0)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
                direction.x = -1;
                direction.y = 0;
            }
            if (movement.x > 0)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);

                direction.x = 1;
                direction.y = 0;
            }
            if (movement.y > 0)
            {
                direction.y = 1;
                direction.x = 0;
            }
            if (movement.y < 0)
            {
                direction.y = -1;
                direction.x = 0;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                if (isAttacking) return;

                animator.SetBool("isAttacking", true);

                isAttacking = true;
                Invoke("Attack", .6f);
                Invoke("ResetAttack", attackDelay);
                
            }



        }

    }

    private void FixedUpdate()
    {
        if (!isAttacking && (movement.x != 0 || movement.y != 0) && !notClimbing)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            freeze();
        }
    }

    private void freeze()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
        animator.SetFloat("Speed", 0);
    }


    private void ResetAttack()
    {
        animator.SetBool("isAttacking", false);
        isAttacking = false;

    }

    private void Attack()
    {

        GameObject b = Instantiate(AttackObject);
        b.transform.position = AttackLocation.transform.position;

        Destroy(b, 1.5f);
    }




    public void TakeDamage(int damage)
    {
        return;
    }
}

