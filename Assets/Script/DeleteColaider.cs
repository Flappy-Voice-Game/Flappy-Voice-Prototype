using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteColaider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) //удаление труб и фона за игроком
    {
        if (collision.gameObject.tag=="gg")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "fon")
        {
            Destroy(collision.gameObject);
        }
    }
}
