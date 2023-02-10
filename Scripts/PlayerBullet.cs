using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D rb;

    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    // When the bullet hits something
    private void OnTriggerEnter2D(Collider2D other) {
        Instantiate(impactEffect, transform.position, transform.rotation);
        // destroy the gameObject that this script is attached to
        Destroy(gameObject);
    }

    // when the bullet leaves the screen
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
