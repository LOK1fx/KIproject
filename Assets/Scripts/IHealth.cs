public interface IHealth
{
    void AddHealth(int hp);
    void TakeDamage(Damage damage);
    int GetHealth();
}