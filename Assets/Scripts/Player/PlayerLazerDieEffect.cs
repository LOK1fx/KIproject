public class PlayerLazerDieEffect : PlayerDeathEffect
{
    protected override void OnPlayerDie(Damage damage)
    {
        if(damage.DamageType == Damage.Type.Lazer)
        {
            var effect = Instantiate(effectPrefab, transform.position, transform.rotation);

            Destroy(effect, 2f);
        }
        else
        {
            var effect = Instantiate(effectPrefab, transform.position, transform.rotation);

            Destroy(effect, 2f);
        }
    }
}
