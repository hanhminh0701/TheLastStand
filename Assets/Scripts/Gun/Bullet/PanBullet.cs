using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanBullet : TargetBullet
{
    int chain;
    List<Collider> enemies = new List<Collider>();
    [SerializeField] private LayerMask layer;
    public override void OnSpawn(Transform target)
    {
        base.OnSpawn(target);
        chain = 2;
    }
    protected override void FixedUpdate()
    {        
        transform.position = Vector3.MoveTowards(transform.position, target.position + new Vector3(0, 1f, 0), bullet.speed * Time.fixedDeltaTime);
        if (transform.position == target.position + new Vector3(0, 1f, 0)) Impact();
        transform.Rotate(0,-480*Time.fixedDeltaTime,0);
    }
    protected override void Impact()
    {
        var hit = ObjectPooler.Ins.SpawnFromPool(bullet.impactTag, transform.position);
        hit.GetComponent<ParticleSystem>().Play();

        if (chain > 0 && HasAnotherEnemyInRange())
        {
            var index = Random.Range(0, enemies.Count);
            target = enemies[index].transform;
            chain--;
            enemies.Clear();
        }
        else gameObject.SetActive(false);
    }
    bool HasAnotherEnemyInRange()
    {        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, bullet.range, layer);
        enemies = hitColliders.ToList();
        enemies.Remove(target.GetComponent<Collider>());
        //foreach (Collider collider in hitColliders)
        //{
        //    if (collider.tag == "Enemy") enemies.Add(collider);
        //    enemies.Remove(target.GetComponent<Collider>());
        //}
        if (enemies.Count == 0) return false;
        return true;
    }
}
