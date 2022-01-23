public struct Damage
{
    public enum Type
    {
        Normal,
        Lazer,
        Void
    }

    public Actor Sender { get; private set; }
    public Type DamageType { get; private set; }
    public int Value { get; private set; }

    public Damage(int value)
    {
        Sender = null;
        Value = value;
        DamageType = Type.Normal;
    }

    public Damage(int value, Type type)
    {
        Sender = null;
        Value = value;
        DamageType = type;
    }

    public Damage(int value, Type type, Actor sender)
    {
        Sender = sender;
        Value = value;
        DamageType = type;
    }

    public Damage(int value, Actor sender)
    {
        Sender = sender;
        Value = value;
        DamageType = Type.Normal;
    }
}
