using UnityEngine;

public class DefaultShootEnchantment : Enchantment
{
    private Bullet _cloneBullet;

    public override bool OnHit(Health health, float damage)
    {
        return false;
    }

    public override bool OnShoot(float lookAngle, Wand wand, int index)
    {
        _cloneBullet = Instantiate(wand.BulletPrefab);
        _cloneBullet.Init(wand, index);
        _cloneBullet.transform.position = wand.ShootPoint.position;
        _cloneBullet.transform.rotation = Quaternion.Euler(StaticConstants.Zero, StaticConstants.Zero, lookAngle);
        StartCoroutine(WaitForShoot(lookAngle, index));
        return true;
    }
}
