using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D Rb2d { get; private set; }
    public PlayerInputReceiver PlayerInputReceiver { get; private set; }
    
    private void Awake()
    {
        Rb2d  = GetComponent<Rigidbody2D>();
        PlayerInputReceiver = GetComponent<PlayerInputReceiver>();
    }
}
