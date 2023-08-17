using UnityEngine;

public class Interaction : MonoBehaviour
{
    public ItemData item;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            AllInOneSingleton.Ins.EquipItem(gameObject);
    }
}
