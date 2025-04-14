using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;

    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("DataManager");
                    _instance = obj.AddComponent<DataManager>();
                }
            }
            return _instance;
        }
    }
    

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    public void SaveData<T>(T saveData)
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + $"/{typeof(T)}.txt", json);
    }
    
    public T LoadData<T>()
    {
        string loadData = File.ReadAllText(Application.persistentDataPath + $"/{typeof(T)}.txt");
        return JsonUtility.FromJson<T>(loadData);
    }

    [Serializable]
    public class MonsterDataList
    {
        public List<MonsterData> monsterData = new List<MonsterData>();
    }

    [Serializable]
    public class MonsterData
    {
        public float spawnDelay;
        public int spawnPoint;
        public ObjectManager.PoolType type;
    }
    
}
