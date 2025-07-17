using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {
    public GameObject healthIndicator;
    public Player player;

    private List<GameObject> hearts = new List<GameObject>();

    void Start() {
        UpdateHearts();
    }

    void Update() {
        if (hearts.Count != player.CurrentHealth) {
            UpdateHearts();
        }
    }

    void UpdateHearts() {
        foreach (var heart in hearts) {
            Destroy(heart);
        }
        hearts.Clear();

        for (var heartNumber = 0; heartNumber < player.CurrentHealth; ++heartNumber) {
            var newHeart = Instantiate(healthIndicator, transform);
            hearts.Add(newHeart);
        }
    }
}
