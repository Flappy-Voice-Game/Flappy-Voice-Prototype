using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenerator : MonoBehaviour
{
    public GameObject pipesWithMoney;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        float rand = Random.Range(-3f, -1.2f);
        Vector3 pos = new Vector3(transform.position.x, rand, 0f);

        GameObject newPipe = Instantiate(pipesWithMoney, pos, Quaternion.identity);
    }
}