using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class PP : MonoBehaviour
{
    [SerializeField] float speed, delel, ochki,bestScore,coin;
    [SerializeField] Transform spynchik;
    [SerializeField] Text textochki, textsym,bestSoreText,coinText;
    [SerializeField] bool StatPos;
    [SerializeField] GameObject DeadLine;
    [SerializeField] int skinNum;


    Animator anim;
    AudioClip clip;


    float[] samples;
    static bool p1;

   
    void Start()
    {
        

        coin = PlayerPrefs.GetFloat("coin",coin);      // загрузка кол. монет и лучший результат
        bestScore= PlayerPrefs.GetFloat("bestScore", bestScore);


        ochki = 0;
        StatPos = true;
        

        anim = GetComponent<Animator>();

        clip = Microphone.Start(null, true, 1, 44100); //оброботка микрофона
        samples = new float[clip.channels * clip.samples];
        
       

        PlayerPrefs.SetInt("skinNum", skinNum); //выбор скина
        if (PlayerPrefs.GetInt("skinNum") == 0)
        {
            anim.SetInteger("SN", 0);
        }
        else if (PlayerPrefs.GetInt("skinNum") == 1)
        {
            anim.SetInteger("SN", 1);
        }
        else
        {
            anim.SetInteger("SN", 0);
        }
    }

    void Update()
    {
        bestSoreText.text = "Best score: "+bestScore; // текста
        coinText.text = "" + coin;
        textochki.text = "" + ochki;


        transform.position += Vector3.right * 0.010f; //движение птицы вправо

        

        float average = 0.0f; //оброботка микрофона
        for (int i = 0; i < samples.Length; ++i)
        {
            average += Mathf.Abs(samples[i]);
        }
        clip.GetData(samples, 0);
        average /= samples.Length;
        average *= 1000;


        if (average > 60) //движение вверх и вниз
        {
            StatPos = false;
            p1 = true;
        }
        if (StatPos == false)
        {
            if (average > 60)
            {
                transform.position += Vector3.up * average / delel;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 20);
                DeadLine.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.position += Vector3.down * speed;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -20);
                DeadLine.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }


        Debug.Log(Mathf.Round(average)); //отоброжения уровня шума
        textsym.text = "" + Mathf.Round(average);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "gg") //столкновение с опасностями
        {
            if (bestScore<ochki)
            {
                bestScore = ochki;
                PlayerPrefs.SetFloat("bestScore",bestScore);
            }
            SceneManager.LoadScene(0);
            ochki = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ochko") //( ͡° ͜ʖ ͡°) тригер для очков
        {
            Destroy(collision.gameObject);
            ochki++;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pole") //очищение очков
        {
            ochki = 0;
        }
        if (collision.gameObject.tag == "coin") //тригер для монеток
        {
            Destroy(collision.gameObject);
            coin++;
            PlayerPrefs.SetFloat("coin",coin);
            Destroy(collision.gameObject);
        }
    }
}

