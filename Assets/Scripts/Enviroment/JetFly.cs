using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetFly : MonoBehaviour
{
    [SerializeField] private float startY = -3.5f;
    [SerializeField] private float endY = 6f;
    [SerializeField] private float speed = 10f;
    void Start()
    {
        ResetPosition();
    }

    void Update()
    {
        Move();
    }

    void ResetPosition()
    {
        Vector3 pos = transform.position;
        pos.y = startY;
        transform.position = pos;
    }

    void Move()
    {
        transform.Translate(Vector3.up * (speed * Time.deltaTime));

        if (transform.position.y > endY)
        {
            ResetPosition();
        }
    }
}
