using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Behavior")]
    public int maxHealth = 3;

    public bool IsDead { get; private set; }

    public int CurrentHealth {  get; private set; }

    void Start() {
        CurrentHealth = maxHealth;
    }

    public void takeDamage(int incomingDamage) {
        CurrentHealth -= incomingDamage;
        if (CurrentHealth <= 0) {
            IsDead = true;
            GameManager.instance.GameOver();
        }
    }

    public void receiveBoost(float duration, float boostStrength) {
        StartCoroutine(launchSpeedBoostCoroutine(duration, boostStrength));
    }

    private IEnumerator launchSpeedBoostCoroutine(float duration, float boostStrength) {
        var movementController = GetComponent<PlayerMovement>();
        movementController.changeMoveSpeed(movementController.defaultMoveSpeed * boostStrength);

        yield return new WaitForSeconds(duration);

        movementController.changeMoveSpeed(movementController.defaultMoveSpeed);
    }
}
