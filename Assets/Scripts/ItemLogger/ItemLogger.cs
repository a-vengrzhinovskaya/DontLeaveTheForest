using System.IO;
using UnityEngine;

public class ItemLogger : MonoBehaviour {
    public static ItemLogger instance;

    public ItemLog currentLog = new();
    public ItemLog previousLog = new();

    private string savePath;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        savePath = Path.Combine(Application.persistentDataPath, "item_log.json");
        LoadPreviousLog();
    }

    public void AddEntry(string itemName) {
        var entry = new ItemLogEntry {
            itemName = itemName
        };
        currentLog.entries.Add(entry);
    }

    private void LoadPreviousLog() {
        if (File.Exists(savePath)) {
            var json = File.ReadAllText(savePath);
            previousLog = JsonUtility.FromJson<ItemLog>(json);
        }
    }

    public void SaveCurrentLog() {
        var json = JsonUtility.ToJson(currentLog, true);
        File.WriteAllText(savePath, json);
    }
}
