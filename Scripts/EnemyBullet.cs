using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        // we are setting the direction at the start instead of update so that it only goes in the
        // direction the player was in when it spawned instead of always following them
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            PlayerHealthController.instance.DamagePlayer();
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible () {
        Destroy(gameObject);
    }
}
