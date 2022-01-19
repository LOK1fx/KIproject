public abstract class BonusModifier : Modifier
{
    public Bonus Bonus { get; private set; }

    public BonusModifier(Bonus bonus)
    {
        Bonus = bonus;
    }
}