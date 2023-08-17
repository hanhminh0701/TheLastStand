

using UnityEngine;

public class Fireball : TargetBullet
{
    protected override void Impact()
    {
        var hit = ObjectPooler.Ins.SpawnFromPool(bullet.impactTag, transform.position);
        hit.GetComponent<ParticleSystem>().Play();
        Invoke(nameof(Deactive), 1f);
    }
    void Deactive() => gameObject.SetActive(false);
}
