using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool stopFighting;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 direction;

    private bool isShooting;
    private float shootDelay = .7f;

    private AudioSource PlayerAudioSource;

    private Vector2 dist;



    public static bool notClimbing;

    private void Start()
    {

        dist = new Vector2(0.8f, 0.8f);

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }



    void Update()
    {
        

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");




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

        

    }

    private void FixedUpdate()
    {
        
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        
    }

    private void freeze()
    {

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

    }

}

