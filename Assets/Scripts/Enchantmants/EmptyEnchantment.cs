public class EmptyEnchantment : Enchantment
{
    public override bool OnHit(Health health, float damage)
    {
        return false;
    }

    public override bool OnShoot(float lookAngle, Wand wand, int index)
    {
        StartCoroutine(WaitForShoot(lookAngle, index));
        return false;
    }
}
