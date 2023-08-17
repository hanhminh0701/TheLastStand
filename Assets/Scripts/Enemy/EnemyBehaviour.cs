using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{    
    [SerializeField] private float attackRange;
    [SerializeField] private int attackState;
    [SerializeField] protected LayerMask layer;
    [SerializeField] private int meleeDmg;
    [SerializeField] private string meleeHitTag;
    [SerializeField] protected ItemData attack;
 
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public bool isDead;    
    [HideInInspector] public Animator animator;    
    bool isStop;
    Vector3 direction;
    float distance;

    private void Start()
    {        
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
        if (isDead || PlayerController.Ins.isDead)
        {
            animator.ResetTrigger("Attack");
            agent.isStopped = true;
            return;
        }                
        if (PlayerInAttackRange())
            Attack();
        else if(!isStop) Chase();
    }
    void Attack()
    {
        transform.rotation = Quaternion.LookRotation(direction);
        isStop = true;
        agent.isStopped = true;
        if (attackState > 1) animator.SetInteger("State", Random.Range(0, attackState));
        animator.SetTrigger("Attack");
    }
    void Chase()
    {
        agent.isStopped = false;
        animator.ResetTrigger("Attack");
        agent.SetDestination(PlayerController.Ins.transform.position);
    }
    bool PlayerInAttackRange()
    {
        distance = Vector3.Distance(PlayerController.Ins.transform.position, transform.position);
        if (distance <= attackRange)
        {
            direction = PlayerController.Ins.transform.position - transform.position;
            if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), direction, out RaycastHit hit, layer) && hit.collider.tag == "Player")
                return true;
        }
        return false;
    }
    void MeleeAttack()
    {
        var hit = ObjectPooler.Ins.SpawnFromPool(meleeHitTag, PlayerController.Ins.transform.position + new Vector3(0,1f,0));
        hit.transform.SetParent(PlayerController.Ins.transform);
        hit.GetComponent<ParticleSystem>().Play();
        PlayerController.Ins.GetComponent<PlayerHealth>().TakeDamage(meleeDmg);
    }
    void RangeAttack() => ObjectPooler.Ins.SpawnFromPool(attack.tag, transform.position + new Vector3(0, 1f, 0), PlayerController.Ins.transform);
    void AbleToMove() => isStop = false;
}
