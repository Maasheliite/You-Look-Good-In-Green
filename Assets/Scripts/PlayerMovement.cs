using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AnimatorOverrideController magicSword;
    public AnimatorOverrideController evilHelmet;


    public Animator animator;


    public GameObject Slash;
    public Transform Slashpoint;
    private bool stopFighting;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 direction;

    private bool isShooting;
    private float shootDelay = .7f;

    private AudioSource PlayerAudioSource;
    public AudioClip Eating;
    public float Volume;

    private Vector2 dist;

    public GameObject Prompt;


    public static bool notClimbing;

    private void Start()
    {
        PlayerAudioSource = gameObject.GetComponent<AudioSource>();
        if (ItemStatic.magicSword)
        {
            animator.runtimeAnimatorController = magicSword as RuntimeAnimatorController;
        }

        if (ItemStatic.helmet)
        {
            animator.runtimeAnimatorController = evilHelmet as RuntimeAnimatorController;
        }


        dist = new Vector2(0.8f, 0.8f);

        rb.constraints = RigidbodyConstraints2D.FreezeAll;




        ItemStatic.isInRange = false;
    }



    void Update()
    {
        


        if (ItemStatic.isInRange)
        {
            Prompt.SetActive(true);
        }

        else
        {
            Prompt.SetActive(false);
        }


        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (!isShooting)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
  
        }

        else if (isShooting)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", 0);
        }



        if (movement.x < 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
            Prompt.transform.localRotation = Quaternion.Euler(0, 180, 0);
            direction.x = -1;
            direction.y = 0;
        }
        if (movement.x > 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Prompt.transform.localRotation = Quaternion.Euler(0, 0, 0);

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
        if (!stopFighting && ItemStatic.sword || !stopFighting && ItemStatic.magicSword)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (isShooting) return;

                animator.SetBool("isAttacking", true);

                isShooting = true;
                Invoke("ActuallyShoot", .1f);
                Invoke("ResetShoot", shootDelay);

            }
        }

        

    }

    private void FixedUpdate()
    {
        if (!isShooting && (movement.x != 0 || movement.y != 0) && !notClimbing)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            freeze();
        }


        if (Input.GetKeyDown(KeyCode.F) && ItemStatic.berries)
        {
            PlayerAudioSource.PlayOneShot(Eating, Volume);
            ItemStatic.health = 100;
            ItemStatic.berries = false;
        }
    }

    private void freeze()
    {

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
        animator.SetFloat("Speed", 0);
    }


    private void ResetShoot()
    {
        animator.SetBool("isAttacking", false);
        isShooting = false;

    }

    private void ActuallyShoot()
    {
        Slashpoint.transform.position = rb.position + dist * direction;
        GameObject b = Instantiate(Slash);
        b.transform.position = Slashpoint.transform.position;
        if (direction.y == -1 || direction.y == 1)
        {
            b.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (direction.x == -1 || direction.x == 1)
        {
            b.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        Destroy(b, 0.5f);

    }



    public void StopFighting()
    {
        stopFighting = true;
    }

    public void StartFighting()
    {
        stopFighting = false;
    }


    public void MagicSword()
    {
        animator.runtimeAnimatorController = magicSword as RuntimeAnimatorController;
    }

    public void DemonHelmet()
    {
        animator.runtimeAnimatorController = evilHelmet as RuntimeAnimatorController;
    }

    public void TakeDamage(int damage)
    {
        ItemStatic.health -= damage;
        Debug.Log(ItemStatic.health);

        if (ItemStatic.health <= 0)
        {
            FindObjectOfType<LevelLoader>().LoadLose();
        }
    }
}

