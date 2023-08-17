using UnityEngine;

public class CurveBullet : BulletBehaviour
{
    Vector3 startPosition;
    Vector3 endPosition;
    Vector3 direction;
    float startTime;
    float distance;
    public override void OnSpawn(Transform target)
    {
        startPosition = transform.position;
        endPosition = target.position + new Vector3(0, 1f, 0);
        startTime = Time.time;
        distance = Vector3.Distance(startPosition, endPosition);
        direction = (target.position - startPosition).normalized;        
    }
    protected override void FixedUpdate()
    {       
        transform.Rotate(-180f * Time.fixedDeltaTime, -180f * Time.fixedDeltaTime, 0);
        float elapsedTime = Time.time - startTime;
        float dx = bullet.speed * elapsedTime;
        float dy = AllInOneSingleton.Ins.curve.Evaluate(dx / distance);
        transform.position = startPosition + dx * direction + new Vector3(0, dy * distance / 2f, 0);
        if (transform.position == endPosition) Impact();
    }
}
