using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteColaider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) //�������� ���� � ���� �� �������
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
