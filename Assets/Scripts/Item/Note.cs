public class Note : Item {
    public string text;

    private string itemName = "Note";

    protected override void TakeEffect() {
        ItemLogger.instance.AddEntry(itemName);
        GameManager.instance.ShowNote(text);
    }
}
