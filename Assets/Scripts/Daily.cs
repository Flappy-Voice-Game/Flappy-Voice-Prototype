using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Daily : MonoBehaviour
{
    public Text time;
    public Text currentReward;

    public Animator dailyButtonAnim;
    public Animator dailyPanelAnim;

    public GameObject dailyPanel;

    private int money;

    private DateTime? lastClaimTime
    {
        get
        {
            string data = PlayerPrefs.GetString("lastClaimedTime", null);

            if (!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if (value != null)
                PlayerPrefs.SetString("lastClaimedTime", value.ToString());
            else
                PlayerPrefs.DeleteKey("lastClaimedTime");
        }
    }

    private bool canClaimReward;
    public float claimCooldown;

    private void Start()
    {
        dailyPanel.SetActive(false);
        StartCoroutine(RewardsStateUpdater());
    }

    private IEnumerator RewardsStateUpdater()
    {
        while (true)
        {
            UpdateRewardState();
            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateRewardState()
    {
        canClaimReward = true;

        if (lastClaimTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - lastClaimTime.Value;

            if (timeSpan.TotalHours < claimCooldown)
                canClaimReward = false;
        }

        UpdateRewardsUI();
    }

    private void UpdateRewardsUI()
    {
        if (canClaimReward)
            time.text = "Claim";
        else
        {
            var nextClaimTime = lastClaimTime.Value.AddHours(claimCooldown);
            var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

            string cd = $"{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

            time.text = cd;
        }
    }

    public void ClaimReward()
    {
        dailyPanel.SetActive(true);
        if (!canClaimReward)
        {
            dailyButtonAnim.SetTrigger("error");
            return;
        }

        dailyPanelAnim.SetBool("open", true);
        int rnd = UnityEngine.Random.Range(30, 100);
        currentReward.text = "you got: " + rnd + " money";
        money = PlayerPrefs.GetInt("Money");
        money += rnd;
        PlayerPrefs.SetInt("Money", money);

        lastClaimTime = DateTime.UtcNow;

        UpdateRewardState();
    }

    public void CloseDailyPanel()
    {
        dailyPanelAnim.SetBool("open", false);
    }
}
