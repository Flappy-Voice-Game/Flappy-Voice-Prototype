using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTrub : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pipes;
    public GameObject pipesMones;
    public GameObject fon;

    [SerializeField] float rastTrub=0;
    [SerializeField] float rastfona = 0;
    void Start()
    {
        StartCoroutine(Spawner()); //������ �������
        StartCoroutine(SpawnerFona());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            float randTruba = Random.Range(1,10); //���������� ������ ���� � �������
            yield return new WaitForSeconds(1);
            float rand = Random.Range(-1.87f, -0.25f);

            if (randTruba > 5) // ����� ����� ��� �������
            {                
                GameObject newPipe = Instantiate(pipes, new Vector3(rastTrub, rand, 1), Quaternion.identity);
                rastTrub += 5;
            }
            else // ����� ����� � ��������
            {
                
                GameObject newPipe = Instantiate(pipesMones, new Vector3(rastTrub, rand, 1), Quaternion.identity);
                rastTrub += 5;
            }       
        }
        
    }
    IEnumerator SpawnerFona() //����� ����
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            GameObject newPipe = Instantiate(fon, new Vector3(rastfona, 0.57f, 0), Quaternion.identity);
            rastfona += 2.85f;
        }
    }
}
