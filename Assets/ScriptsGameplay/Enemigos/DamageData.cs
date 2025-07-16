
//Consigna: Enum
public class DamageData
{
    public int Amount;
    public DamageType Type;

    public DamageData(int amount, DamageType type)
    {
        Amount = amount;
        Type = type;
    }
}
public enum DamageType
{
    Bullet,
    Fire,
    // Explosion,
    // Poison
}