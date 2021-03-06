using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    Text level;
    Text playerName;
    Image healthSlider;
    Image expSlider;
    PlayerStats playerStats;

    void Awake()
    {
        level = transform.GetChild(2).GetComponent<Text>();
        playerName = transform.GetChild(3).GetComponent<Text>();
        healthSlider = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        expSlider = transform.GetChild(1).GetChild(0).GetComponent<Image>();
    }

    void Start()
    {
        playerStats = GameManager.Instance.playerStats;
    }

    void Update()
    {
        level.text = "Level " + playerStats.CurrentLevel.ToString("00");
        UpdateHealth();
        UpdateExp();
    }

    void UpdateHealth()
    {
        float sliderPercent = (float)playerStats.CurrentHealth / playerStats.MaxHealth;
        healthSlider.fillAmount = sliderPercent;
    }

    void UpdateExp()
    {
        float sliderPercent = (float)playerStats.CurrentExp / playerStats.NextLevelExp;
        expSlider.fillAmount = sliderPercent;
    }
}
