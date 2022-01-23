public class HPBonusModifier : BonusModifier
{
    public HPBonusModifier(Bonus bonus) : base(bonus)
    {

    }

    public override void Apply(Player player)
    {
        if (player.GetHealth() < player.MaxHealth)
        {
            player.AddHealth(1);
        }      
    }
}