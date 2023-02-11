using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer() {
        currentHealth--; 
        // if the health drops to or below 0, deactivate the player
        // the player is an important object that we will likely reference in other places,
        // so if we were to destroy it instead of deactivate it, we would probably get errors in those places
        if (currentHealth <= 0) {
            PlayerController.instance.gameObject.SetActive(false);
        }
    }
}