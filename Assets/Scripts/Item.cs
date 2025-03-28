using System;
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
    [SerializeField] private float dropItemSpeed = 1f;
    public int CoinScore => itemCoinScore;
    public int PowerScore => itemPowerScore; 
    public int BoomScore => itemBoomScore;
    
    private Rigidbody2D _rb2d;
    
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rb2d.velocity = Vector2.down * dropItemSpeed;
    }

    public ItemType GetItemType()
    {
        return type;
    }
}
