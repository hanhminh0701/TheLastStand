using UnityEngine;

public class EnemyTargetBullet : TargetBullet
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy" && other.tag != "Bullet" && other.tag != "Item")
        {
            Impact();
            if (other.TryGetComponent(out PlayerHealth player)) player.TakeDamage(bullet.damage);
        }
    }
}
