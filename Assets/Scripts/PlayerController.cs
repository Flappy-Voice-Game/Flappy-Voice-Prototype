using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] Text scoreText, noiseLevelText, bestSoreText, moneyText, moneyInShopText;
    [SerializeField] bool startPos;
    [HideInInspector][SerializeField] private string leaderBoard = ""; //МАКСУ

    [SerializeField] Slider sensitivitySlider;
    [SerializeField] Slider sensitivitySlider2;
    
    public int money;

    public bool pause;
    public bool dead;

    private int score, bestScore;
    [SerializeField] private int deadScore;

    private Rigidbody2D rb;
    private Animator anim;
    private AudioClip clip;
    private PipeGenerator pg;
    public InterstitialAds IAds;

    private float[] samples;
    private float delel = 5000f;
    private float sensitivity;

    public UI ui;

    private void Start()
    {
        pg = FindObjectOfType<PipeGenerator>();

        if (PlayerPrefs.HasKey("sens"))
        {
            sensitivity = PlayerPrefs.GetFloat("sens");
            sensitivitySlider.value = sensitivity;
            sensitivitySlider2.value = sensitivity;
        }
        else
            sensitivity = 60f;

        if (PlayerPrefs.HasKey("deadScore"))
            deadScore = PlayerPrefs.GetInt("deadScore");
        else
            deadScore = 0;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //PlayGamesPlatform.DebugLogEnabled = true;
        //PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(succesc =>
        {
            if (succesc)
            {
            }
            else
            {

            }
        });

        if (PlayerPrefs.HasKey("Money"))
            money = PlayerPrefs.GetInt("Money");
        else
            money = 0;

        bestScore = PlayerPrefs.GetInt("bestScore", bestScore);

        score = 0;
        startPos = true;

        anim = GetComponent<Animator>();

        clip = Microphone.Start(null, true, 1, 44100); //оброботка микрофона
        samples = new float[clip.channels * clip.samples];
    }

    private void Update()
    {
        bestSoreText.text = "Best score: " + bestScore; // текста
        moneyText.text = "Money: " + money;
        moneyInShopText.text = "Money: " + money;
        scoreText.text = "Score: " + score;

        float average = 0.0f; //оброботка микрофона
        for (int i = 0; i < samples.Length; i++)
            average += Mathf.Abs(samples[i]);

        anim.SetInteger("skinNum", PlayerPrefs.GetInt("skinNum"));

        clip.GetData(samples, 0);
        average /= samples.Length;
        average *= 1000;


        if (average > sensitivity) //движение вверх и вниз
            startPos = false;

        if (startPos == false)
        {
            if (average > sensitivity)
            {
                if (!pause)
                    transform.position += Vector3.up * average / delel;

                StartCoroutine(UpRotate());
                
            }
            else
            {
                if(!pause)
                    transform.position += Vector3.down * speed;

                StartCoroutine(DownRotate());
            }
        }

        noiseLevelText.text = "Noise level: " + Mathf.Round(average);
    }


    private IEnumerator UpRotate()
    {
        Quaternion Target = Quaternion.Euler(0f, 0f, 20f);
        while (transform.rotation != Target)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Target, Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator DownRotate()
    {
        Quaternion Target = Quaternion.Euler(0f, 0f, -20f);
        while (transform.rotation != Target)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Target, Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "gg") //столкновение с опасностями
        {
            transform.position = new Vector2(0f, 1000f);
            Dead();
            ui.openDeadPanel();
            deadScore += 1;
            PlayerPrefs.SetInt("deadScore", deadScore);

            if (bestScore < score)
            {
                bestScore = score;
                PlayerPrefs.SetInt("bestScore", bestScore);
                Social.ReportScore(bestScore, leaderBoard,(bool success)=> { });
            }

            if (deadScore == 5)
            {
                IAds.ShowAd();
                deadScore = 0;
                PlayerPrefs.SetInt("deadScore", deadScore);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ochko") //тригер для очков
        {
            pg.Spawn();
            Destroy(collision.gameObject);
            score++;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pole") //очищение очков
            score = 0;

        if (collision.gameObject.tag == "coin") //тригер для монеток
        {
            Destroy(collision.gameObject);
            money++;
            PlayerPrefs.SetInt("Money", money);
            Destroy(collision.gameObject);
        }
    }

    public void Pause()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        pause = true;
    }

    public void Dead()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        dead = true;
    }

    public void Resume()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        pause = false;
    }

    public void SaveSensitivity()
    {
        sensitivity = sensitivitySlider.value;
        PlayerPrefs.SetFloat("sens", sensitivity);
    }
    public void SaveSensitivity2()
    {
        sensitivity = sensitivitySlider2.value;
        PlayerPrefs.SetFloat("sens", sensitivity);
    }

    public void TabLid()
    {
        Social.ShowLeaderboardUI();
    }
}