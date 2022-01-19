public class CoinBonusModifier : BonusModifier
{
    public CoinBonusModifier(Bonus bonus) : base(bonus)
    {
    }

    public override void Apply(Player player)
    {
        if (player.TryGetPawnController<PlayerController>(out var controller))
        {
            controller.AddScore(1);
        }
    }
}