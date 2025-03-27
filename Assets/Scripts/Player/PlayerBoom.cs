using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoom : MonoBehaviour
{
    private PlayerInputReceiver _playerInputReceiver;
    void Awake()
    {
        _playerInputReceiver = GetComponent<PlayerInputReceiver>();
    }

    private void OnEnable()
    {
        _playerInputReceiver.OnBoomEvent += Boom;
    }
    
    private void OnDisable()
    {
        _playerInputReceiver.OnBoomEvent -= Boom;
    }

    private void Boom()
    {
        
    }
}
