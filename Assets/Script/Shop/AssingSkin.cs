using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssingSkin : MonoBehaviour
{
    public Sprite standart;
    public Sprite gray;
    public Sprite blue;
    public Sprite green;

    public GameObject player;

    private void Start()
    {
        if (PlayerPrefs.GetInt("skinNum") == 1)
            player.GetComponent<SpriteRenderer>().sprite = gray;
        else if (PlayerPrefs.GetInt("skinNum") == 2)
            player.GetComponent<SpriteRenderer>().sprite = blue;
        else if (PlayerPrefs.GetInt("skinNum") == 3)
            player.GetComponent<SpriteRenderer>().sprite = green;
        else
            player.GetComponent<SpriteRenderer>().sprite = standart;
    }
}
