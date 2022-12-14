using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinControl : MonoBehaviour
{
    public int skinNum;
    public int price;

    public Text priceText;

    public Image[] skins;

    public PP pp;

    private void Start()
    {
        pp = FindObjectOfType<PP>();

        if (PlayerPrefs.GetInt("skin1" + "buy") == 0)
        {
            foreach (Image img in skins)
            {
                if ("skin1" == img.name)
                {
                    PlayerPrefs.SetInt("skin1" + "buy", 1);
                    PlayerPrefs.SetInt("skin1" + "equip", 1);
                }
                else
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "buy", 0);
            }
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 0)
            priceText.text = "price: " + price;
        else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 1)
        {
            if (PlayerPrefs.GetInt(GetComponent<Image>().name + "equip") == 1)
                priceText.text = "equipped";
            else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "equip") == 0)
                priceText.text = "equip";
        }
    }

    public void buy()
    {
        if (PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 0)
        {
            if (pp.money >= price)
            {
                priceText.text = "equipped";
                pp.money -= price;

                PlayerPrefs.SetInt("Money", pp.money);
                PlayerPrefs.SetInt(GetComponent<Image>().name + "buy", 1);
                PlayerPrefs.SetInt("skinNum", skinNum);

                foreach(Image img in skins)
                {
                    if (GetComponent<Image>().name == img.name)
                        PlayerPrefs.SetInt(GetComponent<Image>().name + "equip", 1);
                    else
                        PlayerPrefs.SetInt(img.name + "equip", 0);
                }
            }
        }
        else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 1)
        {
            priceText.text = "equipped";

            PlayerPrefs.SetInt(GetComponent<Image>().name + "equip", 1);
            PlayerPrefs.SetInt("skinNum", skinNum);

            foreach(Image img in skins)
            {
                if (GetComponent<Image>().name == img.name)
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "equip", 1);
                else
                    PlayerPrefs.SetInt(img.name + "equip", 0);
            }
        }
    }
}
