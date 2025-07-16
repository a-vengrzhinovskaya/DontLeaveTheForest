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
}
