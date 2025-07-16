//TP2 - Martin Bielenia

// This class is the interface used for player to damage enemies with objects under the layer "Enemies".
//Consigna: Generic
public interface IDamageEnemy<T>
{
    void TakeHit(T damage);
}
