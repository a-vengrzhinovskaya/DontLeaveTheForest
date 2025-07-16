public class Trap : Item {
    public int damage = 1;

    public Trap(Player _player) : base(_player) { }

    protected override void TakeEffect() {
        player.takeDamage(damage);
    }
}
