public abstract class BonusModifier : Modifier
{
    public Bonus Bonus { get; private set; }
    public bool MakePlayerInvicible;
    public bool CanStuck = true;

    public BonusModifier(Bonus bonus)
    {
        Bonus = bonus;
    }
}