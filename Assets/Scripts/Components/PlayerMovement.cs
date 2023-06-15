using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public GameObject AttackObject;
    public Transform AttackLocation;

    public Transform RespawnPoint;

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

    //Audioshit
    private AudioSource audioSource;
    public AudioClip hurtSound;
    public AudioClip slamSound;
    public AudioClip mudSound;
    public AudioClip shootSound;
    public AudioClip dashSound;
    public AudioClip dieSound;
    public AudioClip reviveSound;

    //Stepsound stuff
    public Tilemap tilemap;
    public AudioClip stepSoundGrass;
    public AudioClip stepSoundDirt;
    public AudioClip stepSoundClay;

    public float stepDistance = 1f;
    private float stepTimer = 0;
    private List<string> listOfDirtTiles = new List<string> { "GrassTileset_26","GrassTileset_1", "GrassTileset_0", "GrassTileset_8", "GrassTileset_9" };


    private void Start()
    {
        dist = new Vector2(0.8f, 0.8f);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        audioSource = gameObject.GetComponent<AudioSource>();
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
                if (isAttacking)
                {
                    return;
                }

                animator.SetBool("isAttacking", true);

                isAttacking = true;
                Invoke("Attack", .6f);
                Invoke("ResetAttack", attackDelay);
            }
            if (Input.GetButtonDown("Fire1") && CanShoot && !EventSystem.current.IsPointerOverGameObject())
            {
                if (isAttacking)
                {
                    return;
                }

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

                if (dashCooldownTimer <= 0f && Input.GetKeyDown(KeyCode.Space) && CanDash && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
                {
                    Dash();
                }
            }
            if (Input.GetKeyDown(KeyCode.H) && HealTimer <= Time.time && CanHeal == true)
            {
                PerformHeal();
            }
        }
    }

    private void FixedUpdate()
    {
        if (animator.GetFloat("Speed") > 0)
        {
            doFootStep();
        }
        else
        {
            stepTimer = 0;
        }

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

    private void doFootStep()
    {
        if (stepTimer <= 0)
        {
            stepTimer = stepDistance;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

            if (hit.collider != null)
            {

                Vector3Int tilePosition = tilemap.WorldToCell(hit.point);

                TileBase tile = tilemap.GetTile(tilePosition);

                if (tile is RuleTile ruleTile)
                {
                    float previousPitch = audioSource.pitch; 
                    audioSource.pitch = (Random.Range(0.9f, 1.1f));
                    Sprite tileSprite = tilemap.GetSprite(tilePosition);
                    if (listOfDirtTiles.Contains(tileSprite.name))
                    {
                        audioSource.PlayOneShot(stepSoundDirt);
                    }
                    else if (tileSprite.name.ToLower().Contains("grass"))
                    {
                        audioSource.PlayOneShot(stepSoundGrass);
                    }
                    else if (tileSprite.name.ToLower().Contains("tiletileset"))
                    {
                        audioSource.PlayOneShot(stepSoundClay);
                    }
                }else
                {
                    float previousPitch = audioSource.pitch;
                    audioSource.pitch = (Random.Range(0.9f, 1.1f));
                    if (tile.name.ToLower().Contains("dirt"))
                    {
                        audioSource.PlayOneShot(stepSoundDirt);
                    }
                    else if (tile.name.ToLower().Contains("grass"))
                    {
                        audioSource.PlayOneShot(stepSoundGrass);
                    }
                    else if (tile.name.ToLower().Contains("tiletileset"))
                    {
                        audioSource.PlayOneShot(stepSoundClay);
                    }
                }

            }
        }

        stepTimer -= Time.fixedDeltaTime;

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
        audioSource.PlayOneShot(shootSound);
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
        audioSource.PlayOneShot(mudSound);
    }

    private void Dash()
    {
        animator.Play("Dash");

        audioSource.PlayOneShot(dashSound);
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

    }



    public void TakeDamage(int damage)
    {
        audioSource.PlayOneShot(hurtSound);
        Health--;
        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.Play("Die");
        audioSource.PlayOneShot(dieSound);
        Invoke("Revive", 1.25f);
    }

    public void Revive()
    {
        gameObject.transform.position = RespawnPoint.position;
        animator.Play("Revive");
        audioSource.PlayOneShot(reviveSound);
        Health = MaxHealth;
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

    internal void Heal(int value)
    {
        Health += value;
    }

    private static PlayerMovement instance;

    // Public property to access the singleton instance
    public static PlayerMovement Instance
    {
        get
        {
            // Check if the instance is null
            if (instance == null)
            {
                // Find an existing instance in the scene
                instance = FindObjectOfType<PlayerMovement>();

                // If no instance exists, create a new GameObject and add the script to it
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(PlayerMovement).Name);
                    instance = singletonObject.AddComponent<PlayerMovement>();
                }

                // Make the instance persist across scene changes
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    // Optional: Add any other methods or variables you need for your singleton script

    private void Awake()
    {
        // Check if an instance already exists when the script is loaded
        if (instance != null && instance != this)
        {
            // Destroy the duplicate instance if there is one
            Destroy(gameObject);
        }
        else
        {
            // Set the instance if it doesn't already exist
            instance = this;
        }
    }

}
