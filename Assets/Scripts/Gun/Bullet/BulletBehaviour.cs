using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public ItemData bullet;      
    public virtual void OnSpawn(Transform target) { }    
    protected virtual void FixedUpdate() { }      
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Bullet" && other.tag != "Item")
        {
            Impact();
            if (other.TryGetComponent(out EnemyHealth enemy)) enemy.TakeDamage(bullet.damage);
        }              
    }
    protected virtual void Impact()
    {
        var hit = ObjectPooler.Ins.SpawnFromPool(bullet.impactTag, transform.position);
        hit.GetComponent<ParticleSystem>().Play();
        gameObject.SetActive(false);
    }
}
