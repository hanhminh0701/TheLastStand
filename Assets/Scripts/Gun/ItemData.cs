using UnityEngine;
public enum ItemType { Gun, Armor}

[CreateAssetMenu(menuName = "Game/Item")]
public class ItemData: ScriptableObject
{
    public ItemType Type;
    public string tag;
    public string impactTag;
    public int ammo;    
    public Sprite icon;

    [Header("Gun")]
    public int damage;
    public float speed;
    public float range;
    public float cooldown;

    [Header("Armor")]
    public int armor;
    public int regen;

    public void Shoot(Transform target) => ObjectPooler.Ins.SpawnFromPool(tag, PlayerController.Ins.transform.position + new Vector3(0, 1f, 0), target.transform);

}
