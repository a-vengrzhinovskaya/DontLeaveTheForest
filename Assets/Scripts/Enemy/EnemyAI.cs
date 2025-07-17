using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    [Header("Behavior")]
    public Transform playerObject;
    public Player player;
    public float killDistance = 1.5f;
    public float defaultMoveSpeed = 3f;
    public float chaseMoveSpeed = 20f;
    public float safeBorderDistance = 0.5f;

    [Header("Area")]
    public float areaLimit = 13f;
    public float borderOffset = 3f;
    public float viewDistance = 1f;

    [Header("Bounce")]
    public float bounceAmplitude;
    public float bounceFrequency;

    private float animationTime;

    private List<Vector3> patrolPoints;
    private int currentPatrolIndex = 0;
    private bool chasing = false;
    private float currentMoveSpeed;

    private bool isPlayerOutOfBounds;
    private bool isPlayerNearBorder;

    private void Start() {
        GeneratePatrolPoints();
    }

    private void Update() {
        if (player.IsDead) {
            return;
        }

        CheckPlayerPosition();

        if (isPlayerOutOfBounds) {
            chasing = true;
            currentMoveSpeed = chaseMoveSpeed;
            TryToKillPlayer();
        } else {
            chasing = isPlayerNearBorder;
            currentMoveSpeed = defaultMoveSpeed;
        }

        if (chasing) {
            ChasePlayer();
        } else {
            Patrol();
        }
    }

    private void CheckPlayerPosition() {
        float absX = Mathf.Abs(playerObject.position.x);
        float absZ = Mathf.Abs(playerObject.position.z);

        isPlayerOutOfBounds = !(absX < areaLimit && absZ < areaLimit);

        isPlayerNearBorder =
            (absX >= areaLimit - viewDistance && absX < areaLimit) ||
            (absZ >= areaLimit - viewDistance && absZ < areaLimit);
    }

    private void TryToKillPlayer() {
        var distanceToPlayer = Vector3.Distance(transform.position, playerObject.position);
        if (distanceToPlayer < killDistance) {
            KillPlayer();
        }
    }

    private void KillPlayer() {
        player.takeDamage(player.maxHealth);
        currentMoveSpeed = defaultMoveSpeed;

    }

    private void ChasePlayer() {
        var targetPosition = playerObject.position;

        //restrict enemy from entering player's area
        if (!isPlayerOutOfBounds) {
            var clampedX = Mathf.Clamp(playerObject.position.x, -areaLimit, areaLimit);
            var clampedZ = Mathf.Clamp(playerObject.position.z, -areaLimit, areaLimit);

            if (Mathf.Abs(playerObject.position.x) >= areaLimit - viewDistance) {
                clampedX = Mathf.Sign(playerObject.position.x) * (areaLimit + safeBorderDistance);
            }

            if (Mathf.Abs(playerObject.position.z) >= areaLimit - viewDistance) {
                clampedZ = Mathf.Sign(playerObject.position.z) * (areaLimit + safeBorderDistance);
            }

            targetPosition = new Vector3(clampedX, playerObject.position.y, clampedZ);
        }

        var distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        if (distanceToTarget > safeBorderDistance * 0.9f) { //stopping enemy from running into area border
            MoveTowards(targetPosition);
        } else {
            var targetDirection = (playerObject.position - transform.position).normalized;
            targetDirection.y = 0f;

            if (targetDirection != Vector3.zero) {
                transform.forward = Vector3.Slerp(transform.forward, targetDirection, Time.deltaTime * 5f);
            }

            Bounce(false);
        }
    }

    private void Patrol() {
        Vector3 target = patrolPoints[currentPatrolIndex];
        if (Vector3.Distance(transform.position, target) < 0.3f) {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }

        MoveTowards(target);
    }

    private void GeneratePatrolPoints() {
        var patrolY = transform.position.y;
        var patrolDistance = areaLimit + borderOffset;

        patrolPoints = new List<Vector3> {
            new Vector3(-patrolDistance, patrolY,  patrolDistance),
            new Vector3( 0, patrolY,  patrolDistance),
            new Vector3( patrolDistance, patrolY,  patrolDistance),
            new Vector3( patrolDistance, patrolY,  0),
            new Vector3( patrolDistance, patrolY, -patrolDistance),
            new Vector3( 0, patrolY, -patrolDistance),
            new Vector3(-patrolDistance, patrolY, -patrolDistance),
            new Vector3(-patrolDistance, patrolY,  0)
        };
    }

    private void MoveTowards(Vector3 target) {
        var moveDirection = (target - transform.position).normalized;
        var nextPosition = transform.position + moveDirection * currentMoveSpeed * Time.fixedDeltaTime;
        transform.position += moveDirection * currentMoveSpeed * Time.deltaTime;    

        var isMoving = moveDirection != Vector3.zero;
        if (isMoving) {
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * 5f);
        }
        Bounce(isMoving);
    }

    private void Bounce(bool isMoving) {
        if (isMoving) {
            animationTime += Time.deltaTime * bounceFrequency;
            var scale = 1f + Mathf.Sin(animationTime) * bounceAmplitude;
            transform.localScale = new Vector3(1f, scale, 1f);
        } else {
            animationTime = 0f;
            transform.localScale = Vector3.one;
        }
    }
}
