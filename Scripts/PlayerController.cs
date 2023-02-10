using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // static is not visible as a changable variable in unity despite being public
    // it is set for all versions of the PlayerController throughout the game
    public static PlayerController instance;

    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D rb;

    public Transform gunArm;

    private Camera cam;

    public Animator animator;

    public GameObject bulletToFire;
    public Transform firePoint;

    // how long should we wait between spawning a bullet
    public float timeBetweenShots;
    // how long has passed since the last bullet spawned
    private float shotCounter;

    // Awake takes place before the Start function, and when you deactivate and reactivate an object
    private void Awake() {
        // set the current player character instance to this script
        // there can only be one instance at a time, so there can only be one player a time
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // using `Camera.main` could be a little resource intensive because behind the scenes,
        // it is using a function called `findObjectWithTag` which makes Unity search *every* 
        // object in the hierarchy for the tag `Main Camera`. This is why it's best to do this at
        // the start of a level
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        // Time.deltaTime is the time since the last frame in seconds
        // Ex. if player is running 60 FPS, time.deltaTime would be ~1/60, if they were running 30 FPS, it would be ~1/30, etc.
        // transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);
    
        // Move rigid body by multiplying each property of moveInput by moveSpeed
        rb.velocity = moveInput * moveSpeed;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.localPosition);

        // if the mouse is left of player, point the player and gun left
        if (mousePosition.x < screenPoint.x) {
            // we add an 'f' so that Unity knows we're giving it a float value instead of an int value
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
        } else {
            // if the mouse is right of player, point the player and gun right
            // `Vector3.one` is a shortcut way of writing `new Vector3(1f, 1f, 1f)`
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }

        // Make gun arm rotate following the mouse
        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);

        // when the mouse button is clicked
        // the left mouse button is 0, right mouse button is 1, middle mouse button is 2
        if (Input.GetMouseButtonDown(0)) {
            // when we want to spawn an object in Unity, we use `Instantiate`
            // this needs the item you are spawning, where to spawn it, and which rotation it should have
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            shotCounter = timeBetweenShots;
        }

        // when the mouse button is being held down
        if (Input.GetMouseButton(0)) {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0) {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);

                shotCounter = timeBetweenShots;
            }
        }

        // set moving so that we know when to play the idle and moving animations
        if (moveInput != Vector2.zero) {
            // for moving animation
            animator.SetBool("isMoving", true);
        } else {
            // for idle animation
            animator.SetBool("isMoving", false);
        }
    }
}
