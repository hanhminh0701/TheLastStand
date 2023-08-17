using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicGun : MonoBehaviour
{
    [SerializeField] protected Image cooldownImage;
    [SerializeField] protected Image icon;
    [SerializeField] protected LayerMask layer;
    public ItemData item;
    protected bool isCooldown;
    protected List<Collider> enemies = new List<Collider>();
    Vector3 direction;

    protected virtual void Start()
    {
        icon.sprite = item.icon;
        cooldownImage.fillAmount = 0;
    }
    protected virtual void Update()
    {       
        if(PlayerController.Ins.isDead) return;
        if (isCooldown)
        {
            cooldownImage.fillAmount -= 1 / item.cooldown * Time.deltaTime;            
        }
        if (cooldownImage.fillAmount <= 0)
        {
            cooldownImage.fillAmount = 0;
            isCooldown = false;
            if (HasEnemyInRange())
            {
                var index = Random.Range(0, enemies.Count);
                item.Shoot(enemies[index].transform);
                Cooldown();
                enemies.Clear();
            }
        }
    }
    protected bool HasEnemyInRange()
    {
        Collider[] enemyColliders = Physics.OverlapSphere(PlayerController.Ins.transform.position, item.range, layer);                
        foreach (Collider collider in enemyColliders)
        {
            direction = (collider.transform.position - PlayerController.Ins.transform.position).normalized;
            if (Physics.Raycast(PlayerController.Ins.transform.position + new Vector3(0,1f,0), direction, out RaycastHit hit) && hit.collider.tag == "Enemy") 
                enemies.Add(collider);
        }
        if (enemies.Count == 0) return false;
        return true;
    }
    protected void Cooldown()
    {
        isCooldown = true;
        cooldownImage.fillAmount = 1;
    }
}
