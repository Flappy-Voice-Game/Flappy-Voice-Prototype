using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public Animator deadPanelAnim;
    public Animator pausePanelAnim;
    public Animator shopPanelAnim;

    public GameObject deadPanel;
    public GameObject pausePanel;
    public GameObject shopPanel;

    private void Start()
    {
        deadPanel.SetActive(false);
        pausePanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    public PP pp;

    public void openDeadPanel()
    {
        deadPanel.SetActive(true);
        deadPanelAnim.SetBool("open", true);
    }

    public void closeDeadPanel()
    {
        deadPanelAnim.SetBool("open", false);
    }

    public void openPausePanel()
    {
        pausePanel.SetActive(true);
        pausePanelAnim.SetBool("open", true);
    }

    public void closePausePanel()
    {
        pausePanelAnim.SetBool("open", false);
    }

    public void openShopPanel()
    {
        shopPanel.SetActive(true);
        if (pp.pause)
        {
            closePausePanel();
            shopPanelAnim.SetBool("open", true);
        }
        else if(pp.dead)
        {
            closeDeadPanel();
            shopPanelAnim.SetBool("open", true);
        }
    }

    public void closeShopPanel()
    {
        if (pp.pause)
        {
            openPausePanel();
            shopPanelAnim.SetBool("open", false);
        }
        else if(pp.dead)
        {
            openDeadPanel();
            shopPanelAnim.SetBool("open", false);
        }
    }
}