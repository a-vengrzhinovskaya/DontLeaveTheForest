public class Note : Item {
    public string text;

    protected override void TakeEffect() {
        GameManager.instance.ShowNote(text);
    }
}
