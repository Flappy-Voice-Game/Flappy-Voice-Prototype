using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float speed;
    private PP pp;

    private void Start()
    {
        pp = FindObjectOfType<PP>();
    }

    private void Update()
    {
        if (!pp.pause)
            transform.Translate(Vector2.left * speed);
    }
}