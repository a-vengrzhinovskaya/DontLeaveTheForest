using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    public GameObject bonusPrefab;
    public GameObject trapPrefab;
    public GameObject notePrefab;

    public int bonusCount = 3;
    public int trapCount = 3;
    public int noteCount = 4;

    public float spawnAreaSize = 25f;
    public float minDistanceBetweenItems = 2f;

    public List<string> noteTexts;

    private List<Vector3> spawnedPositions = new();

    void Start() {
        SpawnItems(bonusPrefab, bonusCount);
        SpawnItems(trapPrefab, trapCount);
        SpawnNotes();
    }

    void SpawnNotes() {
        for (int noteNumber = 0; noteNumber < Mathf.Min(noteCount, noteTexts.Count); ++noteNumber) {
            var position = GetValidPosition();
            var note = Instantiate(notePrefab, position, notePrefab.transform.rotation); //note must be on ground

            var noteScript = note.GetComponent<Note>();
            noteScript.text = noteTexts[noteNumber];
        }
    }

    void SpawnItems(GameObject prefab, int count) {
        for (int itemNumber = 0; itemNumber < count; ++itemNumber) {
            var position = GetValidPosition();
            Instantiate(prefab, position, Quaternion.identity);
        }
    }

    private Vector3 GetValidPosition() {
        int maxAttempts = 100;
        for (int attempt = 0; attempt < maxAttempts; attempt++) {
            var half = spawnAreaSize / 2f;
            var possibleSpawnPosition = new Vector3(
                Random.Range(-half, half),
                0.03f, //to prevent clipping
                Random.Range(-half, half)
            );

            if (IsPositionAvailable(possibleSpawnPosition)) {
                spawnedPositions.Add(possibleSpawnPosition);
                return possibleSpawnPosition;
            }
        }

        return Vector3.zero;
    }

    private bool IsPositionAvailable(Vector3 positon) {
        foreach (var takenPosition in spawnedPositions) {
            if (Vector3.Distance(takenPosition, positon) < minDistanceBetweenItems) return false;
        }

        var colliders = Physics.OverlapSphere(positon + Vector3.up * 0.5f, 0.5f);
        foreach (var collider in colliders) {
            if (collider.CompareTag("Obstacle") || collider.CompareTag("Player")) {
                return false;
            }
        }

        return true;
    }
}

