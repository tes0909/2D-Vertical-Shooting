using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject itemGoldCoin;
    [SerializeField] private GameObject itemPowerUp;
    [SerializeField] private GameObject itemBoom;
    [SerializeField] private float dropItemSpeed;
    public void ItemDrop()
    {
        float rand = Random.value;

        if (rand < 0.2f)
        {
            Debug.Log("Item has not been dropped");
        }
        else if (rand < 0.5f)
        {
            DropItem(ObjectManager.PoolType.ItemGoldCoin);
        }
        else if (rand < 0.8f)
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
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * dropItemSpeed;
    }
}
