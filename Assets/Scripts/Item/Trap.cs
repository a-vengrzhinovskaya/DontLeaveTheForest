public class Trap : Item {
    public int damage = 1;

    protected override void TakeEffect() {
        player.takeDamage(damage);
    }
}
