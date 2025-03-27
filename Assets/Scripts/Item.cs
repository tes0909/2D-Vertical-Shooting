using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Coin,
        Power,
        Boom
    }
    
    [SerializeField] private ItemType type;
    [SerializeField] private float itemSpeed = 0.1f;
    [SerializeField] private int itemCoinScore = 1000;
    [SerializeField] private int itemPowerScore = 500;
    [SerializeField] private int itemBoomScore = 500;
    public int CoinScore => itemCoinScore;
    public int PowerScore => itemPowerScore; 
    public int BoomScore => itemBoomScore;
    
    private Rigidbody2D _rigidbody2D;
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = Vector2.down * itemSpeed;
    }

    public ItemType GetItemType()
    {
        return type;
    }
}
