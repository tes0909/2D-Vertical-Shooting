using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject itemGoldCoin;
    [SerializeField] private GameObject itemPowerUp;
    [SerializeField] private GameObject itemBoom;
    public void ItemDrop()
    {
        float rand = Random.value;

        if (rand < 0.25f)
        {
            Debug.Log("Item has not been dropped");
        }
        else if (rand < 0.55f)
        {
            DropItem(ObjectManager.PoolType.ItemGoldCoin);
        }
        else if (rand < 0.7f)
        {
            DropItem(ObjectManager.PoolType.ItemPowerUp);
        }
        else
        {
            DropItem(ObjectManager.PoolType.ItemBoom);
        }
    }

    private void DropItem(ObjectManager.PoolType poolType)
    {
        GameObject item = ObjectManager.Instance.GetObject(poolType);
        item.transform.position = transform.position;
    }
}
