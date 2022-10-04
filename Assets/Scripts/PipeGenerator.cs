using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenerator : MonoBehaviour
{
    public GameObject pipesWithMoney;

    private void Start()
    {
<<<<<<< Updated upstream
        for (int i = 0; i < 5; i++)
        {
            Spawn();
        }
=======
        Spawn();
>>>>>>> Stashed changes
    }

    public void Spawn()
    {
        float rand = Random.Range(-3f, -1.2f);
        Vector3 pos = new Vector3(transform.position.x, rand, 0f);

<<<<<<< Updated upstream
        if (randPipe > 5) // спаун трубы без монетки
        {
            GameObject newPipe = Instantiate(pipes, new Vector3(pipeDistance, rand, 1), Quaternion.identity);
            pipeDistance += 3;
        }
        else // спаун трубы с монеткой
        {
            GameObject newPipe = Instantiate(pipesWithMoney, new Vector3(pipeDistance, rand, 1), Quaternion.identity);
            pipeDistance += 3.5f;
        }
=======
        GameObject newPipe = Instantiate(pipesWithMoney, pos, Quaternion.identity);
>>>>>>> Stashed changes
    }
}