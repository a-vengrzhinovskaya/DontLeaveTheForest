public class Bonus : Item {
    public float duration = 3f;
    public float effectStrength = 1.5f;

    private string itemName = "Carrot";

    protected override void TakeEffect() {
        ItemLogger.instance.AddEntry(itemName);
        player.receiveBoost(duration, effectStrength);
    }
}
