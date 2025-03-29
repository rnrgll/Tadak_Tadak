using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // public GameObject gameOverScreen;
    // public TextMeshProUGUI finalScoreText;
    
    public LifeUI lifeUI;

    void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.uiManager = this;
            UpdateScore(0);
        }
    }

    /// <summary>
    /// 게임매니저의 프로퍼티 상에서 자동 호출되므로 직접 호출은 지양 바랍니다 
    /// </summary>
    /// <param name="points">점수</param>
    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateLife(int life)
    {
        lifeUI.UpdateLifeUI(life);
    }

    public void ShowGameOverScreen(int finalScore)
    {
        // gameOverScreen.SetActive(true);
        // finalScoreText.text = "Final Score: " + finalScore;
    }
}
