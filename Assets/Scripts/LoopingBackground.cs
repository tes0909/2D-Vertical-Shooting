using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private float loopMultiplier = 2f;
    private float backgroundHeight = 15.36f;
    void Update()
    {
        ScrollBackground();
        LoopBackground();
    }

    void ScrollBackground()
    {
        transform.Translate(Vector2.down * (scrollSpeed * Time.deltaTime));
    }

    void LoopBackground()
    {
        if (transform.position.y <= -backgroundHeight)
            transform.position += new Vector3(0, backgroundHeight * loopMultiplier, 0);
    }
}
