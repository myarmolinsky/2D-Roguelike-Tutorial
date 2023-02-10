using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    
    // how many grid units away is the player before we start chasing them
    public float rangeToChasePlayer;
    // the direction the player is in relative to the enemy
    private Vector3 moveDirection;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3.Distance gets the distance between 2 points
        // we can access the PlayerController instance because it is public static
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer) {
            moveDirection = PlayerController.instance.transform.position - transform.position;
            animator.SetBool("isMoving", true);
        } else {
            // if the player is not within range, reset the moveDirection to 0 so that the enemy does not keep moving
            moveDirection = Vector3.zero;
            animator.SetBool("isMoving", false);
        }

        moveDirection.Normalize();
        rb.velocity = moveDirection * moveSpeed;
    }
}
