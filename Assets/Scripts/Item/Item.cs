using UnityEngine;

public abstract class Item : MonoBehaviour {
    public Player player;

    protected Item(Player _player) {
        this.player = _player;
    }

    void Start() {
        Spawn();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            TakeEffect();
            Despawn();
        }
    }

    private void Spawn() {

    }

    private void Despawn() {

    }

    protected abstract void TakeEffect();
}
