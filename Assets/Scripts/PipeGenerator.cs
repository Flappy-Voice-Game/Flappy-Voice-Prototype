using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenerator : MonoBehaviour
{
    public GameObject pipes;
    public GameObject pipesWithMoney;

    private float pipeDistance = 5;

    private void Start()
    {
        for (int i = 0; i < 1000; i++)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        float randPipe = Random.Range(1, 10); //���������� ������ ���� � �������
        float rand = Random.Range(-3f, -1.2f);

        if (randPipe > 5) // ����� ����� ��� �������
        {
            GameObject newPipe = Instantiate(pipes, new Vector3(pipeDistance, rand, 1), Quaternion.identity);
            pipeDistance += 4;
        }
        else // ����� ����� � ��������
        {
            GameObject newPipe = Instantiate(pipesWithMoney, new Vector3(pipeDistance, rand, 1), Quaternion.identity);
            pipeDistance += 4.5f;
        }
    }
}