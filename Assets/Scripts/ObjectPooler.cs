using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Singleton
    public static ObjectPooler Ins;
    private void Awake() => Ins = this;
    #endregion
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
    private void Start()
    {
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i < pool.size; i++)
            {
                var obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Transform target)
    {
        var objectToSpawn = poolDictionary[tag].Dequeue();  
        objectToSpawn.transform.position = position;
        objectToSpawn.GetComponent<BulletBehaviour>().OnSpawn(target);
        objectToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        var objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;        
        objectToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
