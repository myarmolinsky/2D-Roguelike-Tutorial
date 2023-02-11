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

    public int health = 150;

    public GameObject[] deathSplatters;
    public GameObject hitEffect;

    public Animator animator;

    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public float shootRange;
    
    public SpriteRenderer body;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (body.isVisible) {
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

            if (moveDirection != Vector3.zero) {
                // if the skeleton is moving, turn the it in the direction of the player
                if (transform.position.x < PlayerController.instance.transform.position.x) {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                } else {
                    transform.localScale = Vector3.one;
                }
            }

            // only shoot if this enemy should shoot and if it is in the given range of the player
            if (shouldShoot && Vector3.Distance(PlayerController.instance.transform.position, transform.position) < shootRange) {
                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0) {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                }
            }
        }
    }

    // this is the function we'll call when we want to damage the enemy
    public void DamageEnemy(int damage) {
        Instantiate(hitEffect, transform.position, transform.rotation);
        health -= damage;

        // if the enemy is out of health, it is dead, destroy it
        if (health <= 0) {
            Destroy(gameObject);
            int selectedSplatter = Random.Range(0, deathSplatters.Length);
            int splatterRotation = Random.Range(0, 4);
            Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, 90f * splatterRotation));
        }
    }
}
