using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    [Header("Game Over")]
    public GameObject gameOverUI;

    [Header("Notes")]
    public GameObject noteUI;
    public TMPro.TextMeshProUGUI noteText;

    private void Start() {
        //LockCursor();
    }

    public void GameOver() {
        noteUI.SetActive(false);
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        //UnlockCursor();
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void ShowNote(string message) {
        //UnlockCursor();
        noteUI.SetActive(true);
        noteText.text = message;
    }

    public void HideNote() {
        //LockCursor();
        noteUI.SetActive(false);
    }

    private void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
