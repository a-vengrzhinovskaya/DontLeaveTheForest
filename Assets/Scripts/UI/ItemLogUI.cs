using UnityEngine;
using TMPro;

public class ItemLogUI : MonoBehaviour {
    public GameObject logUI;
    public Transform currentItemList;
    public Transform previousItemList;
    public GameObject logItemPrefab;

    private int lastEntryCount = 0;

    private void Update() {
        if (logUI.activeSelf) {
            var currentLog = ItemLogger.instance.currentLog;
            if (currentLog.entries.Count != lastEntryCount) {
                FillList(currentItemList, currentLog);
                lastEntryCount = currentLog.entries.Count;
            }
        }
    }

    public void ChangeLogVisibility() {
        logUI.SetActive(!logUI.activeSelf);
        if (logUI.activeSelf) {
            var currentLog = ItemLogger.instance.currentLog;
            var previousLog = ItemLogger.instance.previousLog;

            FillList(currentItemList, currentLog);
            FillList(previousItemList, previousLog);
            lastEntryCount = currentLog.entries.Count;
        }
    }

    private void FillList(Transform view, ItemLog log) {
        foreach (Transform entry in view) {
            Destroy(entry.gameObject);
        }

        foreach (var entry in log.entries) {
            var entryGameObject = Instantiate(logItemPrefab, view);
            var textComponent = entryGameObject.GetComponentInChildren<TMP_Text>();
            if (textComponent != null) {
                textComponent.text = entry.itemName;
            } else {
                Debug.LogWarning($"no text on prefab. entry {entry.itemName}");
            }
        }
    }
}
