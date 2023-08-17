using UnityEngine;

public class ExplodeBullet : CurveBullet
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask layer;
    Vector3 explodeDirection;
    protected override void Impact()
    {
        base.Impact();
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layer);
        foreach (Collider collider in colliders)
        {
            explodeDirection = (collider.transform.position - transform.position).normalized;
            if (Physics.Raycast(transform.position, explodeDirection, out RaycastHit hit) && hit.collider.tag == "Enemy")
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(bullet.damage/2);
        }
    }
}
