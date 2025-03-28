using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject itemGoldCoin;
    [SerializeField] private GameObject itemPowerUp;
    [SerializeField] private GameObject itemBoom;

    public void ItemDrop()
    {
        float rand = Random.value;

        if (rand < 0.2f)
        {
            Debug.Log("Item has not been dropped");
        }
        else if (rand < 0.5f)
        {
            Instantiate(itemGoldCoin, transform.position, Quaternion.identity);
        }
        else if (rand < 0.8f)
        {
            Instantiate(itemPowerUp, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(itemBoom, transform.position, Quaternion.identity);
        }
    }
    
}
