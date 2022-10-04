using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float speed;
    private PlayerController pp;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        pp = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (!pp.pause)
            transform.Translate(Vector2.left * speed);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("destroy", true);
        }
    }
}