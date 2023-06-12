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

    // Dash Variables
    public float dashDistance = 5f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    private bool CanDash = false;
    private bool CanHeal = false;

    private int MaxHealth = 20;
    private int Health = 20;
    private float HealTimer = 0f;


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

            

            if (!isDashing)
            {
                dashCooldownTimer -= Time.deltaTime;

                if (dashCooldownTimer <= 0f && Input.GetKeyDown(KeyCode.Mouse1) && CanDash && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
                {
                    Dash();
                }
            }
            if (Input.GetKeyDown(KeyCode.H) && HealTimer<=Time.time && CanHeal == true)
            {
                PerformHeal();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isAttacking && !isDashing && (movement.x != 0 || movement.y != 0) && !notClimbing)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        else if (isDashing)
        {

            dashTimer -= Time.deltaTime;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;


            if (dashTimer <= 0f)
            {
                isDashing = false;
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            Freeze();
        }

    }

    private void Freeze()
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

    private void Dash()
    {
        animator.Play("Dash");

        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;

        // Calculate the dash direction based on player input
        Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        // Apply dash force to the rigidbody using easing function
        StartCoroutine(DashCoroutine(dashDirection));
    }

    private IEnumerator DashCoroutine(Vector2 dashDirection)
    {
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            float t = elapsedTime / dashDuration;
            float easedT = EaseInEaseOut(t);

            // Calculate current dash speed based on eased t
            float currentDashSpeed = Mathf.Lerp(0f, dashDistance, easedT);

            // Apply dash force to the rigidbody
            rb.velocity = dashDirection * currentDashSpeed;

            elapsedTime += Time.fixedDeltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        isDashing = false;
    }

    private float EaseInEaseOut(float t)
    {
        // Apply easing function (you can adjust the curve as per your preference)
        return Mathf.SmoothStep(0f, 1f, t);
    }

    private void PerformHeal()
    {
        Health = MaxHealth;
        HealTimer = Time.time + 20f;
        Debug.Log("heal");

    }

    public void TakeDamage(int damage)
    {
        Health--;
    }

    public void ActivateDash()
    {
        CanDash = true;
    }

    public void ActivateHeal()
    {
        CanHeal = true;
    }
}
