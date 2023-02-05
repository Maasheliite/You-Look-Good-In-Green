using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SoundEffects
{
    public AudioClip audioclip;
    public float soundDelay;
    public float soundVolume;
};

public class SlimeAI : MonoBehaviour
{
    private Transform Player;
    public float DetectRange = 5;
    public float FireRange = 3;

    private float dist;

    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 5f;

    public float AttackTimer;
    private float AttackCooldown;
    [SerializeField]
    private bool IsAttacking;
    [SerializeField]
    private bool IsStandingStill;
    [SerializeField]
    private bool IsMoving;

    private Vector2 IdleWalk;
    private bool idletime;
    private float timer = 0;
    private float walking;

    public Animator animator;

    public int damage = 1;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        IsAttacking = false;
    }
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(gameObject.transform.position, Player.transform.position);

        if (dist < DetectRange)
        {
            Vector3 direction = Player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            direction.Normalize();
            movement = direction;
            IsMoving = true;
            IsStandingStill = false;
            idletime = false;
            animator.SetBool("Idling", false);
        }
        else if (idletime == false) Idle();

        //attack timer
        if (AttackCooldown > 0)
        {
            AttackCooldown = AttackCooldown - Time.deltaTime;
        }
        else
        {
            IsAttacking = false;
        }

    }

    private void FixedUpdate()
    {
        if (timer < Time.time && idletime == true)
        {
            MoveCharacter(movement);

            if (walking < Time.time)
            {
                Debug.Log("Timeout");
                IsStandingStill = true;
                IsMoving = false;
                animator.SetBool("Idling", true);


                timer = Time.time + Random.Range(2.5f, 4f);
                idletime = false;
            }
        }
        if (dist < DetectRange)
        {
            MoveCharacter(movement);
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        if (direction.x < 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (direction.x > 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            Destroy(gameObject);

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && (AttackCooldown <= 0))
        {
            bool leftHit = false;
            bool rightHit = false;
            float f = 0.5f;

            var hit = collision.contacts[0];
            if (Mathf.Abs(hit.normal.x) > f)
            {
                if (hit.normal.x > 0f)
                    leftHit = true;
                else
                    rightHit = true;
            }

            if (leftHit)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (rightHit)
            {
                gameObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            FindObjectOfType<PlayerMovement>().TakeDamage(damage);
            animator.SetBool("Attacking", true);
            AttackCooldown = AttackTimer;
            IsAttacking = true;
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Attacking", false);
        }
    }

    private void Idle()
    {
        
        if (timer < Time.time)
        {
            Debug.Log("Idle");
            IsMoving = true;
            IsStandingStill = false;
            animator.SetBool("Idling", false);

            idletime = true;
            walking = Time.time + 5;

            IdleWalk = Random.insideUnitCircle * 4;

            Vector3 direction = IdleWalk - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            direction.Normalize();
            movement = direction;
        }

    }

}
