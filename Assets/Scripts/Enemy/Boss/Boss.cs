using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public Rigidbody2D Rb2d { get; private set; }
    public GameObject Target { get; set; }
    
    private BossStateMachine stateMachine;
    private void Awake()
    {
        stateMachine = new BossStateMachine(this);
        Animator = GetComponent<Animator>();
        Rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.EnterState);
    }

    private void Update()
    {
        stateMachine.Execute();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(stateMachine.PlayerBullet)) // 플레이어 총알
        {
            Bullet bullet = other.GetComponent<Bullet>();
            stateMachine.CurrentState.TakeDamaged(bullet.damage);
            other.GetComponent<ReturnObject>()?.ReturnObj(); // => 총알 반환
        }
    }

    public void RunCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
