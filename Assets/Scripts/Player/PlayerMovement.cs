using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    public Transform playerObject;

    [Header("Movement")]
    public float defaultMoveSpeed = 4;
    public Transform orientation;

    [Header("Bounce")]
    public float bounceAmplitude = 0.15f;
    public float bounceFrequency = 6;

    private float currentMoveSpeed;

    private Vector2 moveInput;
    private Vector3 moveDirection;

    private Rigidbody rigidBody;

    private float animationTime;
    private Vector3 initialLocalPosition;

    private PlayerControls controls;

    private void Awake() {
        controls = new PlayerControls();
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += _ => moveInput = Vector2.zero;
    }

    private void OnEnable() {
        controls.Player.Enable();
    }

    private void OnDisable() {
        controls.Player.Disable();
    }


    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;

        currentMoveSpeed = defaultMoveSpeed;

        initialLocalPosition = playerObject.localPosition;
    }

    private void Update() {
        ControlSpeed();
        Bounce();
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    public void changeMoveSpeed(float newMoveSpeed) {
        currentMoveSpeed = newMoveSpeed;
    }

    private void MovePlayer() {
        moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;

        if (moveDirection.magnitude > 0.1f) {
            var targetRotation = Quaternion.LookRotation(moveDirection.normalized);
            playerObject.rotation = Quaternion.Slerp(playerObject.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }

        rigidBody.AddForce(moveDirection.normalized * currentMoveSpeed * 10f, ForceMode.Force);
    }

    private void ControlSpeed() {
        var flatVelocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        if (flatVelocity.magnitude > currentMoveSpeed) {
            var limitedVelocity = flatVelocity.normalized * currentMoveSpeed;
            rigidBody.velocity = new Vector3(limitedVelocity.x, rigidBody.velocity.y, limitedVelocity.z);
        }
    }

    private void Bounce() {
        if (moveInput.magnitude > 0.1f) {
            animationTime += Time.deltaTime * bounceFrequency;
            var scale = 1f + Mathf.Sin(animationTime) * bounceAmplitude;
            playerObject.localScale = new Vector3(1f, scale, 1f);
        } else {
            animationTime = 0f;
            playerObject.localScale = Vector3.one;
        }
    }
}
