using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PP : MonoBehaviour
{
    [SerializeField] private float delel, speed;
    [SerializeField] Transform spawn;
    [SerializeField] Text scoreText, noiseLevelText, bestSoreText, moneyText, moneyInShopText;
    [SerializeField] bool statPos;
    [SerializeField] GameObject DeadLine, deadPanel;
    [HideInInspector][SerializeField] private string leaderBoard = ""; //МАКСУ
    [SerializeField] Slider sliderMicro;
    
    public int money;
   public int ReklamChet;

    public bool pause;
    public bool _isSpawn;
    public bool dead;

    private int score, bestScore;
    [SerializeField] private int currentSkin;

    private Rigidbody2D rb;
    private Animator anim;
    private AudioClip clip;
    public PipeGenerator pg;
    private AdsInitializer ad;

    private float[] samples;

    public UI ui;

    private void Start()
    {
        ReklamChet = PlayerPrefs.GetInt("RCH", ReklamChet );
        delel = PlayerPrefs.GetFloat("delel",delel);
        sliderMicro.value = PlayerPrefs.GetFloat("delel", sliderMicro.value);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(succesc =>
        {
            if (succesc)
            {
            }
            else
            {

            }
        });
        money = PlayerPrefs.GetInt("Money", money);      // загрузка кол. монет и лучший результат
        bestScore = PlayerPrefs.GetInt("bestScore", bestScore);

        score = 0;
        statPos = true;

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

       
        PlayerPrefs.SetFloat("delel", sliderMicro.value);
        delel = PlayerPrefs.GetFloat("delel", delel);
        currentSkin = PlayerPrefs.GetInt("skinNum");
        anim.SetInteger("skinNum", currentSkin);

        float average = 0.0f; //оброботка микрофона
        for (int i = 0; i < samples.Length; ++i)
            average += Mathf.Abs(samples[i]);

        clip.GetData(samples, 0);
        average /= samples.Length;
        average *= 1000;


        if (average > 60) //движение вверх и вниз
            statPos = false;

        if (statPos == false)
        {
            if (average > 60)
            {
                _isSpawn = true;
                if(!pause)
                    transform.position += Vector3.up * average/delel;

                gameObject.transform.rotation = Quaternion.Euler(0, 0, 20);
                DeadLine.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                if(!pause)
                    transform.position += Vector3.down * speed;

                gameObject.transform.rotation = Quaternion.Euler(0, 0, -20);
                DeadLine.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        //Debug.Log(Mathf.Round(average)); //отоброжения уровня шума
        noiseLevelText.text = "Noise level: " + Mathf.Round(average);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "gg") //столкновение с опасностями
        {

            transform.position = new Vector2(0f, 1000f);
            Dead();
            ui.openDeadPanel();
            if (bestScore < score)
            {
                bestScore = score;
                PlayerPrefs.SetInt("bestScore", bestScore);
                Social.ReportScore(bestScore, leaderBoard,(bool success)=> { });
            }
            if (ReklamChet==5)
            {
                ReklamChet = 0;
                ad.ShowRewardedAds();
            }
            else
            {
                ReklamChet++;
            }
            PlayerPrefs.SetInt("RCH", ReklamChet);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ochko") //тригер для очков
        {
            Destroy(collision.gameObject);
            score++;
            pg.Spawn();
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

    public void AddMoney()
    {
        money += 10;
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
    public void TabLid()
    {
        Social.ShowLeaderboardUI();
    }
    public void Exit()
    {
        //PlayGamesPlatform.Instance.SignOut();
    }
}