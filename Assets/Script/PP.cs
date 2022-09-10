using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class PP : MonoBehaviour
{
    [SerializeField] float speed, delel, ochki,bestScore;
    [SerializeField] Transform spynchik;
    [SerializeField] Text textochki, textsym,bestSoreText;
    Animator anim;


    AudioClip clip;
    float[] samples;

    void Start()
    {
       bestScore= PlayerPrefs.GetFloat("bestScore", bestScore);
        ochki = 0;
        clip = Microphone.Start(null, true, 1, 44100);
        samples = new float[clip.channels * clip.samples];
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        bestSoreText.text = "Best score: "+bestScore;
        textochki.text = "" + ochki;
        transform.position += Vector3.right * 0.01f;

        clip.GetData(samples, 0);

        float average = 0.0f;

        for (int i = 0; i < samples.Length; ++i)
        {
            average += Mathf.Abs(samples[i]);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.Play("ggCmert");
        }

        average /= samples.Length;

        // Debug.Log(Mathf.Round(average *1000));

        average *= 1000;

        if (average > 50)
        {

            transform.position += Vector3.up * average / delel;

            gameObject.transform.rotation = Quaternion.Euler(0, 0, 40);


        }
        else
        {

            transform.position += Vector3.down * speed;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -40);

        }
        Debug.Log(Mathf.Round(average));
        textsym.text = "" + Mathf.Round(average);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "gg")
        {
            if (bestScore<ochki)
            {
                bestScore = ochki;
                PlayerPrefs.SetFloat("bestScore",bestScore);
            }
            SceneManager.LoadScene(0);
            ochki = 0;
            // gameObject.transform.position = spynchik.position;
            ochki = 0;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ochko")
        {


            ochki++;



        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pole")
        {
            ochki = 0;

        }
    }
}

