using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class AllInOneSingleton : MonoBehaviour
{
    #region Singleton
    public static AllInOneSingleton Ins;
    private void Awake()
    {
        Ins = this;
    }
    #endregion
        
    public AnimationCurve curve;
    public ItemSlot[] itemSlots;
    public TextMeshProUGUI scoreText;
    int score;
    public void EquipItem(GameObject item)
    {
        var itemData = item.GetComponent<Interaction>().item;
        if (itemData.Type == ItemType.Armor && ContainsArmor(itemData)) return; 
        if (HasFreeSlot(out ItemSlot freeSlot))
        {
            freeSlot.UpdateItemSlot(itemData);
            item.SetActive(false);
        }            
    }
    private bool ContainsArmor(ItemData item)
    {
        var itemSlot = itemSlots.FirstOrDefault(slot => slot.item == item);
        return itemSlot == null ? false : true;

    }
    bool HasFreeSlot(out ItemSlot freeSlot)
    {
        freeSlot = itemSlots.FirstOrDefault(slot => slot.item == null);
        return freeSlot == null ? false : true;
    }
    public void UpdateScore(int enemyScore)
    {
        score += enemyScore;
        scoreText.text = "Score: " + score.ToString();
    }
}
