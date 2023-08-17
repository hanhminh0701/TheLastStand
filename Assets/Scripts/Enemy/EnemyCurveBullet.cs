using UnityEngine;

public class EnemyCurveBullet : CurveBullet
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy" && other.tag != "Bullet")
        {
            Impact();
            if (other.TryGetComponent(out PlayerHealth player)) player.TakeDamage(bullet.damage);
        }        
    }
}
