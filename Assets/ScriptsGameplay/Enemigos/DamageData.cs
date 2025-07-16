
//Consigna: Enum
public class DamageData
{
    private int _amount;
    private DamageType _type;

    public int Amount => _amount;
    public DamageType Type => _type;

    public DamageData(int amount, DamageType type)
    {
        _amount = amount;
        _type = type;
    }
}
public enum DamageType
{
    Bullet,
    Fire,
    // Explosion,
    // Poison
}