using UnityEngine;

public class TargetBullet : BulletBehaviour
{
    protected Transform target;
    public override void OnSpawn(Transform target) => this.target = target;
    protected override void FixedUpdate()
    {        
        transform.position = Vector3.MoveTowards(transform.position, target.position + new Vector3(0, 1f, 0), bullet.speed * Time.fixedDeltaTime);
        transform.LookAt(target.position + new Vector3(0,1f,0));
        if(transform.position == target.position + new Vector3(0, 1f, 0)) gameObject.SetActive(false);
    }
}
