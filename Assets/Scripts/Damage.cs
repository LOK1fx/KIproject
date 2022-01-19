public struct Damage
{
    public enum Type
    {
        Normal,
        Lazer,
        Void
    }

    public Type DamageType { get; private set; }
    public int Value { get; private set; }

    public Damage(int value)
    {
        Value = value;
        DamageType = Type.Normal;
    }

    public Damage(int value, Type type)
    {
        Value = value;
        DamageType = type;
    }

    public void SetDamageType(Type type)
    {
        DamageType = type;
    }

    public void SetDamageValue(int value)
    {
        Value = value;
    }
}
