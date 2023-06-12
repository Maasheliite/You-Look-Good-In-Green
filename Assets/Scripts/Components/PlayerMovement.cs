using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public static bool CanDash = false;
    public static bool CanHeal = false;
    public static bool CanSlam = false;
    public static bool CanUnbush = false;
    public static bool CanTeleport = false;
    public static bool CanShoot = false;
    public static bool CanMudPool = false;

    public static int SkillPoints = 10;


    private int MaxHealth = 20;
    private int Health = 20;
    private float HealTimer = 0f;

    //shooting variables
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 10f;

    //mud pool variables
    public GameObject mudPoolPrefab;
    public float mudPoolRange = 5f;


    //All Buttons
    public Button UnbushButton;
    public Button ShootButton;
    public Button DashButton;
    public Button TeleportButton;
    public Button SlamButton;
    public Button MudPoolButton;

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
            if (Input.GetButtonDown("Fire2") && CanSlam)
            {
                if (isAttacking) return;

                animator.SetBool("isAttacking", true);

                isAttacking = true;
                Invoke("Attack", .6f);
                Invoke("ResetAttack", attackDelay);
            }
            if (Input.GetButtonDown("Fire1") && CanShoot)
            {
                if (isAttacking) return;

                animator.SetBool("isLeafAttacking", true);

                isAttacking = true;
                Invoke("LeafAttack", .6f);
                Invoke("ResetAttack", attackDelay);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (CanMudPool)
                {
                    SpawnMudPool();
                }
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
        animator.SetBool("isLeafAttacking", false);

        isAttacking = false;
    }

    private void Attack()
    {
        GameObject b = Instantiate(AttackObject);
        b.transform.position = AttackLocation.transform.position;
        Destroy(b, 1.5f);
    }
    private void LeafAttack()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        projectile.transform.right = direction;

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = projectile.transform.right * projectileSpeed; // Adjust projectileSpeed as needed

        Destroy(projectile, 1.5f);
    }

    private void SpawnMudPool()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 spawnPosition = transform.position + (mousePosition - transform.position).normalized * mudPoolRange;

        Instantiate(mudPoolPrefab, spawnPosition, Quaternion.identity);
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
        if (SkillPoints > 0)
        {
            CanDash = true;
            SkillPoints--;
        }
    }

    public void ActivateHeal()
    {
        if (SkillPoints > 0)
        {
            CanHeal = true;
            SkillPoints--;
            UnbushButton.interactable = true;
            ShootButton.interactable = true;
        }
    }

    public void ActivateSlam()
    {
        if (SkillPoints > 0)
        {
            CanSlam = true;
            SkillPoints--;
        }
    }

    public void ActivateUnbush()
    {
        if (SkillPoints > 0)
        {
            CanUnbush = true;
            SkillPoints--;
            DashButton.interactable = true;
            TeleportButton.interactable = true;
        }
    }
    public void ActivateTeleport()
    {
        if (SkillPoints > 0)
        {
            CanTeleport = true;
            SkillPoints--;
        }
    }
    public void ActivateShoot()
    {
        if (SkillPoints > 0)
        {
            CanShoot = true;
            SkillPoints--;
            SlamButton.interactable = true;
            MudPoolButton.interactable = true;
        }
    }

    public void ActivateMud()
    {
        if (SkillPoints > 0)
        {
            CanMudPool = true;
            SkillPoints--;
        }
    }
}
