using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTrub : MonoBehaviour
{
    public GameObject pipes;
    public GameObject fon;

    [SerializeField] float rastTrub = 0;
    [SerializeField] float rastfona = 0;

    private PP pp;

    private void Start()
    {
        pp = FindObjectOfType<PP>();
    }

    private void Update()
    {
        if (!pp.pause)
        {
            StartCoroutine(Spawner());
            StartCoroutine(SpawnerFona());
        }
    }

    IEnumerator Spawner()
    {
        while (true)
        {

            yield return new WaitForSeconds(2);
            float rand = Random.Range(-1.87f, -0.25f);
            GameObject newPipe = Instantiate(pipes, new Vector3(rastTrub, rand, 1), Quaternion.identity);
            rastTrub += 5;
            Destroy(newPipe, 20);
        }

    }

    IEnumerator SpawnerFona()
    {
        while (true)
        {

            yield return new WaitForSeconds(1.5f);
            GameObject newPipe = Instantiate(fon, new Vector3(rastfona, 0.57f, 0), Quaternion.identity);
            rastfona += 2.85f;
            Destroy(newPipe, 20);
        }

    }
}
