using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTrub : MonoBehaviour
{
    public GameObject pipes;
    public GameObject pipesMones;
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


        StartCoroutine(Spawner()); //çàïóñê êàðóòèí
        StartCoroutine(SpawnerFona());
    }


    IEnumerator Spawner()
    {
        while (true)
        {


            yield return new WaitForSeconds(2);

            float randTruba = Random.Range(1,10); //ðàíäîìÿòñÿ âûñîòà òðóá è ìîíåòêè
            yield return new WaitForSeconds(1);

            float rand = Random.Range(-1.87f, -0.25f);

            if (randTruba > 5) // ñïàóí òðóáû áåç ìîíåòêè
            {                
                GameObject newPipe = Instantiate(pipes, new Vector3(rastTrub, rand, 1), Quaternion.identity);
                rastTrub += 5;
            }
            else // ñïàóí òðóáû ñ ìîíåòêîé
            {
                
                GameObject newPipe = Instantiate(pipesMones, new Vector3(rastTrub, rand, 1), Quaternion.identity);
                rastTrub += 5;
            }       
        }

    }

    IEnumerator SpawnerFona() //ñïàóí ôîíà
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            GameObject newPipe = Instantiate(fon, new Vector3(rastfona, 0.57f, 0), Quaternion.identity);
            rastfona += 2.85f;
        }
    }
}
