using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : BasicGun
{    
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private Sprite defaultIcon;    
    int currentAmmo;
    
    protected override void Start() => ClearSlot();
    protected override void Update()
    {
        if (item == null || item.Type == ItemType.Armor || PlayerController.Ins.isDead) return;
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
                UpdateAmmoUI();
                enemies.Clear();
            }
        }
    }    
    public void UpdateItemSlot(ItemData item)
    {        
        this.item = item;
        currentAmmo = item.ammo;
        icon.sprite = item.icon;
        ammoText.text = currentAmmo.ToString();
        ammoText.enabled = true;
        if (item.Type == ItemType.Armor) PlayerController.Ins.GetComponent<PlayerHealth>().itemSlot = this;
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = defaultIcon;        
        ammoText.enabled = false;
        cooldownImage.fillAmount = 0;
    }    
    public void UpdateAmmoUI()
    {
        currentAmmo--;
        if (currentAmmo <= 0)
        {
            ClearSlot();
            return;
        }
        ammoText.text = currentAmmo.ToString();        
        Cooldown();
    }
}
