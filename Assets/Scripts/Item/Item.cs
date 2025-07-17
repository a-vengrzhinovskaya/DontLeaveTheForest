using UnityEngine;

public abstract class Item : MonoBehaviour {
    protected Player player;

    void Start() {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            TakeEffect();
            Destroy(gameObject);
        }
    }

    protected abstract void TakeEffect();
}
