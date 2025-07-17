using System.Collections.Generic;

[System.Serializable]
public class ItemLogEntry {
    public string itemName;
}

[System.Serializable]
public class ItemLog {
    public List<ItemLogEntry> entries = new();
}