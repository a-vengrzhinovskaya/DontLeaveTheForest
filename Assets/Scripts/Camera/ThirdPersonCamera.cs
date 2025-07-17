using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    public Transform orientation;
    public Transform player;
    public Transform playerObject;
    public Rigidbody rigidBody;

    public float rotationSpeed;

    private void Update() {
        var viewDirection = Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized;
        orientation.forward = viewDirection.normalized;
    }
}
