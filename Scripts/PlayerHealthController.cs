using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;

    public float invincibilityLength = 1f;
    private float invincibilityCounter;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityCounter > 0) {
            invincibilityCounter -= Time.deltaTime;

            if (invincibilityCounter <= 0) {
                PlayerController.instance.body.color = new Color(PlayerController.instance.body.color.r, PlayerController.instance.body.color.g, PlayerController.instance.body.color.b, 1f);
            }
        }
    }

    public void DamagePlayer() {
        if (invincibilityCounter <= 0) {
            currentHealth--;
            invincibilityCounter = invincibilityLength;

            PlayerController.instance.body.color = new Color(PlayerController.instance.body.color.r, PlayerController.instance.body.color.g, PlayerController.instance.body.color.b, 0.5f);

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth + " / " + maxHealth;

            // if the health drops to or below 0, deactivate the player
            // the player is an important object that we will likely reference in other places,
            // so if we were to destroy it instead of deactivate it, we would probably get errors in those places
            if (currentHealth <= 0) {
                PlayerController.instance.gameObject.SetActive(false);

                UIController.instance.deathScreen.SetActive(true);
            }
        }
    }
}
