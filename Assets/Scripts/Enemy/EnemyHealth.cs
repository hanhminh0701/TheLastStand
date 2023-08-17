using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Image healthPrefab;
    [SerializeField] private float barPosition;
    Image healthFrame;
    Image healthBar;
    [SerializeField] private Sprite healthSprite;
    [SerializeField] protected float health;
    [SerializeField] private int score;
    protected float currentHealth;
    EnemyBehaviour enemyBehaviour;
    
    void Start()
    {
        healthFrame = Instantiate(healthPrefab, AllInOneSingleton.Ins.transform);
        healthBar = new List<Image>(healthFrame.GetComponentsInChildren<Image>()).Find(image => image != healthFrame);
        healthBar.sprite = healthSprite;
        Restore();
        enemyBehaviour = GetComponent<EnemyBehaviour>();               
    }
    void Restore() => currentHealth = health;
    void Update()
    {
        healthFrame.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, barPosition, 0));
        UpdateLife();
    }
    void UpdateLife()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, health);
        healthBar.fillAmount = currentHealth / health;
    }
    public void TakeDamage(int dmg)
    {        
        currentHealth -= dmg;
        if (currentHealth <= 0) Die();
    }           
    void Die()
    {
        AllInOneSingleton.Ins.UpdateScore(score);
        healthFrame.enabled = false;
        GetComponent<Collider>().enabled = false;
        enemyBehaviour.isDead = true;
        enemyBehaviour.animator.SetTrigger("Die");
        enemyBehaviour.agent.isStopped = true;
        Invoke(nameof(DeActive), 4f);
    }
    void DeActive() => gameObject.SetActive(false);
}
