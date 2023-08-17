using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image healthPrefab;
    [SerializeField] private float barPosition;
    Image healthFrame;
    Image healthBar;
    [SerializeField] private Sprite healthSprite;
    [SerializeField] private float health;
    [SerializeField] private float currentHealth;
    [HideInInspector] public ItemSlot itemSlot;
    float healthRegen;
    int armor;

    void Start()
    {
        healthFrame = Instantiate(healthPrefab, AllInOneSingleton.Ins.transform);
        healthBar = new List<Image>(healthFrame.GetComponentsInChildren<Image>()).Find(image => image != healthFrame);
        healthBar.sprite = healthSprite;        
        Restore();
    }
    void Restore() => currentHealth = health;
    void Update()
    {
        healthFrame.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0,barPosition,0));
        UpdateLife();
        Regen();
    }
    public void TakeDamage(int dmg)
    {
        dmg -= armor;
        if (dmg > 0)
        {
            currentHealth -= dmg;
            if (currentHealth <= 0) Die();
        }
        if(itemSlot != null) itemSlot.UpdateAmmoUI();
    }
    void UpdateLife()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, health);
        healthBar.fillAmount = currentHealth / health;
    }
    void Regen()
    {
        if (healthRegen == 0 || PlayerController.Ins.isDead) return;
        currentHealth += healthRegen * Time.deltaTime;
    }
    void Die()
    {
        PlayerController.Ins.isDead = true;
        GetComponent<Animator>().SetTrigger("Die");
    }
}
