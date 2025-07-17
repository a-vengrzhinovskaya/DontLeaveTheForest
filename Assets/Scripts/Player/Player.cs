using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Game Manager")]

    [Header("Behavior")]
    public int maxHealth = 3;

    public bool isDead { get; private set; }

    public int CurrentHealth {  get; private set; }

    void Start() {
        CurrentHealth = maxHealth;
    }

    public void takeDamage(int incomingDamage) {
        CurrentHealth -= incomingDamage;
        if (CurrentHealth <= 0) {
            isDead = true;
            GameManager.instance.GameOver();
        }
    }

    public void getSpeedBoost(float duration, float boostStrength) {
        StartCoroutine(SpeedBoostCoroutine(duration, boostStrength));
    }

    private IEnumerator SpeedBoostCoroutine(float duration, float boostStrength) {
        var movementController = GetComponent<PlayerMovement>();
        var originalSpeed = movementController.moveSpeed;
        movementController.moveSpeed = originalSpeed * boostStrength;

        yield return new WaitForSeconds(duration);

        movementController.moveSpeed = originalSpeed;
    }
}
