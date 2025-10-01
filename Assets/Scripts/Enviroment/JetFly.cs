using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetFly : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition = new Vector3(0f, -3.5f, 0f);
    [SerializeField] private Vector3 endPosition = new Vector3(0f, 6f, 0f);
    private float lerpTime = 0.5f;
    private float currentTime;
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
        transform.position = startPosition;
        currentTime = 0f;
    }

    void Move()
    {
        if (currentTime < lerpTime)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, currentTime / lerpTime);
        }
        else
        {
            transform.position = endPosition;
            ResetPosition();
        }
    }
}
