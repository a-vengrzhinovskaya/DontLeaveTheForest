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

        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDirection != Vector3.zero) {
            playerObject.forward = Vector3.Slerp(
                playerObject.forward,
                inputDirection.normalized,
                Time.deltaTime * rotationSpeed
            );
        }
    }
}
