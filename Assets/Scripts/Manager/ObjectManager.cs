using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance { get; private set; }
    
    public enum PoolType
    {
        EnemyMini1,
        EnemyElite1,
        EnemyElite2,
        PlayerBullet1,
        PlayerBullet2,
        EnemyBullet1,
        EnemyBullet2,
        ItemGoldCoin,
        ItemPowerUp,
        ItemBoom
    }
    [System.Serializable]
    public class Pool
    {
        public PoolType key;
        public GameObject prefab;
        public int size;
    }
    
    public List<Pool> PoolList;
    private Dictionary<PoolType, Queue<GameObject>> PoolDictionary = new Dictionary<PoolType, Queue<GameObject>>();
    private Dictionary<PoolType, List<GameObject>> ActiveObject = new Dictionary<PoolType, List<GameObject>>(); // 활성화 오브젝트 리스트
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (var pool in PoolList)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            List<GameObject> list = new List<GameObject>(); // 활성화 오브젝트(List) 타입 생성
            
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            PoolDictionary.Add(pool.key, queue);
            ActiveObject.Add(pool.key, list);
        }
    }

    public GameObject GetObject(PoolType key)
    {
        if (PoolDictionary.ContainsKey(key) && PoolDictionary[key].Count > 0)
        {
            GameObject obj = PoolDictionary[key].Dequeue();
            obj.SetActive(true);
            ActiveObject[key].Add(obj);
            return obj;
        }

        return null;
    }

    public void ReturnObject(PoolType key, GameObject obj)
    {
        if(!PoolDictionary.ContainsKey(key)) return;
        
        obj.SetActive(false);
        PoolDictionary[key].Enqueue(obj);
        ActiveObject[key].Remove(obj);
    }

    // 해당 풀 타입의 모든 오브젝트를 가져옴
    public GameObject[] GetObjects(PoolType key)
    {
        return ActiveObject[key].ToArray(); // 큐를 배열로 반환
    }
}
