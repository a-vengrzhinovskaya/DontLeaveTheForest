public class Bonus : Item {
    public float duration = 3f;
    public float effectStrength = 1.5f;

    protected override void TakeEffect() {
        player.getSpeedBoost(duration, effectStrength);
    }
}
