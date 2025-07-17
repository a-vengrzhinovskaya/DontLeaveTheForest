using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public Transform playerObject;

    [Header("Movement")]
    public float moveSpeed = 4;
    public Transform orientation;

    [Header("Bounce")]
    public float bounceAmplitude = 0.15f;
    public float bounceFrequency = 6;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rigidBody;

    private float animationTime;
    private Vector3 initialLocalPosition;

    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;

        initialLocalPosition = playerObject.localPosition;
    }

    private void Update() {
        GetInput();
        ControlSpeed();
        Bounce();
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void GetInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer() {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void ControlSpeed() {
        var flatVelocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        if (flatVelocity.magnitude > moveSpeed) {
            var limitedVelocity = flatVelocity.normalized * moveSpeed;
            rigidBody.velocity = new Vector3(limitedVelocity.x, rigidBody.velocity.y, limitedVelocity.z);
        }
    }

    private void Bounce() {
        var isMoving = new Vector3(horizontalInput, 0f, verticalInput).magnitude > 0.1f;

        if (isMoving) {
            animationTime += Time.deltaTime * bounceFrequency;
            var scale = 1f + Mathf.Sin(animationTime) * bounceAmplitude;
            playerObject.localScale = new Vector3(1f, scale, 1f);
        } else {
            animationTime = 0f;
            playerObject.localScale = Vector3.one;
        }
    }
}
