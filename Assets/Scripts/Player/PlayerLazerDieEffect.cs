public class PlayerLazerDieEffect : PlayerDeathEffect
{
    protected override void OnPlayerDie(Damage.Type type)
    {
        if(type == Damage.Type.Lazer)
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
