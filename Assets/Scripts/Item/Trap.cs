public class Trap : Item {
    public int damage = 1;

    private string itemName = "Trap";

    protected override void TakeEffect() {
        ItemLogger.instance.AddEntry(itemName);
        player.takeDamage(damage);
    }
}
