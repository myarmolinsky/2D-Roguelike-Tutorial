using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveInput;

    public Rigidbody2D rb;

    public Transform gunArm;

    private Camera cam;

    public Animator animator;

    public GameObject bulletToFire;
    public Transform firePoint;

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

        // transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);
    
        // Move rigid body by multiplying each property of moveInput by moveSpeed
        rb.velocity = moveInput * moveSpeed;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.localPosition);

        if (mousePosition.x < screenPoint.x) {
            // we add an 'f' so that Unity knows we're giving it a float value instead of an int value
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
        } else {
            // `Vector3.one` is a shortcut way of writing `new Vector3(1f, 1f, 1f)`
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }

        // Make gun arm rotate following the mouse
        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);

        // when the mouse button is held down
        // the left mouse button is 0, right mouse button is 1, middle mouse button is 2
        if (Input.GetMouseButtonDown(0)) {
            // when we want to spawn an object in Unity, we use `Instantiate`
            // this needs the item you are spawning, where to spawn it, and which rotation it should have
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
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
