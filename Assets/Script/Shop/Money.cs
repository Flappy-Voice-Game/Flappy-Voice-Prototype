using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public int money;

    public Text moneyText;

    private void Update()
    {
        moneyText.text = "Money: " + money;
    }

    public void UpdateText()
    {
        if (PlayerPrefs.HasKey("Money"))
            money = PlayerPrefs.GetInt("Money");
    }

    public void Test()
    {
        money += 100;
        PlayerPrefs.SetInt("Money", money);
        UpdateText();
    }
}
